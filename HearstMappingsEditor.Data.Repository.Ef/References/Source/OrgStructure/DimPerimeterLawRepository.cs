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
    public class DimPerimeterLawRepository :
        BaseReferenceSimpleRepository<DimPerimeterLaw, DimPerimeterLawFilter>,
        IReferenceRepository<DimPerimeterLaw, DimPerimeterLawFilter>
    {
        protected override string SingularEntityName => "DimPerimeterLaw";
        protected override string PluralEntityName => "DimPerimeterLaws";

        public override IQueryable<DimPerimeterLaw> GetListQuery(FinancialStatementContext db, DimPerimeterLawFilter filter)
        {
            filter.PerimeterLawDesc = filter.PerimeterLawDesc?.Trim();
            filter.PerimeterLawCode = filter.PerimeterLawCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var perimeterLawDescFilterEmpty = string.IsNullOrEmpty(filter.PerimeterLawDesc);
            var perimeterLawCodeFilterEmpty = string.IsNullOrEmpty(filter.PerimeterLawCode);

            var query = db.DimPerimeterLaws.Where(pl =>
                (createDateFromFilterEmpty || pl.CreateDate.HasValue && pl.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || pl.CreateDate.HasValue && pl.CreateDate.Value <= filter.CreateDateTo.Value)
                && (perimeterLawDescFilterEmpty || pl.PerimeterLawDesc != null && pl.PerimeterLawDesc.Contains(filter.PerimeterLawDesc))
                && (perimeterLawCodeFilterEmpty || pl.PerimeterLawCode != null && pl.PerimeterLawCode.Contains(filter.PerimeterLawCode)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimPerimeterLawSortTypes.PerimeterLawID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawID) : query.OrderByDescending(p => p.PerimeterLawID);
                        break;
                    case DimPerimeterLawSortTypes.PerimeterLawDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawDesc) : query.OrderByDescending(p => p.PerimeterLawDesc);
                        break;
                    case DimPerimeterLawSortTypes.PerimeterLawCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawCode) : query.OrderByDescending(p => p.PerimeterLawCode);
                        break;
                    case DimPerimeterLawSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimPerimeterLaw existingItem)
        {
            db.DimPerimeterLaws.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimPerimeterLaw item)
        {
            item.CreateDate = DateTime.Now;
            db.DimPerimeterLaws.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimPerimeterLaw existingItem, DimPerimeterLaw item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.PerimeterLawCode != item.PerimeterLawCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterLawCode", existingItem.PerimeterLawCode, item.PerimeterLawCode);
                existingItem.PerimeterLawCode = item.PerimeterLawCode;
            }
            if (existingItem.PerimeterLawDesc != item.PerimeterLawDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterLawDesc", existingItem.PerimeterLawDesc, item.PerimeterLawDesc);
                existingItem.PerimeterLawDesc = item.PerimeterLawDesc;
            }

            return logEntries;
        }

        protected override async Task<DimPerimeterLaw> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimPerimeterLaws.FirstOrDefaultAsync(p => p.PerimeterLawID == id);
        }

        protected override string GetDetailsForLog(DimPerimeterLaw model)
        {
            return $"PerimeterLawID: {model.PerimeterLawID}; " +
                   $"PerimeterLawDesc: {model.PerimeterLawDesc}; " +
                   $"PerimeterLawCode: {model.PerimeterLawCode}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
