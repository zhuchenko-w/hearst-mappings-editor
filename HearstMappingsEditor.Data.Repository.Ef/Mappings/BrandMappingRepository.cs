using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class BrandMappingRepository : 
        BaseMappingRepository<BrandMapping, BrandMappingViewModel, BrandMappingFilter, BrandMappingRestriction>,
        IMappingRepository<BrandMappingViewModel, BrandMappingFilter, BrandMappingRestriction>
    {
        protected override string SingularEntityName => "brand mapping";
        protected override string PluralEntityName => "brand mappings";

        public BrandMappingRepository(IRestrictionsRepository restrictionsRepository) : base(restrictionsRepository) { }

        public override async Task<IList<BrandMappingViewModel>> GetList(BrandMappingFilter filter)
        {
            using(var db = new FinancialStatementContext())
            {
                var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
                var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
                var lobDetailIDsFilterEmpty = filter.LOBDetailIDs == null || filter.LOBDetailIDs.Count == 0;
                var projectIDsFilterEmpty = filter.ProjectIDs == null || filter.ProjectIDs.Count == 0;

                var query = from bm in db.BrandMappings
                            from dimProject in db.DimProjects.Where(p => p.ProjectID == bm.ProjectID).DefaultIfEmpty()
                            from dimBrand in db.DimBrands.Where(p => p.LOBDetailID == bm.LOBDetailID).DefaultIfEmpty()
                            where  (createDateFromFilterEmpty || bm.CreateDate.HasValue && bm.CreateDate.Value >= filter.CreateDateFrom.Value)
                                && (createDateToFilterEmpty || bm.CreateDate.HasValue && bm.CreateDate.Value <= filter.CreateDateTo.Value)
                                && (lobDetailIDsFilterEmpty || filter.LOBDetailIDs.Contains(bm.LOBDetailID))
                                && (projectIDsFilterEmpty || filter.ProjectIDs.Contains(bm.ProjectID))
                            select new {
                                LOBDetailID = bm.LOBDetailID,
                                LOBDetailCode = dimBrand.LOBDetailCode + " (" + dimBrand.LOBDetailDesc + ")",
								ProjectID = bm.ProjectID,
                                ProjectCode = dimProject.ManagementProject + " (" + dimProject.Description + ")",
								CreateDate = bm.CreateDate,
								RowId = bm.RowId
                            };

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case BrandMappingSortTypes.LOBDetail:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.LOBDetailCode) : query.OrderByDescending(p => p.LOBDetailCode);
                            break;
                        case BrandMappingSortTypes.Project:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProjectCode) : query.OrderByDescending(p => p.ProjectCode);
                            break;
                        case BrandMappingSortTypes.CreateDate:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.CreateDate) : query.OrderByDescending(p => p.CreateDate);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.CreateDate);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreateDate);
                }

                if (filter.Skip.HasValue)
                {
                    query = query.Skip(filter.Skip.Value);
                }
                if (filter.Take.HasValue)
                {
                    query = query.Take(filter.Take.Value);
                }

                try
                {
                    return (await query.ToListAsync()).Select(p => new BrandMappingViewModel
                    {
                        LOBDetailID = p.LOBDetailID,
                        LOBDetailCode = p.LOBDetailCode,
                        ProjectID = p.ProjectID,
                        ProjectCode = p.ProjectCode,
                        CreateDate = p.CreateDate,
						RowId = p.RowId,
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting brand mappings", ex);
                }
            }
        }

        protected override async Task<IList<BrandMappingRestriction>> GetRestrictions(FinancialStatementContext db)
        {
            return await _restrictionsRepository.GetBrandMappingRestrictions(db);
        }
        protected override bool CheckRestrictionMatchesModel(BrandMappingRestriction restriction, BrandMappingViewModel model)
        {
            return (restriction.LOBDetailIdIsSet || restriction.ProjectIdIsSet)
                    && (!restriction.LOBDetailIdIsSet || restriction.LOBDetailID == model.LOBDetailID)
                    && (!restriction.ProjectIdIsSet || restriction.ProjectID == model.ProjectID);
        }

        protected override void RemoveMappingFunc(FinancialStatementContext db, BrandMapping existingMapping)
        {
            db.BrandMappings.Remove(existingMapping);
        }

        protected override void CheckRestrictionsOnEditFunc(BrandMappingRestriction restriction, BrandMappingViewModel existingModel, BrandMappingViewModel model, IList<string> errorList)
        {
            if (restriction.LOBDetailIdIsSet && existingModel.LOBDetailID != model.LOBDetailID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("LOBDetailID", existingModel.RowId));
            }
            if (restriction.ProjectIdIsSet && existingModel.ProjectID != model.ProjectID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("ProjectID", existingModel.RowId));
            }
        }
        protected override IList<LogParams> EditMappingFunc(FinancialStatementContext db, BrandMapping existingMapping, BrandMappingViewModel existingModel, BrandMappingViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingMapping.ProjectID != model.ProjectID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(ProjectID) ProjectCode",
                    existingModel.ProjectID,
                    existingModel.ProjectCode,
                    model.ProjectID,
                    model.ProjectCode);
                existingMapping.ProjectID = model.ProjectID;
            }
            if (existingMapping.LOBDetailID != model.LOBDetailID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(LOBDetailID) BrandCode",
                    existingModel.LOBDetailID,
                    existingModel.LOBDetailCode,
                    model.LOBDetailID,
                    model.LOBDetailCode);
                existingMapping.LOBDetailID = model.LOBDetailID;
            }

            return logEntries;
        }
        protected override void AddMappingFunc(FinancialStatementContext db, BrandMappingViewModel model)
        {
            var newMapping = TinyMapper.Map<BrandMapping>(model);
            newMapping.CreateDate = DateTime.Now;
            db.BrandMappings.Add(newMapping);
        }

        protected override async Task<BrandMapping> GetSingle(FinancialStatementContext db, long rowId)
        {
            return await db.BrandMappings.FirstOrDefaultAsync(p => p.RowId == rowId);
        }
        protected override IQueryable<BrandMappingViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId)
        {
            return from bm in db.BrandMappings
                   from dimProject in db.DimProjects.Where(p => p.ProjectID == bm.ProjectID).DefaultIfEmpty()
                   from dimBrand in db.DimBrands.Where(p => p.LOBDetailID == bm.LOBDetailID).DefaultIfEmpty()
                   where bm.RowId == rowId
                   select new BrandMappingViewModel
                   {
                       LOBDetailID = bm.LOBDetailID,
                       LOBDetailCode = dimBrand.LOBDetailCode,
                       ProjectID = bm.ProjectID,
                       ProjectCode = dimProject.ProjectCode,
                       CreateDate = bm.CreateDate,
                       RowId = bm.RowId
                   };
        }

        protected override string GetDetailsForLog(BrandMappingViewModel model)
        {
            return $"LOBDetailID: {model.LOBDetailID}; " +
                   $"LOBDetailCode: {model.LOBDetailCode}; " +
                   $"ProjectID: {model.ProjectID}; " +
                   $"ProjectCode: {model.ProjectCode}";
        }
    }
}
