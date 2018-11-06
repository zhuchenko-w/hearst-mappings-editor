using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class DimAccountGroupRepository :
        BaseReferenceSimpleRepository<DimAccountGroup, DimAccountGroupFilter>, 
        IReferenceRepository<DimAccountGroup, DimAccountGroupFilter>
    {
        protected override string SingularEntityName => "DimAccountGroup";
        protected override string PluralEntityName => "DimAccountGroups";

        public override IQueryable<DimAccountGroup> GetListQuery(FinancialStatementContext db, DimAccountGroupFilter filter)
        {
            filter.AccountGroupDesc = filter.AccountGroupDesc?.Trim();

            var accountGroupNameFilterEmpty = string.IsNullOrEmpty(filter.AccountGroupDesc);

            var query = db.DimAccountGroups.Where(dag => accountGroupNameFilterEmpty || dag.AccountGroupDesc != null && dag.AccountGroupDesc.Contains(filter.AccountGroupDesc));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimAccountGroupSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountGroupID) : query.OrderByDescending(p => p.AccountGroupID);
                        break;
                    case DimAccountGroupSortTypes.AccountGroupDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountGroupDesc) : query.OrderByDescending(p => p.AccountGroupDesc);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.AccountGroupID);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.AccountGroupID);
            }

            return query;
        }

        protected override void RemoveFunc(FinancialStatementContext db, DimAccountGroup existingItem)
        {
            db.DimAccountGroups.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimAccountGroup item)
        {
            db.DimAccountGroups.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimAccountGroup existingItem, DimAccountGroup item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.AccountGroupDesc != item.AccountGroupDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AccountGroupDesc", existingItem.AccountGroupDesc, item.AccountGroupDesc);
                existingItem.AccountGroupDesc = item.AccountGroupDesc;
            }

            return logEntries;
        }

        protected override async Task<DimAccountGroup> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimAccountGroups.FirstOrDefaultAsync(p => p.AccountGroupID == id);
        }

        protected override string GetDetailsForLog(DimAccountGroup model)
        {
            return $"AccountGroupID: {model.AccountGroupID}; " +
                   $"AccountGroupDesc: {model.AccountGroupDesc}";
        }
    }
}
