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
    public class DimProductRepository :
        BaseReferenceSimpleRepository<DimProduct, DimProductFilter>, 
        IReferenceRepository<DimProduct, DimProductFilter>
    {
        protected override string SingularEntityName => "DimProduct";
        protected override string PluralEntityName => "DimProducts";

        public override IQueryable<DimProduct> GetListQuery(FinancialStatementContext db, DimProductFilter filter)
        {
            filter.ProductDesc = filter.ProductDesc?.Trim();
            filter.ProductCode = filter.ProductCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var productCodeFilterEmpty = string.IsNullOrEmpty(filter.ProductCode);
            var productDescFilterEmpty = string.IsNullOrEmpty(filter.ProductDesc);

            var query = db.DimProducts.Where(dp =>
                (createDateFromFilterEmpty || dp.CreateDate.HasValue && dp.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || dp.CreateDate.HasValue && dp.CreateDate.Value <= filter.CreateDateTo.Value)
                && (productCodeFilterEmpty || dp.ProductCode != null && dp.ProductCode.Contains(filter.ProductCode))
                && (productDescFilterEmpty || dp.ProductDesc != null && dp.ProductDesc.Contains(filter.ProductDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimProductSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProductID) : query.OrderByDescending(p => p.ProductID);
                        break;
                    case DimProductSortTypes.ProductCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProductCode) : query.OrderByDescending(p => p.ProductCode);
                        break;
                    case DimProductSortTypes.ProductDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProductDesc) : query.OrderByDescending(p => p.ProductDesc);
                        break;
                    case DimProductSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimProduct existingItem)
        {
            db.DimProducts.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimProduct item)
        {
            item.CreateDate = DateTime.Now;
            db.DimProducts.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimProduct existingItem, DimProduct item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.ProductCode != item.ProductCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ProductCode", existingItem.ProductCode, item.ProductCode);
                existingItem.ProductCode = item.ProductCode;
            }
            if (existingItem.ProductDesc != item.ProductDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ProductDesc", existingItem.ProductDesc, item.ProductDesc);
                existingItem.ProductDesc = item.ProductDesc;
            }

            return logEntries;
        }

        protected override async Task<DimProduct> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimProducts.FirstOrDefaultAsync(p => p.ProductID == id);
        }

        protected override string GetDetailsForLog(DimProduct model)
        {
            return $"ProductID: {model.ProductID}; " +
                   $"ProductCode: {model.ProductCode}; " +
                   $"ProductDesc: {model.ProductDesc}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
