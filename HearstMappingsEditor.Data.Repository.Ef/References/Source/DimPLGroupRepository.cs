using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class DimPLGroupRepository :
        BaseReferenceSimpleRepository<DimPLGroup, DimPLGroupFilter>, 
        IReferenceRepository<DimPLGroup, DimPLGroupFilter>
    {
        protected override string SingularEntityName => "DimPLGroup";
        protected override string PluralEntityName => "DimPLGroups";

        public override IQueryable<DimPLGroup> GetListQuery(FinancialStatementContext db, DimPLGroupFilter filter)
        {
            filter.PLGroupName = filter.PLGroupName?.Trim();

            var plGroupNameFilterEmpty = string.IsNullOrEmpty(filter.PLGroupName);

            var query = db.DimPLGroups.Where(dplg => plGroupNameFilterEmpty || dplg.PLGroupName != null && dplg.PLGroupName.Contains(filter.PLGroupName));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimPLGroupSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLGroupID) : query.OrderByDescending(p => p.PLGroupID);
                        break;
                    case DimPLGroupSortTypes.PLGroupName:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PLGroupName) : query.OrderByDescending(p => p.PLGroupName);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.PLGroupID);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.PLGroupID);
            }

            return query;
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimPLGroup existingItem)
        {
            db.DimPLGroups.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimPLGroup item)
        {
            db.DimPLGroups.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimPLGroup existingItem, DimPLGroup item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.PLGroupName != item.PLGroupName)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PLGroupName", existingItem.PLGroupName, item.PLGroupName);
                existingItem.PLGroupName = item.PLGroupName;
            }

            return logEntries;
        }

        protected override async Task<DimPLGroup> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimPLGroups.FirstOrDefaultAsync(p => p.PLGroupID == id);
        }

        protected override string GetDetailsForLog(DimPLGroup model)
        {
            return $"PLGroupID: {model.PLGroupID}; " +
                   $"PLGroupName: {model.PLGroupName}";
        }
    }
}
