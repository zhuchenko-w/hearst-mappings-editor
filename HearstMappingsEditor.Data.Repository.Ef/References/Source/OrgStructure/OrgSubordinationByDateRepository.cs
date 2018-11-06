using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class OrgSubordinationByDateRepository : 
        BaseReferenceExtendedRepository<OrgSubordinationByDate, OrgSubordinationByDateViewModel, OrgSubordinationByDateFilter>, 
        IReferenceRepository<OrgSubordinationByDateViewModel, OrgSubordinationByDateFilter>
    {
        protected override string SingularEntityName => "OrgSubordinationByDate";
        protected override string PluralEntityName => "OrgSubordinationByDates";

        public override async Task<IList<OrgSubordinationByDateViewModel>> GetList(OrgSubordinationByDateFilter filter)
        {
            using (var db = new FinancialStatementContext())
            {
                var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
                var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
                var dateStartFromFilterEmpty = !filter.DateStartFrom.HasValue;
                var dateStartToFilterEmpty = !filter.DateStartTo.HasValue;
                var dateEndFromFilterEmpty = !filter.DateEndFrom.HasValue;
                var dateEndToFilterEmpty = !filter.DateEndTo.HasValue;
                var allOrgStructureIDsFilterEmpty = filter.AllOrgStructureIDs == null || filter.AllOrgStructureIDs.Count == 0;
                var companyIDsFilterEmpty = filter.CompanyIDs == null || filter.CompanyIDs.Count == 0;
                var perimeterIDsFilterEmpty = filter.PerimeterIDs == null || filter.PerimeterIDs.Count == 0;
                var perimeterLawIDsFilterEmpty = filter.PerimeterLawIDs == null || filter.PerimeterLawIDs.Count == 0;

                var query = from osbd in db.OrgSubordinationByDates
                            from dimAllOrgStructure in db.DimAllOrgStructures.Where(p => p.AllOrgStructureID == osbd.AllOrgStructureID).DefaultIfEmpty()
                            from dimCompany in db.DimCompanies.Where(p => p.CompanyID == osbd.CompanyID).DefaultIfEmpty()
                            from dimPerimeter in db.DimPerimeters.Where(p => p.PerimeterID == osbd.PerimeterID).DefaultIfEmpty()
                            from dimPerimeterLaw in db.DimPerimeterLaws.Where(p => p.PerimeterLawID == osbd.PerimeterLawID).DefaultIfEmpty()
                            where (createDateFromFilterEmpty || osbd.CreateDate.HasValue && osbd.CreateDate.Value >= filter.CreateDateFrom.Value)
                                && (createDateToFilterEmpty || osbd.CreateDate.HasValue && osbd.CreateDate.Value <= filter.CreateDateTo.Value)
                                && (dateStartFromFilterEmpty || osbd.DateStart.HasValue && osbd.DateStart.Value >= filter.DateStartFrom.Value)
                                && (dateStartToFilterEmpty || osbd.DateStart.HasValue && osbd.DateStart.Value <= filter.DateStartTo.Value)
                                && (dateEndFromFilterEmpty || osbd.DateEnd.HasValue && osbd.DateEnd.Value >= filter.DateEndFrom.Value)
                                && (dateEndToFilterEmpty || osbd.DateEnd.HasValue && osbd.DateEnd.Value <= filter.DateEndTo.Value)
                                && (allOrgStructureIDsFilterEmpty || osbd.AllOrgStructureID.HasValue && filter.AllOrgStructureIDs.Contains(osbd.AllOrgStructureID.Value) || !osbd.AllOrgStructureID.HasValue && filter.AllOrgStructureIDs.Contains(null))
                                && (companyIDsFilterEmpty || filter.CompanyIDs.Contains(osbd.CompanyID))
                                && (perimeterIDsFilterEmpty || osbd.PerimeterID.HasValue && filter.PerimeterIDs.Contains(osbd.PerimeterID.Value) || !osbd.PerimeterID.HasValue && filter.PerimeterIDs.Contains(0))
                                && (perimeterLawIDsFilterEmpty || osbd.PerimeterLawID.HasValue && filter.PerimeterLawIDs.Contains(osbd.PerimeterLawID.Value) || !osbd.PerimeterLawID.HasValue && filter.PerimeterLawIDs.Contains(0))
                            select new
                            {
                                osbd.OrgSubordinationByDateID,
                                osbd.AllOrgStructureID,
                                dimAllOrgStructure.AllOrgStructure,
                                osbd.CompanyID,
                                Company = dimCompany.CompanyCode + " (" + dimCompany.CompanyDesc + ")",
                                osbd.PerimeterID,
                                Perimeter = osbd.PerimeterID.HasValue ? dimPerimeter.PerimeterCode + " (" + dimPerimeter.PerimeterDesc + ")" : null,
                                osbd.PerimeterLawID,
                                PerimeterLaw = osbd.PerimeterLawID.HasValue ? dimPerimeterLaw.PerimeterLawCode + " (" + dimPerimeterLaw.PerimeterLawDesc + ")" : null,
                                osbd.DateStart,
                                osbd.DateEnd,
                                osbd.CreateDate
                            };

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case OrgSubordinationByDateSortTypes.Id:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.OrgSubordinationByDateID) : query.OrderByDescending(p => p.OrgSubordinationByDateID);
                            break;
                        case OrgSubordinationByDateSortTypes.AllOrgStructure:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.AllOrgStructure) : query.OrderByDescending(p => p.AllOrgStructure);
                            break;
                        case OrgSubordinationByDateSortTypes.Company:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.Company) : query.OrderByDescending(p => p.Company);
                            break;
                        case OrgSubordinationByDateSortTypes.Perimeter:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.Perimeter) : query.OrderByDescending(p => p.Perimeter);
                            break;
                        case OrgSubordinationByDateSortTypes.PerimeterLaw:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterLaw) : query.OrderByDescending(p => p.PerimeterLaw);
                            break;
                        case OrgSubordinationByDateSortTypes.DateStart:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.DateStart) : query.OrderByDescending(p => p.DateStart);
                            break;
                        case OrgSubordinationByDateSortTypes.DateEnd:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.DateEnd) : query.OrderByDescending(p => p.DateEnd);
                            break;
                        case OrgSubordinationByDateSortTypes.CreateDate:
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

                if (filter.Skip.HasValue)
                {
                    query = query.Skip(filter.Skip.Value);
                }
                if (filter.Take.HasValue)
                {
                    query = query.Take(filter.Take.Value);
                }

                try
                {
                    return (await query.ToListAsync()).Select(p => new OrgSubordinationByDateViewModel
                    {
                        OrgSubordinationByDateID = p.OrgSubordinationByDateID,
                        AllOrgStructureID = p.AllOrgStructureID,
                        AllOrgStructure = p.AllOrgStructure,
                        CompanyID = p.CompanyID,
                        Company = p.Company,
                        PerimeterID = p.PerimeterID,
                        Perimeter = p.Perimeter,
                        PerimeterLawID = p.PerimeterLawID,
                        PerimeterLaw = p.PerimeterLaw,
                        DateStart = p.DateStart,
                        DateEnd = p.DateEnd,
                        CreateDate = p.CreateDate
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting OrgSubordinationByDate items", ex);
                }
            }
        }

        protected override void RemoveFunc(FinancialStatementContext db, OrgSubordinationByDate existingItem)
        {
            db.OrgSubordinationByDates.Remove(existingItem);
        }

        protected override void AddFunc(FinancialStatementContext db, OrgSubordinationByDateViewModel model)
        {
            var newItem = TinyMapper.Map<OrgSubordinationByDate>(model);
            newItem.CreateDate = DateTime.Now;
            db.OrgSubordinationByDates.Add(newItem);
        }
        protected override IList<LogParams> EditFunc(FinancialStatementContext db, OrgSubordinationByDate existingItem, OrgSubordinationByDateViewModel existingModel, OrgSubordinationByDateViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingItem.AllOrgStructureID != model.AllOrgStructureID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(AllOrgStructureID) AllOrgStructure",
                    existingModel.AllOrgStructureID,
                    existingModel.AllOrgStructure,
                    model.AllOrgStructureID,
                    model.AllOrgStructure);
                existingItem.AllOrgStructureID = model.AllOrgStructureID;
            }
            if (existingItem.CompanyID != model.CompanyID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(CompanyID) CompanyCode (CompanyDesc)",
                    existingModel.CompanyID,
                    existingModel.Company,
                    model.CompanyID,
                    model.Company);
                existingItem.CompanyID = model.CompanyID;
            }
            if (existingItem.PerimeterID != model.PerimeterID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(PerimeterID) PerimeterCode (PerimeterDesc)",
                    existingModel.PerimeterID,
                    existingModel.Perimeter,
                    model.PerimeterID,
                    model.Perimeter);
                existingItem.PerimeterID = model.PerimeterID;
            }
            if (existingItem.PerimeterLawID != model.PerimeterLawID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(PerimeterLawID) PerimeterLawCode (PerimeterLawDesc)",
                    existingModel.PerimeterLawID,
                    existingModel.PerimeterLaw,
                    model.PerimeterLawID,
                    model.PerimeterLaw);
                existingItem.PerimeterLawID = model.PerimeterLawID;
            }
            if (existingItem.DateEnd != model.DateEnd)
            {
                AddPropertyChangeLogEntry(logEntries, model, "DateEnd", existingItem.DateEnd.ToString(), model.DateEnd.ToString());
                existingItem.DateEnd = model.DateEnd;
            }
            if (existingItem.DateStart != model.DateStart)
            {
                AddPropertyChangeLogEntry(logEntries, model, "DateStart", existingItem.DateStart.ToString(), model.DateStart.ToString());
                existingItem.DateStart = model.DateStart;
            }

            return logEntries;
        }

        protected override async Task<OrgSubordinationByDate> GetSingle(FinancialStatementContext db, long id)
        {
            return await db.OrgSubordinationByDates.FirstOrDefaultAsync(p => p.OrgSubordinationByDateID == id);
        }
        protected override IQueryable<OrgSubordinationByDateViewModel> GetSingleModelQuery(FinancialStatementContext db, long id)
        {
            return from osbd in db.OrgSubordinationByDates
                   from dimAllOrgStructure in db.DimAllOrgStructures.Where(p => p.AllOrgStructureID == osbd.AllOrgStructureID).DefaultIfEmpty()
                   from dimCompany in db.DimCompanies.Where(p => p.CompanyID == osbd.CompanyID).DefaultIfEmpty()
                   from dimPerimeter in db.DimPerimeters.Where(p => p.PerimeterID == osbd.PerimeterID).DefaultIfEmpty()
                   from dimPerimeterLaw in db.DimPerimeterLaws.Where(p => p.PerimeterLawID == osbd.PerimeterLawID).DefaultIfEmpty()
                   where osbd.OrgSubordinationByDateID == id
                   select new OrgSubordinationByDateViewModel
                   {
                       OrgSubordinationByDateID = osbd.OrgSubordinationByDateID,
                       AllOrgStructureID = osbd.AllOrgStructureID,
                       AllOrgStructure = dimAllOrgStructure.AllOrgStructure,
                       CompanyID = osbd.CompanyID,
                       Company = dimCompany.CompanyCode + " (" + dimCompany.CompanyDesc + ")",
                       PerimeterID = osbd.PerimeterID,
                       Perimeter = osbd.PerimeterID.HasValue ? dimPerimeter.PerimeterCode + " (" + dimPerimeter.PerimeterDesc + ")" : null,
                       PerimeterLawID = osbd.PerimeterLawID,
                       PerimeterLaw = osbd.PerimeterLawID.HasValue ? dimPerimeterLaw.PerimeterLawCode + " (" + dimPerimeterLaw.PerimeterLawDesc + ")" : null,
                       DateStart = osbd.DateStart,
                       DateEnd = osbd.DateEnd,
                       CreateDate = osbd.CreateDate
                   };
        }

        protected override string GetDetailsForLog(OrgSubordinationByDateViewModel model)
        {
            return $"ID: {model.OrgSubordinationByDateID}; " +
                   $"AllOrgStructureID: {model.AllOrgStructureID}; " +
                   $"AllOrgStructure: {model.AllOrgStructure}; " +
                   $"CompanyID: {model.CompanyID}; " +
                   $"CompanyCode (CompanyDesc): {model.Company}; " +
                   $"PerimeterID: {model.PerimeterID}; " +
                   $"PerimeterCode (PerimeterDesc): {model.Perimeter}; " +
                   $"PerimeterLawID: {model.PerimeterLawID}; " +
                   $"PerimeterLawCode (PerimeterLawDesc): {model.PerimeterLaw}; " +
                   $"DateStart: {model.DateStart}; " +
                   $"DateEnd: {model.DateEnd}; " +
                   $"CreateDate: {model.CreateDate}";
        }
    }
}
