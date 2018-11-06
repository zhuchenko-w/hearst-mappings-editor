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
    public class DimPerimeterRepository :
        BaseReferenceSimpleRepository<DimPerimeter, DimPerimeterFilter>,
        IReferenceRepository<DimPerimeter, DimPerimeterFilter>
    {
        protected override string SingularEntityName => "DimPerimeter";
        protected override string PluralEntityName => "DimPerimeters";

        public override IQueryable<DimPerimeter> GetListQuery(FinancialStatementContext db, DimPerimeterFilter filter)
        {
            filter.PerimeterDesc = filter.PerimeterDesc?.Trim();
            filter.PerimeterCode = filter.PerimeterCode?.Trim();
            filter.PerimeterCurrency = filter.PerimeterCurrency?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var perimeterDescFilterEmpty = string.IsNullOrEmpty(filter.PerimeterDesc);
            var perimeterCodeFilterEmpty = string.IsNullOrEmpty(filter.PerimeterCode);
            var perimeterCurrencyFilterEmpty = string.IsNullOrEmpty(filter.PerimeterCurrency);

            var query = db.DimPerimeters.Where(p =>
                (createDateFromFilterEmpty || p.CreateDate.HasValue && p.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || p.CreateDate.HasValue && p.CreateDate.Value <= filter.CreateDateTo.Value)
                && (perimeterDescFilterEmpty || p.PerimeterDesc != null && p.PerimeterDesc.Contains(filter.PerimeterDesc))
                && (perimeterCodeFilterEmpty || p.PerimeterCode != null && p.PerimeterCode.Contains(filter.PerimeterCode))
                && (perimeterCurrencyFilterEmpty || p.PerimeterCurrency != null && p.PerimeterCurrency.Contains(filter.PerimeterCurrency)));
            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimPerimeterSortTypes.PerimeterID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterID) : query.OrderByDescending(p => p.PerimeterID);
                        break;
                    case DimPerimeterSortTypes.PerimeterDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterDesc) : query.OrderByDescending(p => p.PerimeterDesc);
                        break;
                    case DimPerimeterSortTypes.PerimeterCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterCode) : query.OrderByDescending(p => p.PerimeterCode);
                        break;
                    case DimPerimeterSortTypes.PerimeterCurrency:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterCurrency) : query.OrderByDescending(p => p.PerimeterCurrency);
                        break;
                    case DimPerimeterSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimPerimeter existingItem)
        {
            db.DimPerimeters.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimPerimeter item)
        {
            item.CreateDate = DateTime.Now;
            db.DimPerimeters.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimPerimeter existingItem, DimPerimeter item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.PerimeterCode != item.PerimeterCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterCode", existingItem.PerimeterCode, item.PerimeterCode);
                existingItem.PerimeterCode = item.PerimeterCode;
            }
            if (existingItem.PerimeterCurrency != item.PerimeterCurrency)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterCurrency", existingItem.PerimeterCurrency, item.PerimeterCurrency);
                existingItem.PerimeterCurrency = item.PerimeterCurrency;
            }
            if (existingItem.PerimeterDesc != item.PerimeterDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterDesc", existingItem.PerimeterDesc, item.PerimeterDesc);
                existingItem.PerimeterDesc = item.PerimeterDesc;
            }

            return logEntries;
        }

        protected override async Task<DimPerimeter> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimPerimeters.FirstOrDefaultAsync(p => p.PerimeterID == id);
        }

        protected override string GetDetailsForLog(DimPerimeter model)
        {
            return $"PerimeterID: {model.PerimeterID}; " +
                   $"PerimeterDesc: {model.PerimeterDesc}; " +
                   $"PerimeterCode: {model.PerimeterCode}; " +
                   $"PerimeterCurrency: {model.PerimeterCurrency}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
