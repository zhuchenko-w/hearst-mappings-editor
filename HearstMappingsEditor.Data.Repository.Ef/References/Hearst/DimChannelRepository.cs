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
    public class DimChannelRepository :
        BaseReferenceSimpleRepository<DimChannel, DimChannelFilter>, 
        IReferenceRepository<DimChannel, DimChannelFilter>
    {
        protected override string SingularEntityName => "DimChannel";
        protected override string PluralEntityName => "DimChannels";

        public override IQueryable<DimChannel> GetListQuery(FinancialStatementContext db, DimChannelFilter filter)
        {
            filter.ChannelDesc = filter.ChannelDesc?.Trim();
            filter.ChannelCode = filter.ChannelCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var productCodeFilterEmpty = string.IsNullOrEmpty(filter.ChannelCode);
            var productDescFilterEmpty = string.IsNullOrEmpty(filter.ChannelDesc);

            var query = db.DimChannels.Where(dc =>
                (createDateFromFilterEmpty || dc.CreateDate.HasValue && dc.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || dc.CreateDate.HasValue && dc.CreateDate.Value <= filter.CreateDateTo.Value)
                && (productCodeFilterEmpty || dc.ChannelCode != null && dc.ChannelCode.Contains(filter.ChannelCode))
                && (productDescFilterEmpty || dc.ChannelDesc != null && dc.ChannelDesc.Contains(filter.ChannelDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimChannelSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ChannelID) : query.OrderByDescending(p => p.ChannelID);
                        break;
                    case DimChannelSortTypes.ChannelCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ChannelCode) : query.OrderByDescending(p => p.ChannelCode);
                        break;
                    case DimChannelSortTypes.ChannelDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ChannelDesc) : query.OrderByDescending(p => p.ChannelDesc);
                        break;
                    case DimChannelSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimChannel existingItem)
        {
            db.DimChannels.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimChannel item)
        {
            item.CreateDate = DateTime.Now;
            db.DimChannels.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimChannel existingItem, DimChannel item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.ChannelCode != item.ChannelCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ChannelCode", existingItem.ChannelCode, item.ChannelCode);
                existingItem.ChannelCode = item.ChannelCode;
            }
            if (existingItem.ChannelDesc != item.ChannelDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ChannelDesc", existingItem.ChannelDesc, item.ChannelDesc);
                existingItem.ChannelDesc = item.ChannelDesc;
            }

            return logEntries;
        }

        protected override async Task<DimChannel> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimChannels.FirstOrDefaultAsync(p => p.ChannelID == id);
        }

        protected override string GetDetailsForLog(DimChannel model)
        {
            return $"ChannelID: {model.ChannelID}; " +
                   $"ChannelCode: {model.ChannelCode}; " +
                   $"ChannelDesc: {model.ChannelDesc}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
