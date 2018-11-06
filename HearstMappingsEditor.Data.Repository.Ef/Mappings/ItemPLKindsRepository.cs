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
    public class ItemPLKindsRepository : 
        BaseMappingRepository<ItemPLKinds, ItemPLKindsViewModel, ItemPLKindsFilter, ItemPLKindsRestriction>, 
        IMappingRepository<ItemPLKindsViewModel, ItemPLKindsFilter, ItemPLKindsRestriction>
    {
        protected override string SingularEntityName => "ItemPLKind";
        protected override string PluralEntityName => "ItemPLKinds";

        public ItemPLKindsRepository(IRestrictionsRepository restrictionsRepository) : base(restrictionsRepository) { }

        public override async Task<IList<ItemPLKindsViewModel>> GetList(ItemPLKindsFilter filter)
        {
            using (var db = new FinancialStatementContext())
            {
                var deptIDsFilterEmpty = filter.DeptIDs == null || filter.DeptIDs.Count == 0;
                var itemIDsFilterEmpty = filter.ItemIDs == null || filter.ItemIDs.Count == 0;
                var plKindIDsFilterEmpty = filter.PLKindIDs == null || filter.PLKindIDs.Count == 0;

                var query = (from iplk in db.ItemPLKinds
                             from dimDept in db.DimDepts.Where(p => p.DeptID == iplk.DeptID).DefaultIfEmpty()
                             from dimItem in db.DimItems.Where(p => p.ItemID == iplk.ItemID).DefaultIfEmpty()
                             from dimPLKinds in db.DimPLKinds.Where(p => p.PLKindID == iplk.PLKindID).DefaultIfEmpty()
                             where (deptIDsFilterEmpty || iplk.DeptID.HasValue && filter.DeptIDs.Contains(iplk.DeptID.Value) || !iplk.DeptID.HasValue && filter.DeptIDs.Contains(0))
                                 && (itemIDsFilterEmpty || filter.ItemIDs.Contains(iplk.ItemID))
                                 && (plKindIDsFilterEmpty ||filter.PLKindIDs.Contains(iplk.PLKindID))
                             select new
                             {
                                 Dept = dimDept.Dept,
                                 DeptID = iplk.DeptID,
                                 ItemID = iplk.ItemID,
                                 ItemUAN = dimItem.UAN,
                                 PLKindID = iplk.PLKindID,
                                 PLKindName = dimPLKinds.PLKindName,
                                 RowId = iplk.RowId
                             });

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case ItemPLKindsSortTypes.Dept:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.Dept) : query.OrderByDescending(p => p.Dept);
                            break;
                        case ItemPLKindsSortTypes.Item:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.ItemUAN) : query.OrderByDescending(p => p.ItemUAN);
                            break;
                        case ItemPLKindsSortTypes.PLKind:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLKindName) : query.OrderByDescending(p => p.PLKindName);
                            break;
                        case ItemPLKindsSortTypes.RowId:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.RowId) : query.OrderByDescending(p => p.RowId);
                            break;
                        default:
                            query = query.OrderByDescending(p => p.RowId);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(p => p.RowId);
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
                    return (await query.ToListAsync()).Select(p => new ItemPLKindsViewModel
                    {
                        Dept = p.Dept,
                        DeptID = p.DeptID,
                        ItemID = p.ItemID,
                        ItemUAN = p.ItemUAN,
                        PLKindID = p.PLKindID,
                        PLKindName = p.PLKindName,
                        RowId = p.RowId
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting ItemPLKinds", ex);
                }
            }
        }

        protected override async Task<IList<ItemPLKindsRestriction>> GetRestrictions(FinancialStatementContext db)
        {
            return new List<ItemPLKindsRestriction>();
        }
        protected override bool CheckRestrictionMatchesModel(ItemPLKindsRestriction restriction, ItemPLKindsViewModel model)
        {
            return true;
        }

        protected override void RemoveMappingFunc(FinancialStatementContext db, ItemPLKinds existingMapping)
        {
            db.ItemPLKinds.Remove(existingMapping);
        }

        protected override void CheckRestrictionsOnEditFunc(ItemPLKindsRestriction restriction, ItemPLKindsViewModel existingModel, ItemPLKindsViewModel model, IList<string> errorList)
        {
        }
        protected override IList<LogParams> EditMappingFunc(FinancialStatementContext db, ItemPLKinds existingMapping, ItemPLKindsViewModel existingModel, ItemPLKindsViewModel model)
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
            if (existingMapping.ItemID != model.ItemID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(ItemID) ItemUAN",
                    existingModel.ItemID,
                    existingModel.ItemUAN,
                    model.ItemID,
                    model.ItemUAN);
                existingMapping.ItemID = model.ItemID;
            }
            if (existingMapping.PLKindID != model.PLKindID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(PLKindID) PLKindName",
                    existingModel.PLKindID,
                    existingModel.PLKindName,
                    model.PLKindID,
                    model.PLKindName);
                existingMapping.PLKindID = model.PLKindID;
            }

            return logEntries;
        }
        protected override void AddMappingFunc(FinancialStatementContext db, ItemPLKindsViewModel model)
        {
            var newMapping = TinyMapper.Map<ItemPLKinds>(model);
            db.ItemPLKinds.Add(newMapping);
        }

        protected override async Task<ItemPLKinds> GetSingle(FinancialStatementContext db, long rowId)
        {
            return await db.ItemPLKinds.FirstOrDefaultAsync(p => p.RowId == rowId);
        }
        protected override IQueryable<ItemPLKindsViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId)
        {
            return from iplk in db.ItemPLKinds
                   from dimDept in db.DimDepts.Where(p => p.DeptID == iplk.DeptID).DefaultIfEmpty()
                   from dimItem in db.DimItems.Where(p => p.ItemID == iplk.ItemID).DefaultIfEmpty()
                   from dimPLKinds in db.DimPLKinds.Where(p => p.PLKindID == iplk.PLKindID).DefaultIfEmpty()
                   where iplk.RowId == rowId
                   select new ItemPLKindsViewModel
                   {
                       Dept = dimDept.Dept,
                       DeptID = iplk.DeptID,
                       ItemID = iplk.ItemID,
                       ItemUAN = dimItem.UAN,
                       PLKindID = iplk.PLKindID,
                       PLKindName = dimPLKinds.PLKindName
                   };
        }

        protected override string GetDetailsForLog(ItemPLKindsViewModel model)
        {
            return $"DeptID: {model.DeptID}; " +
                   $"Dept: {model.Dept}; " +
                   $"ItemID: {model.ItemID}; " +
                   $"ItemUAN: {model.ItemUAN}; " +
                   $"PLKindID: {model.PLKindID}; " +
                   $"PLKindName: {model.PLKindName}";
        }
    }
}
