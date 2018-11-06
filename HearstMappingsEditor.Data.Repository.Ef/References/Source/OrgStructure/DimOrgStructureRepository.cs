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
    [Obsolete("Table splitted due to normalization")]
    public class DimOrgStructureRepository :
        BaseReferenceSimpleRepository<DimOrgStructure, DimOrgStructureFilter>,
        IReferenceRepository<DimOrgStructure, DimOrgStructureFilter>
    {
        protected override string SingularEntityName => "DimOrgStructure";
        protected override string PluralEntityName => "DimOrgStructures";

        public override IQueryable<DimOrgStructure> GetListQuery(FinancialStatementContext db, DimOrgStructureFilter filter)
        {
            filter.AllOrgStructure = filter.AllOrgStructure?.Trim();
            filter.PerimeterLawDesc = filter.PerimeterLawDesc?.Trim();
            filter.PerimeterLawCode = filter.PerimeterLawCode?.Trim();
            filter.PerimeterDesc = filter.PerimeterDesc?.Trim();
            filter.PerimeterCode = filter.PerimeterCode?.Trim();
            filter.PerimeterCurrency = filter.PerimeterCurrency?.Trim();
            filter.CompanyDesc = filter.CompanyDesc?.Trim();
            filter.CompanyCode = filter.CompanyCode?.Trim();

            var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
            var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
            var dateStartFromFilterEmpty = !filter.DateStartFrom.HasValue;
            var dateStartToFilterEmpty = !filter.DateStartTo.HasValue;
            var dateEndFromFilterEmpty = !filter.DateEndFrom.HasValue;
            var dateEndToFilterEmpty = !filter.DateEndTo.HasValue;
            var allOrgStructureFilterEmpty = string.IsNullOrEmpty(filter.AllOrgStructure);
            var perimeterLawDescFilterEmpty = string.IsNullOrEmpty(filter.PerimeterLawDesc);
            var perimeterLawCodeFilterEmpty = string.IsNullOrEmpty(filter.PerimeterLawCode);
            var perimeterDescFilterEmpty = string.IsNullOrEmpty(filter.PerimeterDesc);
            var perimeterCodeFilterEmpty = string.IsNullOrEmpty(filter.PerimeterCode);
            var perimeterCurrencyFilterEmpty = string.IsNullOrEmpty(filter.PerimeterCurrency);
            var companyDescFilterEmpty = string.IsNullOrEmpty(filter.CompanyDesc);
            var companyCodeFilterEmpty = string.IsNullOrEmpty(filter.CompanyCode);

            var query = db.DimOrgStructures.Where(os =>
                (createDateFromFilterEmpty || os.CreateDate.HasValue && os.CreateDate.Value >= filter.CreateDateFrom.Value)
                && (createDateToFilterEmpty || os.CreateDate.HasValue && os.CreateDate.Value <= filter.CreateDateTo.Value)
                && (dateStartFromFilterEmpty || os.DateStart.HasValue && os.DateStart.Value >= filter.DateStartFrom.Value)
                && (dateStartToFilterEmpty || os.DateStart.HasValue && os.DateStart.Value <= filter.CreateDateTo.Value)
                && (dateEndFromFilterEmpty || os.DateEnd.HasValue && os.DateEnd.Value >= filter.DateEndFrom.Value)
                && (dateEndToFilterEmpty || os.DateEnd.HasValue && os.DateEnd.Value <= filter.CreateDateTo.Value)
                && (allOrgStructureFilterEmpty || os.AllOrgStructure != null && os.AllOrgStructure.Contains(filter.AllOrgStructure))
                && (perimeterLawDescFilterEmpty || os.PerimeterLawDesc != null && os.PerimeterLawDesc.Contains(filter.PerimeterLawDesc))
                && (perimeterLawCodeFilterEmpty || os.PerimeterLawCode != null && os.PerimeterLawCode.Contains(filter.PerimeterLawCode))
                && (perimeterDescFilterEmpty || os.PerimeterDesc != null && os.PerimeterDesc.Contains(filter.PerimeterDesc))
                && (perimeterCodeFilterEmpty || os.PerimeterCode != null && os.PerimeterCode.Contains(filter.PerimeterCode))
                && (perimeterCurrencyFilterEmpty || os.PerimeterCurrency != null && os.PerimeterCurrency.Contains(filter.PerimeterCurrency))
                && (companyDescFilterEmpty || os.CompanyDesc != null && os.CompanyDesc.Contains(filter.CompanyDesc))
                && (companyCodeFilterEmpty || os.CompanyCode != null && os.CompanyCode.Contains(filter.CompanyCode)));

            if (filter.SortMode != null)
            {
                switch (filter.SortMode.SortType)
                {
                    case DimOrgStructureSortTypes.AllOrgStructureID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AllOrgStructureID) : query.OrderByDescending(p => p.AllOrgStructureID);
                        break;
                    case DimOrgStructureSortTypes.AllOrgStructure:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.AllOrgStructure) : query.OrderByDescending(p => p.AllOrgStructure);
                        break;
                    case DimOrgStructureSortTypes.PerimeterLawID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawID) : query.OrderByDescending(p => p.PerimeterLawID);
                        break;
                    case DimOrgStructureSortTypes.PerimeterLawDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawDesc) : query.OrderByDescending(p => p.PerimeterLawDesc);
                        break;
                    case DimOrgStructureSortTypes.PerimeterLawCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLawCode) : query.OrderByDescending(p => p.PerimeterLawCode);
                        break;
                    case DimOrgStructureSortTypes.PerimeterID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterID) : query.OrderByDescending(p => p.PerimeterID);
                        break;
                    case DimOrgStructureSortTypes.PerimeterDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterDesc) : query.OrderByDescending(p => p.PerimeterDesc);
                        break;
                    case DimOrgStructureSortTypes.PerimeterCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterCode) : query.OrderByDescending(p => p.PerimeterCode);
                        break;
                    case DimOrgStructureSortTypes.PerimeterCurrency:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterCurrency) : query.OrderByDescending(p => p.PerimeterCurrency);
                        break;
                    case DimOrgStructureSortTypes.CompanyID:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyID) : query.OrderByDescending(p => p.CompanyID);
                        break;
                    case DimOrgStructureSortTypes.CompanyDesc:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyDesc) : query.OrderByDescending(p => p.CompanyDesc);
                        break;
                    case DimOrgStructureSortTypes.CompanyCode:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.CompanyCode) : query.OrderByDescending(p => p.CompanyCode);
                        break;
                    case DimOrgStructureSortTypes.DateStart:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.DateStart) : query.OrderByDescending(p => p.DateStart);
                        break;
                    case DimOrgStructureSortTypes.DateEnd:
                        query = filter.SortMode.Ascending ? query.OrderBy(p => p.DateEnd) : query.OrderByDescending(p => p.DateEnd);
                        break;
                    case DimOrgStructureSortTypes.CreateDate:
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

        protected override void RemoveFunc(FinancialStatementContext db, DimOrgStructure existingItem)
        {
            db.DimOrgStructures.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, DimOrgStructure item)
        {
            item.CreateDate = DateTime.Now;
            db.DimOrgStructures.Add(item);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, DimOrgStructure existingItem, DimOrgStructure item)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.AllOrgStructure != item.AllOrgStructure)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AllOrgStructure", existingItem.AllOrgStructure, item.AllOrgStructure);
                existingItem.AllOrgStructure = item.AllOrgStructure;
            }
            if (existingItem.AllOrgStructureID != item.AllOrgStructureID)
            {
                AddPropertyChangeLogEntry(logEntries, item, "AllOrgStructureID", existingItem.AllOrgStructureID + "", item.AllOrgStructureID + "");
                existingItem.AllOrgStructureID = item.AllOrgStructureID;
            }
            if (existingItem.CompanyCode != item.CompanyCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "CompanyCode", existingItem.CompanyCode, item.CompanyCode);
                existingItem.CompanyCode = item.CompanyCode;
            }
            if (existingItem.CompanyDesc != item.CompanyDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "CompanyDesc", existingItem.CompanyDesc, item.CompanyDesc);
                existingItem.CompanyDesc = item.CompanyDesc;
            }
            if (existingItem.DateEnd != item.DateEnd)
            {
                AddPropertyChangeLogEntry(logEntries, item, "DateEnd", existingItem.DateEnd.ToString(), item.DateEnd.ToString());
                existingItem.DateEnd = item.DateEnd;
            }
            if (existingItem.DateStart != item.DateStart)
            {
                AddPropertyChangeLogEntry(logEntries, item, "DateStart", existingItem.DateStart.ToString(), item.DateStart.ToString());
                existingItem.DateStart = item.DateStart;
            }
            if (existingItem.PerimeterCode != item.PerimeterCode)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterCode", existingItem.PerimeterCode, item.PerimeterCode);
                existingItem.PerimeterCode = item.PerimeterCode;
            }
            if (existingItem.PerimeterCurrency != item.PerimeterCurrency)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterCurrency", existingItem.PerimeterCurrency, item.PerimeterCurrency);
                existingItem.PerimeterCurrency = item.PerimeterCurrency;
            }
            if (existingItem.PerimeterDesc != item.PerimeterDesc)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterDesc", existingItem.PerimeterDesc, item.PerimeterDesc);
                existingItem.PerimeterDesc = item.PerimeterDesc;
            }
            if (existingItem.PerimeterID != item.PerimeterID)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterID", existingItem.PerimeterID + "", item.PerimeterID + "");
                existingItem.PerimeterID = item.PerimeterID;
            }
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
            if (existingItem.PerimeterLawID != item.PerimeterLawID)
            {
                AddPropertyChangeLogEntry(logEntries, item, "PerimeterLawID", existingItem.PerimeterLawID + "", item.PerimeterLawID + "");
                existingItem.PerimeterLawID = item.PerimeterLawID;
            }

            return logEntries;
        }

        protected override async Task<DimOrgStructure> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.DimOrgStructures.FirstOrDefaultAsync(p => p.CompanyID == id);
        }

        protected override string GetDetailsForLog(DimOrgStructure model)
        {
            return $"AllOrgStructureID: {model.AllOrgStructureID}; " +
                   $"AllOrgStructure: {model.AllOrgStructure}; " +
                   $"PerimeterLawID: {model.PerimeterLawID}; " +
                   $"PerimeterLawDesc: {model.PerimeterLawDesc}; " +
                   $"PerimeterLawCode: {model.PerimeterLawCode}; " +
                   $"PerimeterID: {model.PerimeterID}; " +
                   $"PerimeterDesc: {model.PerimeterDesc}; " +
                   $"PerimeterCode: {model.PerimeterCode}; " +
                   $"PerimeterCurrency: {model.PerimeterCurrency}; " +
                   $"CompanyID: {model.CompanyID}; " +
                   $"CompanyDesc: {model.CompanyDesc}; " +
                   $"CompanyCode: {model.CompanyCode}; " +
                   $"DateStart: {model.DateStart}; " +
                   $"DateEnd: {model.DateEnd}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
