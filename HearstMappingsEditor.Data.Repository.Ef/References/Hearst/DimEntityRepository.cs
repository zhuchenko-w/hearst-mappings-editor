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
    public class DimEntityRepository :
        BaseReferenceSimpleRepository<DimEntity, DimEntityFilter>, 
        IReferenceRepository<DimEntity, DimEntityFilter>
    {
        protected override string SingularEntityName => "DimEntity";
        protected override string PluralEntityName => "DimEntities";

        public override IQueryable<DimEntity> GetListQuery(FinancialStatementContext db, DimEntityFilter filter)
        {
            filter.EntityDesc = filter.EntityDesc?.Trim();
            filter.EntityCode = filter.EntityCode?.Trim();
            filter.EntityCurrency = filter.EntityCurrency?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var entityCodeFilterEmpty = string.IsNullOrEmpty(filter.EntityCode);
            var entityDescFilterEmpty = string.IsNullOrEmpty(filter.EntityDesc);
            var entityCurrencyFilterEmpty = string.IsNullOrEmpty(filter.EntityCurrency);

            var query = db.DimEntities.Where(da =>
                (createDateFromFilterEmpty || da.CreateDate.HasValue && da.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || da.CreateDate.HasValue && da.CreateDate.Value <= filter.CreateDateTo.Value)
                && (entityCodeFilterEmpty || da.EntityCode != null && da.EntityCode.Contains(filter.EntityCode))
                && (entityDescFilterEmpty || da.EntityDesc != null && da.EntityDesc.Contains(filter.EntityDesc))
                && (entityCurrencyFilterEmpty || da.EntityCurrency != null && da.EntityCurrency.Contains(filter.EntityCurrency)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimEntitySortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.EntityID) : query.OrderByDescending(p => p.EntityID);
                        break;
                    case DimEntitySortTypes.EntityCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.EntityCode) : query.OrderByDescending(p => p.EntityCode);
                        break;
                    case DimEntitySortTypes.EntityDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.EntityDesc) : query.OrderByDescending(p => p.EntityDesc);
                        break;
                    case DimEntitySortTypes.EntityCurrency:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.EntityCurrency) : query.OrderByDescending(p => p.EntityCurrency);
                        break;
                    case DimEntitySortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimEntity existingItem)
        {
            db.DimEntities.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimEntity item)
        {
            item.CreateDate = DateTime.Now;
            db.DimEntities.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimEntity existingItem, DimEntity item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.EntityCode != item.EntityCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "EntityCode", existingItem.EntityCode, item.EntityCode);
                existingItem.EntityCode = item.EntityCode;
            }
            if (existingItem.EntityDesc != item.EntityDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "EntityDesc", existingItem.EntityDesc, item.EntityDesc);
                existingItem.EntityDesc = item.EntityDesc;
            }
            if (existingItem.EntityCurrency != item.EntityCurrency)
            {
                AddPropertyChangeLogEntry(logEntries, item, "EntityCurrency", existingItem.EntityCurrency, item.EntityCurrency);
                existingItem.EntityCurrency = item.EntityCurrency;
            }

            return logEntries;
        }

        protected override async Task<DimEntity> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimEntities.FirstOrDefaultAsync(p => p.EntityID == id);
        }

        protected override string GetDetailsForLog(DimEntity model)
        {
            return $"EntityID: {model.EntityID}; " +
                   $"EntityCode: {model.EntityCode}; " +
                   $"EntityDesc: {model.EntityDesc}; " +
                   $"EntityCurrency: {model.EntityCurrency}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
