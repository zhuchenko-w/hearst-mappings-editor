using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class DimItemRepository :
        BaseReferenceSimpleRepository<DimItem, DimItemFilter>, 
        IReferenceRepository<DimItem, DimItemFilter>
    {
        protected override string SingularEntityName => "DimItem";
        protected override string PluralEntityName => "DimItems";

        public override IQueryable<DimItem> GetListQuery(FinancialStatementContext db, DimItemFilter filter)
        {
            filter.Ic3p = filter.Ic3p?.Trim();
            filter.WGO = filter.WGO?.Trim();
            filter.UAN = filter.UAN?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var itemSignsFilterEmpty = filter.ItemSigns == null || filter.ItemSigns.Count == 0;
            var signMRsFilterEmpty = filter.SignMRs == null || filter.SignMRs.Count == 0;
            var ic3pFilterEmpty = string.IsNullOrEmpty(filter.Ic3p);
            var wgoFilterEmpty = string.IsNullOrEmpty(filter.WGO);
            var uanFilterEmpty = string.IsNullOrEmpty(filter.UAN);

            var query = db.DimItems.Where(di =>
                (createDateFromFilterEmpty || di.CreateDate.HasValue && di.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || di.CreateDate.HasValue && di.CreateDate.Value <= filter.CreateDateTo.Value)
                && (itemSignsFilterEmpty || di.ItemSign.HasValue && filter.ItemSigns.Contains(di.ItemSign.Value))
                && (signMRsFilterEmpty || di.SignMR.HasValue && filter.SignMRs.Contains(di.SignMR.Value))
                && (uanFilterEmpty || di.UAN != null && di.UAN.Contains(filter.UAN))
                && (wgoFilterEmpty || di.WGO != null && di.WGO.Contains(filter.WGO))
                && (ic3pFilterEmpty || di.Ic3p != null && di.Ic3p.Contains(filter.Ic3p)));

            //if (!string.IsNullOrEmpty(filter.Ic3p))
            //{
            //    query = query.Where("Ic3p LIKE @searchTerm", new ObjectParameter("searchTerm", GetStringContainedFilterPattern(filter.Ic3p)));
            //    //.Where(p => SqlMethods.Like(p.Ic3p, GetStringContainedFilterPattern(filter.Ic3p), '/'));
            //}
            //if (!string.IsNullOrEmpty(filter.WGO))
            //{
            //    query = query.Where(p => SqlMethods.Like(p.WGO, GetStringContainedFilterPattern(filter.WGO), '/'));
            //}
            //if (!string.IsNullOrEmpty(filter.UAN))
            //{
            //    query = query.Where(p => SqlMethods.Like(p.UAN, GetStringContainedFilterPattern(filter.UAN), '/'));
            //}

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimItemSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ItemID) : query.OrderByDescending(p => p.ItemID);
                        break;
                    case DimItemSortTypes.Ic3p:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.Ic3p) : query.OrderByDescending(p => p.Ic3p);
                        break;
                    case DimItemSortTypes.UAN:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.UAN) : query.OrderByDescending(p => p.UAN);
                        break;
                    case DimItemSortTypes.WGO:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.WGO) : query.OrderByDescending(p => p.WGO);
                        break;
                    case DimItemSortTypes.ItemSign:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ItemSign) : query.OrderByDescending(p => p.ItemSign);
                        break;
                    case DimItemSortTypes.SignMR:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.SignMR) : query.OrderByDescending(p => p.SignMR);
                        break;
                    case DimItemSortTypes.CreateDate:
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

            return query;
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimItem existingItem)
        {
            db.DimItems.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimItem item)
        {
            if (string.IsNullOrEmpty(item.UAN))
            {
                throw new PublicException("UAN can not be null or empty");
            }
            item.CreateDate = DateTime.Now;
            db.DimItems.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimItem existingItem, DimItem item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.Ic3p != item.Ic3p)
            {
                AddPropertyChangeLogEntry(logEntries, item, "Ic3p", existingItem.Ic3p, item.Ic3p);
                existingItem.Ic3p = item.Ic3p;
            }
            if (existingItem.ItemSign != item.ItemSign)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ItemSign", existingItem.ItemSign + "", item.ItemSign + "");
                existingItem.ItemSign = item.ItemSign;
            }
            if (existingItem.SignMR != item.SignMR)
            {
                AddPropertyChangeLogEntry(logEntries, item, "SignMR", existingItem.SignMR + "", item.SignMR + "");
                existingItem.SignMR = item.SignMR;
            }
            if (existingItem.UAN != item.UAN)
            {
                if (string.IsNullOrEmpty(item.UAN))
                {
                    throw new PublicException("UAN can not be null or empty");
                }
                AddPropertyChangeLogEntry(logEntries, item, "UAN", existingItem.UAN, item.UAN);
                existingItem.UAN = item.UAN;
            }
            if (existingItem.WGO != item.WGO)
            {
                AddPropertyChangeLogEntry(logEntries, item, "WGO", existingItem.WGO, item.WGO);
                existingItem.WGO = item.WGO;
            }

            return logEntries;
        }

        protected override async Task<DimItem> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimItems.FirstOrDefaultAsync(p => p.ItemID == id);
        }

        protected override string GetDetailsForLog(DimItem model)
        {
            return $"ItemID: {model.ItemID}; " +
                   $"UAN: {model.UAN}; " +
                   $"WGO: {model.WGO}; " +
                   $"Ic3p: {model.Ic3p}; " +
                   $"ItemSign: {model.ItemSign}; " +
                   $"SignMR: {model.SignMR}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
