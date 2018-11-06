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
    public class DimAccountRepository :
        BaseReferenceSimpleRepository<DimAccount, DimAccountFilter>, 
        IReferenceRepository<DimAccount, DimAccountFilter>
    {
        protected override string SingularEntityName => "DimAccount";
        protected override string PluralEntityName => "DimAccounts";

        public override IQueryable<DimAccount> GetListQuery(FinancialStatementContext db, DimAccountFilter filter)
        {
            filter.AccountDesc = filter.AccountDesc?.Trim();
            filter.AccountCode = filter.AccountCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var accountCodeFilterEmpty = string.IsNullOrEmpty(filter.AccountCode);
            var accountDescFilterEmpty = string.IsNullOrEmpty(filter.AccountDesc);

            var query = db.DimAccounts.Where(da =>
                (createDateFromFilterEmpty || da.CreateDate.HasValue && da.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || da.CreateDate.HasValue && da.CreateDate.Value <= filter.CreateDateTo.Value)
                && (accountCodeFilterEmpty || da.AccountCode != null && da.AccountCode.Contains(filter.AccountCode))
                && (accountDescFilterEmpty || da.AccountDesc != null && da.AccountDesc.Contains(filter.AccountDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimAccountSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountID) : query.OrderByDescending(p => p.AccountID);
                        break;
                    case DimAccountSortTypes.AccountCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountCode) : query.OrderByDescending(p => p.AccountCode);
                        break;
                    case DimAccountSortTypes.AccountDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountDesc) : query.OrderByDescending(p => p.AccountDesc);
                        break;
                    case DimAccountSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimAccount existingItem)
        {
            db.DimAccounts.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimAccount item)
        {
            item.CreateDate = DateTime.Now;
            db.DimAccounts.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimAccount existingItem, DimAccount item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.AccountCode != item.AccountCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AccountCode", existingItem.AccountCode, item.AccountCode);
                existingItem.AccountCode = item.AccountCode;
            }
            if (existingItem.AccountDesc != item.AccountDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AccountDesc", existingItem.AccountDesc, item.AccountDesc);
                existingItem.AccountDesc = item.AccountDesc;
            }

            return logEntries;
        }

        protected override async Task<DimAccount> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimAccounts.FirstOrDefaultAsync(p => p.AccountID == id);
        }

        protected override string GetDetailsForLog(DimAccount model)
        {
            return $"AccountID: {model.AccountID}; " +
                   $"AccountCode: {model.AccountCode}; " +
                   $"AccountDesc: {model.AccountDesc}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
