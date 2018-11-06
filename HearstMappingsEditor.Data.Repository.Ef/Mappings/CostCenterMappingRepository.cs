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
    public class CostCenterMappingRepository : 
        BaseMappingRepository<CostCenterMapping, CostCenterMappingViewModel, CostCenterMappingFilter, CostCenterMappingRestriction>, 
        IMappingRepository<CostCenterMappingViewModel, CostCenterMappingFilter, CostCenterMappingRestriction>
    {
        protected override string SingularEntityName => "department mapping";
        protected override string PluralEntityName => "department mappings";

        public CostCenterMappingRepository(IRestrictionsRepository restrictionsRepository) : base(restrictionsRepository) { }

        public override async Task<IList<CostCenterMappingViewModel>> GetList(CostCenterMappingFilter filter)
        {
            using(var db = new FinancialStatementContext())
            {
                var deptIDsFilterEmpty = filter.DeptIDs == null || filter.DeptIDs.Count == 0;
                var costCenterIDsFilterEmpty = filter.CostCenterIDs == null || filter.CostCenterIDs.Count == 0;
                var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
                var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
                var printDigitalsFilterEmpty = filter.PrintDigitals == null || filter.PrintDigitals.Count == 0;

                var query = from ccm in db.CostCenterMappings
                            from dimDept in db.DimDepts.Where(p => p.DeptID == ccm.DeptID).DefaultIfEmpty()
                            from dimCostCenter in db.DimCostCenters.Where(p => p.CostCenterID == ccm.CostCenterID).DefaultIfEmpty()
                            where (costCenterIDsFilterEmpty || filter.CostCenterIDs.Contains(ccm.CostCenterID))
                                && (deptIDsFilterEmpty || filter.DeptIDs.Contains(ccm.DeptID) || dimDept.Dept == null && filter.DeptIDs.Contains(0))
                                && (printDigitalsFilterEmpty || !string.IsNullOrEmpty(ccm.PrintDigital) && filter.PrintDigitals.Contains(ccm.PrintDigital))
                                && (createDateFromFilterEmpty || ccm.CreateDate.HasValue && ccm.CreateDate.Value >= filter.CreateDateFrom.Value)
                                && (createDateToFilterEmpty || ccm.CreateDate.HasValue && ccm.CreateDate.Value <= filter.CreateDateTo.Value)
                            select new {
                                DeptID = ccm.DeptID,
                                Dept = dimDept.Dept,
                                CostCenterID = ccm.CostCenterID,
                                CostCenterDesc = dimCostCenter.CostCenterDesc,
                                PrintDigital = ccm.PrintDigital,
                                CreateDate = ccm.CreateDate,
								RowId = ccm.RowId
                            };

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case CostCenterMappingSortTypes.Dept:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.Dept) : query.OrderByDescending(p => p.Dept);
                            break;
                        case CostCenterMappingSortTypes.PrintDigital:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PrintDigital) : query.OrderByDescending(p => p.PrintDigital);
                            break;
                        case CostCenterMappingSortTypes.CostCenter:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.CostCenterDesc) : query.OrderByDescending(p => p.CostCenterDesc);
                            break;
                        case CostCenterMappingSortTypes.CreateDate:
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
                    return (await query.ToListAsync()).Select(p => new CostCenterMappingViewModel
                    {
                        DeptID = p.DeptID,
                        Dept = p.Dept,
                        CostCenterID = p.CostCenterID,
                        CostCenterDesc = p.CostCenterDesc,
                        PrintDigital = p.PrintDigital,
                        CreateDate = p.CreateDate,
						RowId = p.RowId
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting department mappings", ex);
                }
            }
        }

        protected override async Task<IList<CostCenterMappingRestriction>> GetRestrictions(FinancialStatementContext db)
        {
            return await _restrictionsRepository.GetCostCenterMappingRestrictions(db);
        }
        protected override bool CheckRestrictionMatchesModel(CostCenterMappingRestriction restriction, CostCenterMappingViewModel model)
        {
            return (restriction.CostCenterIdIsSet || restriction.DeptIdIsSet || restriction.PrintDigitalIsSet)
                    && (!restriction.CostCenterIdIsSet || restriction.CostCenterID == model.CostCenterID)
                    && (!restriction.DeptIdIsSet || restriction.DeptID == model.DeptID)
                    && (!restriction.PrintDigitalIsSet || restriction.PrintDigital == model.PrintDigital);
        }

        protected override void RemoveMappingFunc(FinancialStatementContext db, CostCenterMapping existingMapping)
        {
            db.CostCenterMappings.Remove(existingMapping);
        }

        protected override void CheckRestrictionsOnEditFunc(CostCenterMappingRestriction restriction, CostCenterMappingViewModel existingModel, CostCenterMappingViewModel model, IList<string> errorList)
        {
            if (restriction.CostCenterIdIsSet && existingModel.CostCenterID != model.CostCenterID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("CostCenterID", existingModel.RowId));
            }
            if (restriction.DeptIdIsSet && existingModel.DeptID != model.DeptID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("DeptID", existingModel.RowId));
            }
            if (restriction.PrintDigitalIsSet && existingModel.PrintDigital != model.PrintDigital)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("PrintDigital", existingModel.RowId));
            }
        }
        protected override IList<LogParams> EditMappingFunc(FinancialStatementContext db, CostCenterMapping existingMapping, CostCenterMappingViewModel existingModel, CostCenterMappingViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingMapping.DeptID != model.DeptID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(DeptID) Dept",
                    existingModel.DeptID,
                    existingModel.Dept,
                    model.DeptID,
                    model.Dept);
                existingMapping.DeptID = model.DeptID;
            }
            if (existingMapping.CostCenterID != model.CostCenterID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(CostCenterID) CostCenterDesc",
                    existingModel.CostCenterID,
                    existingModel.CostCenterDesc,
                    model.CostCenterID,
                    model.CostCenterDesc);
                existingMapping.CostCenterID = model.CostCenterID;
            }
            if (existingMapping.PrintDigital != model.PrintDigital)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "PrintDigital",
                    existingModel.PrintDigital,
                    model.PrintDigital);
                existingMapping.PrintDigital = model.PrintDigital;
            }

            return logEntries;
        }
        protected override void AddMappingFunc(FinancialStatementContext db, CostCenterMappingViewModel model)
        {
            var newMapping = TinyMapper.Map<CostCenterMapping>(model);
            newMapping.CreateDate = DateTime.Now;
            db.CostCenterMappings.Add(newMapping);
        }

        protected override async Task<CostCenterMapping> GetSingle(FinancialStatementContext db, long rowId)
        {
            return await db.CostCenterMappings.FirstOrDefaultAsync(p => p.RowId == rowId);
        }
        protected override IQueryable<CostCenterMappingViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId)
        {
            return from ccm in db.CostCenterMappings
                   from dimDept in db.DimDepts.Where(p => p.DeptID == ccm.DeptID).DefaultIfEmpty()
                   from dimCostCenter in db.DimCostCenters.Where(p => p.CostCenterID == ccm.CostCenterID).DefaultIfEmpty()
                   where ccm.RowId == rowId
                   select new CostCenterMappingViewModel
                   {
                       DeptID = ccm.DeptID,
                       Dept = dimDept.Dept,
                       CostCenterID = ccm.CostCenterID,
                       CostCenterDesc = dimCostCenter.CostCenterDesc,
                       PrintDigital = ccm.PrintDigital,
                       CreateDate = ccm.CreateDate,
                       RowId = rowId
                   };
        }

        protected override string GetDetailsForLog(CostCenterMappingViewModel model)
        {
            return $"DeptID: {model.DeptID}; " +
                   $"Dept: {model.Dept}; " +
                   $"CostCenterID: {model.CostCenterID}; " +
                   $"CostCenterDesc: {model.CostCenterDesc}; " +
                   $"PrintDigital: {model.PrintDigital}";
        }
    }
}
