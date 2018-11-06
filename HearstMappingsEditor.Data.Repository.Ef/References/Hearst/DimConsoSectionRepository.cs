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
    public class DimConsoSectionRepository :
        BaseReferenceSimpleRepository<DimConsoSection, DimConsoSectionFilter>, 
        IReferenceRepository<DimConsoSection, DimConsoSectionFilter>
    {
        protected override string SingularEntityName => "DimConsoSection";
        protected override string PluralEntityName => "DimConsoSections";

        public override IQueryable<DimConsoSection> GetListQuery(FinancialStatementContext db, DimConsoSectionFilter filter)
        {
            filter.ConsoSectionDesc = filter.ConsoSectionDesc?.Trim();
            filter.ConsoSectionCode = filter.ConsoSectionCode?.Trim();

            var consoSectionCodeFilterEmpty = string.IsNullOrEmpty(filter.ConsoSectionCode);
            var consoSectionDescFilterEmpty = string.IsNullOrEmpty(filter.ConsoSectionDesc);

            var query = db.DimConsoSections.Where(dcs =>
                (consoSectionCodeFilterEmpty || dcs.ConsoSectionCode != null && dcs.ConsoSectionCode.Contains(filter.ConsoSectionCode))
                && (consoSectionDescFilterEmpty || dcs.ConsoSectionDesc != null && dcs.ConsoSectionDesc.Contains(filter.ConsoSectionDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimConsoSectionSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ConsoSectionID) : query.OrderByDescending(p => p.ConsoSectionID);
                        break;
                    case DimConsoSectionSortTypes.ConsoSectionCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ConsoSectionCode) : query.OrderByDescending(p => p.ConsoSectionCode);
                        break;
                    case DimConsoSectionSortTypes.ConsoSectionDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ConsoSectionDesc) : query.OrderByDescending(p => p.ConsoSectionDesc);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.ConsoSectionID);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.ConsoSectionID);
            }

            return query;
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimConsoSection existingItem)
        {
            db.DimConsoSections.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimConsoSection item)
        {
            db.DimConsoSections.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimConsoSection existingItem, DimConsoSection item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.ConsoSectionCode != item.ConsoSectionCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ConsoSectionCode", existingItem.ConsoSectionCode, item.ConsoSectionCode);
                existingItem.ConsoSectionCode = item.ConsoSectionCode;
            }
            if (existingItem.ConsoSectionDesc != item.ConsoSectionDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ConsoSectionDesc", existingItem.ConsoSectionDesc, item.ConsoSectionDesc);
                existingItem.ConsoSectionDesc = item.ConsoSectionDesc;
            }

            return logEntries;
        }

        protected override async Task<DimConsoSection> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimConsoSections.FirstOrDefaultAsync(p => p.ConsoSectionID == id);
        }

        protected override string GetDetailsForLog(DimConsoSection model)
        {
            return $"ConsoSectionID: {model.ConsoSectionID}; " +
                   $"ConsoSectionCode: {model.ConsoSectionCode}; " +
                   $"ConsoSectionDesc: {model.ConsoSectionDesc}";
        }
    }
}
