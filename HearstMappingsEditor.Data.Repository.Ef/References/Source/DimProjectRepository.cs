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
    public class DimProjectRepository :
        BaseReferenceSimpleRepository<DimProject, DimProjectFilter>, 
        IReferenceRepository<DimProject, DimProjectFilter>
    {
        protected override string SingularEntityName => "DimProject";
        protected override string PluralEntityName => "DimProjects";

        public override IQueryable<DimProject> GetListQuery(FinancialStatementContext db, DimProjectFilter filter)
        {
            filter.C1HypCode = filter.C1HypCode?.Trim();
            filter.C2HypCodeNew = filter.C2HypCodeNew?.Trim();
            filter.C2Management = filter.C2Management?.Trim();
            filter.Description = filter.Description?.Trim();
            filter.ManagementBrand = filter.ManagementBrand?.Trim();
            filter.ManagementParent = filter.ManagementParent?.Trim();
            filter.ManagementProject = filter.ManagementProject?.Trim();
            filter.ProjectCode = filter.ProjectCode?.Trim();
            filter.Type = filter.Type?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var c1HypCodeFilterEmpty = string.IsNullOrEmpty(filter.C1HypCode);
            var c2HypCodeNewFilterEmpty = string.IsNullOrEmpty(filter.C2HypCodeNew);
            var c2ManagementFilterEmpty = string.IsNullOrEmpty(filter.C2Management);
            var descriptionFilterEmpty = string.IsNullOrEmpty(filter.Description);
            var managementBrandFilterEmpty = string.IsNullOrEmpty(filter.ManagementBrand);
            var managementParentFilterEmpty = string.IsNullOrEmpty(filter.ManagementParent);
            var managementProjectFilterEmpty = string.IsNullOrEmpty(filter.ManagementProject);
            var projectCodeFilterEmpty = string.IsNullOrEmpty(filter.ProjectCode);
            var typeFilterEmpty = string.IsNullOrEmpty(filter.Type);
            var projectGroupFilterEmpty = filter.ProjectGroups == null || filter.ProjectGroups.Count == 0;
            var printDigitalFilterEmpty = filter.PrintDigitals == null || filter.PrintDigitals.Count == 0;

            var query = db.DimProjects.Where(dp =>
                (createDateFromFilterEmpty || dp.CreateDate.HasValue && dp.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || dp.CreateDate.HasValue && dp.CreateDate.Value <= filter.CreateDateTo.Value)
                && (printDigitalFilterEmpty || dp.PrintDigital != null && filter.PrintDigitals.Contains(dp.PrintDigital))
                && (projectGroupFilterEmpty || dp.ProjectGroup != null && filter.ProjectGroups.Contains(dp.ProjectGroup) || dp.ProjectGroup == null && filter.ProjectGroups.Any(p=>string.IsNullOrEmpty(p)))
                && (c1HypCodeFilterEmpty || dp.C1HypCode != null && dp.C1HypCode.Contains(filter.C1HypCode))
                && (c2HypCodeNewFilterEmpty || dp.C2HypCodeNew != null && dp.C2HypCodeNew.Contains(filter.C2HypCodeNew))
                && (c2ManagementFilterEmpty || dp.C2Management != null && dp.C2Management.Contains(filter.C2Management))
                && (descriptionFilterEmpty || dp.Description != null && dp.Description.Contains(filter.Description))
                && (managementBrandFilterEmpty || dp.ManagementBrand != null && dp.ManagementBrand.Contains(filter.ManagementBrand))
                && (managementParentFilterEmpty || dp.ManagementParent != null && dp.ManagementParent.Contains(filter.ManagementParent))
                && (managementProjectFilterEmpty || dp.ManagementProject != null && dp.ManagementProject.Contains(filter.ManagementProject))
                && (projectCodeFilterEmpty || dp.ProjectCode != null && dp.ProjectCode.Contains(filter.ProjectCode))
                && (typeFilterEmpty || dp.Type != null && dp.Type.Contains(filter.Type)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimProjectSortTypes.Id:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProjectID) : query.OrderByDescending(p => p.ProjectID);
                        break;
                    case DimProjectSortTypes.ProjectCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProjectCode) : query.OrderByDescending(p => p.ProjectCode);
                        break;
                    case DimProjectSortTypes.ProjectGroup:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProjectGroup) : query.OrderByDescending(p => p.ProjectGroup);
                        break;
                    case DimProjectSortTypes.ManagementProject:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ManagementProject) : query.OrderByDescending(p => p.ManagementProject);
                        break;
                    case DimProjectSortTypes.ManagementParent:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ManagementParent) : query.OrderByDescending(p => p.ManagementParent);
                        break;
                    case DimProjectSortTypes.ManagementBrand:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.ManagementBrand) : query.OrderByDescending(p => p.ManagementBrand);
                        break;
                    case DimProjectSortTypes.PrintDigital:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PrintDigital) : query.OrderByDescending(p => p.PrintDigital);
                        break;
                    case DimProjectSortTypes.Type:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.Type) : query.OrderByDescending(p => p.Type);
                        break;
                    case DimProjectSortTypes.Description:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description);
                        break;
                    case DimProjectSortTypes.C1HypCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.C1HypCode) : query.OrderByDescending(p => p.C1HypCode);
                        break;
                    case DimProjectSortTypes.C2HypCodeNew:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.C2HypCodeNew) : query.OrderByDescending(p => p.C2HypCodeNew);
                        break;
                    case DimProjectSortTypes.C2Management:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.C2Management) : query.OrderByDescending(p => p.C2Management);
                        break;
                    case DimProjectSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimProject existingItem)
        {
            db.DimProjects.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimProject item)
        {
            item.CreateDate = DateTime.Now;
            db.DimProjects.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimProject existingItem, DimProject item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.C1HypCode != item.C1HypCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "C1HypCode", existingItem.C1HypCode, item.C1HypCode);
                existingItem.C1HypCode = item.C1HypCode;
            }
            if (existingItem.C2HypCodeNew != item.C2HypCodeNew)
            {
                AddPropertyChangeLogEntry(logEntries, item, "C2HypCodeNew", existingItem.C2HypCodeNew, item.C2HypCodeNew);
                existingItem.C2HypCodeNew = item.C2HypCodeNew;
            }
            if (existingItem.C2Management != item.C2Management)
            {
                AddPropertyChangeLogEntry(logEntries, item, "C2Management", existingItem.C2Management, item.C2Management);
                existingItem.C2Management = item.C2Management;
            }
            if (existingItem.Description != item.Description)
            {
                AddPropertyChangeLogEntry(logEntries, item, "Description", existingItem.Description, item.Description);
                existingItem.Description = item.Description;
            }
            if (existingItem.ManagementBrand != item.ManagementBrand)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ManagementBrand", existingItem.ManagementBrand, item.ManagementBrand);
                existingItem.ManagementBrand = item.ManagementBrand;
            }
            if (existingItem.ManagementParent != item.ManagementParent)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ManagementParent", existingItem.ManagementParent, item.ManagementParent);
                existingItem.ManagementParent = item.ManagementParent;
            }
            if (existingItem.ManagementProject != item.ManagementProject)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ManagementProject", existingItem.ManagementProject, item.ManagementProject);
                existingItem.ManagementProject = item.ManagementProject;
            }
            if (existingItem.PrintDigital != item.PrintDigital)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PrintDigital", existingItem.PrintDigital, item.PrintDigital);
                existingItem.PrintDigital = item.PrintDigital;
            }
            if (existingItem.ProjectCode != item.ProjectCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ProjectCode", existingItem.ProjectCode, item.ProjectCode);
                existingItem.ProjectCode = item.ProjectCode;
            }
            if (existingItem.ProjectGroup != item.ProjectGroup)
            {
                AddPropertyChangeLogEntry(logEntries, item, "ProjectGroup", existingItem.ProjectGroup, item.ProjectGroup);
                existingItem.ProjectGroup = item.ProjectGroup;
            }
            if (existingItem.Type != item.Type)
            {
                AddPropertyChangeLogEntry(logEntries, item, "Type", existingItem.Type, item.Type);
                existingItem.Type = item.Type;
            }

            return logEntries;
        }

        protected override async Task<DimProject> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimProjects.FirstOrDefaultAsync(p => p.ProjectID == id);
        }

        protected override string GetDetailsForLog(DimProject model)
        {
            return $"ProjectID: {model.ProjectID}; " +
                   $"ProjectCode: {model.ProjectCode}; " +
                   $"ProjectGroup: {model.ProjectGroup}; " +
                   $"ManagementProject: {model.ManagementProject}; " +
                   $"ManagementParent: {model.ManagementParent}; " +
                   $"ManagementBrand: {model.ManagementBrand}; " +
                   $"PrintDigital: {model.PrintDigital}; " +
                   $"Type: {model.Type}; " +
                   $"Description: {model.Description}; " +
                   $"C1HypCode: {model.C1HypCode}; " +
                   $"C2HypCodeNew: {model.C2HypCodeNew}; " +
                   $"C2Management: {model.C2Management}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
