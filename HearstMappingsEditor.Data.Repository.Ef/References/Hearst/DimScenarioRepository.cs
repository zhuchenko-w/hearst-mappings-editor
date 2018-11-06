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
    public class DimScenarioRepository :
        BaseReferenceSimpleRepository<DimScenario, DimScenarioFilter>, 
        IReferenceRepository<DimScenario, DimScenarioFilter>
    {
        protected override string SingularEntityName => "DimScenario";
        protected override string PluralEntityName => "DimScenarios";

        public override IQueryable<DimScenario> GetListQuery(FinancialStatementContext db, DimScenarioFilter filter)
        {
            filter.ScenarioDesc = filter.ScenarioDesc?.Trim();
            filter.ScenarioCode = filter.ScenarioCode?.Trim();
            filter.InputCode = filter.InputCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var monthNumsFilterEmpty = filter.MonthNums == null || filter.MonthNums.Count == 0;
            var scenarioCodeFilterEmpty = string.IsNullOrEmpty(filter.ScenarioCode);
            var scenarioDescFilterEmpty = string.IsNullOrEmpty(filter.ScenarioDesc);
            var inputCodeFilterEmpty = string.IsNullOrEmpty(filter.InputCode);

            var query = db.DimScenarios.Where(ds =>
                (createDateFromFilterEmpty || ds.CreateDate.HasValue && ds.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || ds.CreateDate.HasValue && ds.CreateDate.Value <= filter.CreateDateTo.Value)
                && (monthNumsFilterEmpty || ds.MonthNum.HasValue && filter.MonthNums.Contains(ds.MonthNum.Value) || !ds.MonthNum.HasValue && filter.MonthNums.Contains(0))
                && (scenarioCodeFilterEmpty || ds.ScenarioCode != null && ds.ScenarioCode.Contains(filter.ScenarioCode))
                && (scenarioDescFilterEmpty || ds.ScenarioDesc != null && ds.ScenarioDesc.Contains(filter.ScenarioDesc))
                && (inputCodeFilterEmpty || ds.InputCode != null && ds.InputCode.Contains(filter.InputCode)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimScenarioSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ScenarioID) : query.OrderByDescending(p => p.ScenarioID);
                        break;
                    case DimScenarioSortTypes.ScenarioCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ScenarioCode) : query.OrderByDescending(p => p.ScenarioCode);
                        break;
                    case DimScenarioSortTypes.ScenarioDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ScenarioDesc) : query.OrderByDescending(p => p.ScenarioDesc);
                        break;
                    case DimScenarioSortTypes.InputCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.InputCode) : query.OrderByDescending(p => p.InputCode);
                        break;
                    case DimScenarioSortTypes.MonthNum:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.MonthNum) : query.OrderByDescending(p => p.MonthNum);
                        break;
                    case DimScenarioSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimScenario existingItem)
        {
            db.DimScenarios.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimScenario item)
        {
            item.CreateDate = DateTime.Now;
            db.DimScenarios.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimScenario existingItem, DimScenario item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.ScenarioCode != item.ScenarioCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ScenarioCode", existingItem.ScenarioCode, item.ScenarioCode);
                existingItem.ScenarioCode = item.ScenarioCode;
            }
            if (existingItem.ScenarioDesc != item.ScenarioDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ScenarioDesc", existingItem.ScenarioDesc, item.ScenarioDesc);
                existingItem.ScenarioDesc = item.ScenarioDesc;
            }
            if (existingItem.MonthNum != item.MonthNum)
            {
                AddPropertyChangeLogEntry(logEntries, item, "MonthNum", existingItem.MonthNum + "", item.MonthNum + "");
                existingItem.MonthNum = item.MonthNum;
            }
            if (existingItem.InputCode != item.InputCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "InputCode", existingItem.InputCode, item.InputCode);
                existingItem.InputCode = item.InputCode;
            }

            return logEntries;
        }

        protected override async Task<DimScenario> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimScenarios.FirstOrDefaultAsync(p => p.ScenarioID == id);
        }

        protected override string GetDetailsForLog(DimScenario model)
        {
            return $"ScenarioID: {model.ScenarioID}; " +
                   $"ScenarioCode: {model.ScenarioCode}; " +
                   $"ScenarioDesc: {model.ScenarioDesc}; " +
                   $"MonthNum: {model.MonthNum}; " +
                   $"InputCode: {model.InputCode}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
