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
    public class DimAllOrgStructureRepository :
        BaseReferenceSimpleRepository<DimAllOrgStructure, DimAllOrgStructureFilter>,
        IReferenceRepository<DimAllOrgStructure, DimAllOrgStructureFilter>
    {
        protected override string SingularEntityName => "DimAllOrgStructure";
        protected override string PluralEntityName => "DimAllOrgStructures";

        public override IQueryable<DimAllOrgStructure> GetListQuery(FinancialStatementContext db, DimAllOrgStructureFilter filter)
        {
            filter.AllOrgStructure = filter.AllOrgStructure?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var allOrgStructureFilterEmpty = string.IsNullOrEmpty(filter.AllOrgStructure);

            var query = db.DimAllOrgStructures.Where(aos =>
                (createDateFromFilterEmpty || aos.CreateDate.HasValue && aos.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || aos.CreateDate.HasValue && aos.CreateDate.Value <= filter.CreateDateTo.Value)
                && (allOrgStructureFilterEmpty || aos.AllOrgStructure != null && aos.AllOrgStructure.Contains(filter.AllOrgStructure)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimAllOrgStructureSortTypes.AllOrgStructureID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AllOrgStructureID) : query.OrderByDescending(p => p.AllOrgStructureID);
                        break;
                    case DimAllOrgStructureSortTypes.AllOrgStructure:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AllOrgStructure) : query.OrderByDescending(p => p.AllOrgStructure);
                        break;
                    case DimAllOrgStructureSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimAllOrgStructure existingItem)
        {
            db.DimAllOrgStructures.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimAllOrgStructure item)
        {
            item.CreateDate = DateTime.Now;
            db.DimAllOrgStructures.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimAllOrgStructure existingItem, DimAllOrgStructure item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.AllOrgStructure != item.AllOrgStructure)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AllOrgStructure", existingItem.AllOrgStructure, item.AllOrgStructure);
                existingItem.AllOrgStructure = item.AllOrgStructure;
            }

            return logEntries;
        }

        protected override async Task<DimAllOrgStructure> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimAllOrgStructures.FirstOrDefaultAsync(p => p.AllOrgStructureID == id);
        }

        protected override string GetDetailsForLog(DimAllOrgStructure model)
        {
            return $"AllOrgStructureID: {model.AllOrgStructureID}; " +
                   $"AllOrgStructure: {model.AllOrgStructure}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
