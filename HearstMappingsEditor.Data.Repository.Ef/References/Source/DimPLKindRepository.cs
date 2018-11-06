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
    public class DimPLKindRepository : 
        BaseReferenceExtendedRepository<DimPLKind, DimPLKindViewModel, DimPLKindFilter>, 
        IReferenceRepository<DimPLKindViewModel, DimPLKindFilter>
    {
        protected override string SingularEntityName => "DimPLKind";
        protected override string PluralEntityName => "DimPLKinds";

        public override async Task<IList<DimPLKindViewModel>> GetList(DimPLKindFilter filter)
        {
            using (var db = new FinancialStatementContext())
            {
                filter.PLKindName = filter.PLKindName?.Trim();

                var plGroupIDsFilterEmpty = filter.PLGroupIDs == null || filter.PLGroupIDs.Count == 0;
                var plKindNameFilterEmpty = string.IsNullOrEmpty(filter.PLKindName);

                var query = from dplk in db.DimPLKinds
                            from dimPLGroup in db.DimPLGroups.Where(p => p.PLGroupID == dplk.PLGroupID).DefaultIfEmpty()
                            where (plGroupIDsFilterEmpty || dplk.PLGroupID.HasValue && filter.PLGroupIDs.Contains(dplk.PLGroupID.Value))
                                && (plKindNameFilterEmpty || dplk.PLKindName != null && dplk.PLKindName.Contains(filter.PLKindName))
                            select new
                            {
                                PLGroupID = dimPLGroup.PLGroupID,
                                PLGroupName = dimPLGroup.PLGroupName,
                                PLKindID = dplk.PLKindID,
                                PLKindName = dplk.PLKindName
                            };

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case DimPLKindSortTypes.Id:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLKindID) : query.OrderByDescending(p => p.PLKindID);
                            break;
                        case DimPLKindSortTypes.PLGroupName:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLGroupName) : query.OrderByDescending(p => p.PLGroupName);
                            break;
                        case DimPLKindSortTypes.PLKindName:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLKindName) : query.OrderByDescending(p => p.PLKindName);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.PLKindID);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(p => p.PLKindID);
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
                    return (await query.ToListAsync()).Select(p => new DimPLKindViewModel
                    {
                        PLGroupID = p.PLGroupID,
                        PLGroupName = p.PLGroupName,
                        PLKindID = p.PLKindID,
                        PLKindName = p.PLKindName
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting DimPLKind items", ex);
                }
            }
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimPLKind existingItem)
        {
            db.DimPLKinds.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimPLKindViewModel model)
        {
            if (string.IsNullOrEmpty(model.PLKindName))
            {
                throw new PublicException("PLKindName can not be null or empty");
            }
            var newItem = TinyMapper.Map<DimPLKind>(model);
            db.DimPLKinds.Add(newItem);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimPLKind existingItem, DimPLKindViewModel existingModel, DimPLKindViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.PLGroupID != model.PLGroupID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(PLGroupID) PLGroupName",
                    existingModel.PLGroupID,
                    existingModel.PLGroupName,
                    model.PLGroupID,
                    model.PLGroupName);
                existingItem.PLGroupID = model.PLGroupID;
            }
            if (existingItem.PLKindName != model.PLKindName)
            {
                if (string.IsNullOrEmpty(model.PLKindName))
                {
                    throw new PublicException("PLKindName can not be null or empty");
                }
                AddPropertyChangeLogEntry(logEntries, model, "PLKindName", existingItem.PLKindName, model.PLKindName);
                existingItem.PLKindName = model.PLKindName;
            }

            return logEntries;
        }

        protected override async Task<DimPLKind> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimPLKinds.FirstOrDefaultAsync(p => p.PLKindID == id);
        }
        protected override IQueryable<DimPLKindViewModel> GetSingleModelQuery(FinancialStatementContext db, long id)
        {
            return from dplk in db.DimPLKinds
                   from dimPLGroup in db.DimPLGroups.Where(p => p.PLGroupID == dplk.PLGroupID).DefaultIfEmpty()
                   where dplk.PLKindID == id
                   select new DimPLKindViewModel
                   {
                       PLGroupID = dimPLGroup.PLGroupID,
                       PLGroupName = dimPLGroup.PLGroupName,
                       PLKindID = dplk.PLKindID,
                       PLKindName = dplk.PLKindName
                   };
        }

        protected override string GetDetailsForLog(DimPLKindViewModel model)
        {
            return $"PLKindID: {model.PLKindID}; " +
                   $"PLKindName: {model.PLKindName}; " +
                   $"PLGroupID: {model.PLGroupID}; " +
                   $"PLGroupName: {model.PLGroupName}";
        }
    }
}
