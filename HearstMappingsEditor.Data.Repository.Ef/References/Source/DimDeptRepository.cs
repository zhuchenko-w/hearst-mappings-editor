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
    public class DimDeptRepository :
        BaseReferenceSimpleRepository<DimDept, DimDeptFilter>, 
        IReferenceRepository<DimDept, DimDeptFilter>
    {
        protected override string SingularEntityName => "DimDept";
        protected override string PluralEntityName => "DimDepts";

        public override IQueryable<DimDept> GetListQuery(FinancialStatementContext db, DimDeptFilter filter)
        {
            filter.Dept = filter.Dept?.Trim();
            filter.DeptDesc = filter.DeptDesc?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var deptFilterEmpty = string.IsNullOrEmpty(filter.Dept);
            var deptDescFilterEmpty = string.IsNullOrEmpty(filter.DeptDesc);

            var query = db.DimDepts.Where(dd =>
                (createDateFromFilterEmpty || dd.CreateDate.HasValue && dd.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || dd.CreateDate.HasValue && dd.CreateDate.Value <= filter.CreateDateTo.Value)
                && (deptFilterEmpty || dd.Dept != null && dd.Dept.Contains(filter.Dept))
                && (deptDescFilterEmpty || dd.DeptDesc != null && dd.DeptDesc.Contains(filter.DeptDesc)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimDeptSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.DeptID) : query.OrderByDescending(p => p.DeptID);
                        break;
                    case DimDeptSortTypes.Dept:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.Dept) : query.OrderByDescending(p => p.Dept);
                        break;
                    case DimDeptSortTypes.DeptDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.DeptDesc) : query.OrderByDescending(p => p.DeptDesc);
                        break;
                    case DimDeptSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimDept existingItem)
        {
            db.DimDepts.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimDept item)
        {
            item.CreateDate = DateTime.Now;
            db.DimDepts.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimDept existingItem, DimDept item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.Dept != item.Dept)
            {
                AddPropertyChangeLogEntry(logEntries, item, "Dept", existingItem.Dept, item.Dept);
                existingItem.Dept = item.Dept;
            }
            if (existingItem.DeptDesc != item.DeptDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "DeptDesc", existingItem.DeptDesc, item.DeptDesc);
                existingItem.DeptDesc = item.DeptDesc;
            }

            return logEntries;
        }

        protected override async Task<DimDept> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimDepts.FirstOrDefaultAsync(p => p.DeptID == id);
        }

        protected override string GetDetailsForLog(DimDept model)
        {
            return $"DeptID: {model.DeptID}; " +
                   $"Dept: {model.Dept}; " +
                   $"DeptDesc: {model.DeptDesc}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
