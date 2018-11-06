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
    public class DimCompanyRepository :
        BaseReferenceSimpleRepository<DimCompany, DimCompanyFilter>,
        IReferenceRepository<DimCompany, DimCompanyFilter>
    {
        protected override string SingularEntityName => "DimCompany";
        protected override string PluralEntityName => "DimCompanies";

        public override IQueryable<DimCompany> GetListQuery(FinancialStatementContext db, DimCompanyFilter filter)
        {
            filter.CompanyDesc = filter.CompanyDesc?.Trim();
            filter.CompanyCode = filter.CompanyCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var companyDescFilterEmpty = string.IsNullOrEmpty(filter.CompanyDesc);
            var companyCodeFilterEmpty = string.IsNullOrEmpty(filter.CompanyCode);

            var query = db.DimCompanies.Where(c =>
                (createDateFromFilterEmpty || c.CreateDate.HasValue && c.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || c.CreateDate.HasValue && c.CreateDate.Value <= filter.CreateDateTo.Value)
                && (companyDescFilterEmpty || c.CompanyDesc != null && c.CompanyDesc.Contains(filter.CompanyDesc))
                && (companyCodeFilterEmpty || c.CompanyCode != null && c.CompanyCode.Contains(filter.CompanyCode)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimCompanySortTypes.CompanyID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyID) : query.OrderByDescending(p => p.CompanyID);
                        break;
                    case DimCompanySortTypes.CompanyDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyDesc) : query.OrderByDescending(p => p.CompanyDesc);
                        break;
                    case DimCompanySortTypes.CompanyCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyCode) : query.OrderByDescending(p => p.CompanyCode);
                        break;
                    case DimCompanySortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimCompany existingItem)
        {
            db.DimCompanies.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimCompany item)
        {
            item.CreateDate = DateTime.Now;
            db.DimCompanies.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimCompany existingItem, DimCompany item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.CompanyCode != item.CompanyCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "CompanyCode", existingItem.CompanyCode, item.CompanyCode);
                existingItem.CompanyCode = item.CompanyCode;
            }
            if (existingItem.CompanyDesc != item.CompanyDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "CompanyDesc", existingItem.CompanyDesc, item.CompanyDesc);
                existingItem.CompanyDesc = item.CompanyDesc;
            }

            return logEntries;
        }

        protected override async Task<DimCompany> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimCompanies.FirstOrDefaultAsync(p => p.CompanyID == id);
        }

        protected override string GetDetailsForLog(DimCompany model)
        {
            return $"CompanyID: {model.CompanyID}; " +
                   $"CompanyDesc: {model.CompanyDesc}; " +
                   $"CompanyCode: {model.CompanyCode}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
