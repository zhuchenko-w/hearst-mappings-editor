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
    public class DimYearRepository :
        BaseReferenceSimpleRepository<DimYear, DimYearFilter>, 
        IReferenceRepository<DimYear, DimYearFilter>
    {
        protected override string SingularEntityName => "DimYear";
        protected override string PluralEntityName => "DimYears";

        public override IQueryable<DimYear> GetListQuery(FinancialStatementContext db, DimYearFilter filter)
        {
            filter.YearDesc = filter.YearDesc?.Trim();
            filter.YearCode = filter.YearCode?.Trim();

            var consoSectionCodeFilterEmpty = string.IsNullOrEmpty(filter.YearCode);
            var consoSectionDescFilterEmpty = string.IsNullOrEmpty(filter.YearDesc);

            var query = db.DimYears.Where(dy =>
                (consoSectionCodeFilterEmpty || dy.YearCode != null && dy.YearCode.Contains(filter.YearCode))
                && (consoSectionDescFilterEmpty || dy.YearDesc != null && dy.YearDesc.Contains(filter.YearDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimYearSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.YearID) : query.OrderByDescending(p => p.YearID);
                        break;
                    case DimYearSortTypes.YearCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.YearCode) : query.OrderByDescending(p => p.YearCode);
                        break;
                    case DimYearSortTypes.YearDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.YearDesc) : query.OrderByDescending(p => p.YearDesc);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.YearID);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.YearID);
            }

            return query;
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimYear existingItem)
        {
            db.DimYears.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimYear item)
        {
            if (string.IsNullOrEmpty(item.YearCode))
            {
                throw new PublicException("YearCode can not be null or empty");
            }
            db.DimYears.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimYear existingItem, DimYear item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.YearCode != item.YearCode)
            {
                if (string.IsNullOrEmpty(item.YearCode))
                {
                    throw new PublicException("YearCode can not be null or empty");
                }
                AddPropertyChangeLogEntry(logEntries, item, "YearCode", existingItem.YearCode, item.YearCode);
                existingItem.YearCode = item.YearCode;
            }
            if (existingItem.YearDesc != item.YearDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "YearDesc", existingItem.YearDesc, item.YearDesc);
                existingItem.YearDesc = item.YearDesc;
            }

            return logEntries;
        }

        protected override async Task<DimYear> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimYears.FirstOrDefaultAsync(p => p.YearID == id);
        }

        protected override string GetDetailsForLog(DimYear model)
        {
            return $"YearID: {model.YearID}; " +
                   $"YearCode: {model.YearCode}; " +
                   $"YearDesc: {model.YearDesc}";
        }
    }
}
