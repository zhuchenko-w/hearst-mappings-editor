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
    public class AccountMappingRepository : 
        BaseMappingRepository<AccountMapping, AccountMappingViewModel, AccountMappingFilter, AccountMappingRestriction>, 
        IMappingRepository<AccountMappingViewModel, AccountMappingFilter, AccountMappingRestriction>
    {
        protected override string SingularEntityName => "account mapping";
        protected override string PluralEntityName => "account mappings";

        public AccountMappingRepository(IRestrictionsRepository restrictionsRepository) : base(restrictionsRepository) { }

        public override async Task<IList<AccountMappingViewModel>> GetList(AccountMappingFilter filter)
        {
            using (var db = new FinancialStatementContext())
            {
                var accountGroupIDsFilterEmpty = filter.AccountGroupIDs == null || filter.AccountGroupIDs.Count == 0;
                var accountIDsFilterEmpty = filter.AccountIDs == null || filter.AccountIDs.Count == 0;
                var channelIDsFilterEmpty = filter.ChannelIDs == null || filter.ChannelIDs.Count == 0;
                var createDateFromFilterEmpty = !filter.CreateDateFrom.HasValue;
                var createDateToFilterEmpty = !filter.CreateDateTo.HasValue;
                var deptIDsFilterEmpty = filter.DeptIDs == null || filter.DeptIDs.Count == 0;
                var itemIDsFilterEmpty = filter.ItemIDs == null || filter.ItemIDs.Count == 0;
                var printDigitalsFilterEmpty = filter.PrintDigitals == null || filter.PrintDigitals.Count == 0;
                var productIDsFilterEmpty = filter.ProductIDs == null || filter.ProductIDs.Count == 0;
                var signMappingsFilterEmpty = filter.SignMappings == null || filter.SignMappings.Count == 0;
                var signPLsFilterEmpty = filter.SignPLs == null || filter.SignPLs.Count == 0;

                var query = (from am in db.AccountMappings
                             from dimDept in db.DimDepts.Where(p => p.DeptID == am.DeptID).DefaultIfEmpty()
                             from dimItem in db.DimItems.Where(p => p.ItemID == am.ItemID).DefaultIfEmpty()
                             from dimAccGroup in db.DimAccountGroups.Where(p => p.AccountGroupID == am.AccountGroupID).DefaultIfEmpty()
                             from dimAcc in db.DimAccounts.Where(p => p.AccountID == am.AccountID).DefaultIfEmpty()
                             from dimProduct in db.DimProducts.Where(p => p.ProductID == am.ProductID).DefaultIfEmpty()
                             from dimChannel in db.DimChannels.Where(p => p.ChannelID == am.ChannelID).DefaultIfEmpty()
                             where (accountGroupIDsFilterEmpty || am.AccountGroupID.HasValue && filter.AccountGroupIDs.Contains(am.AccountGroupID.Value))
                                 && (accountIDsFilterEmpty || filter.AccountIDs.Contains(am.AccountID))
                                 && (channelIDsFilterEmpty || filter.ChannelIDs.Contains(am.ChannelID))
                                 && (createDateFromFilterEmpty || am.CreateDate.HasValue && am.CreateDate.Value >= filter.CreateDateFrom.Value)
                                 && (createDateToFilterEmpty || am.CreateDate.HasValue && am.CreateDate.Value <= filter.CreateDateTo.Value)
                                 && (deptIDsFilterEmpty || am.DeptID.HasValue && filter.DeptIDs.Contains(am.DeptID.Value) || !am.DeptID.HasValue && filter.DeptIDs.Contains(0))
                                 && (itemIDsFilterEmpty || filter.ItemIDs.Contains(am.ItemID))
                                 && (printDigitalsFilterEmpty || !string.IsNullOrEmpty(am.PrintDigital) && filter.PrintDigitals.Contains(am.PrintDigital))
                                 && (productIDsFilterEmpty || filter.ProductIDs.Contains(am.ProductID))
                                 && (signMappingsFilterEmpty || am.SignMapping.HasValue && filter.SignMappings.Contains(am.SignMapping.Value))
                                 && (signPLsFilterEmpty || am.SignPL.HasValue && filter.SignPLs.Contains(am.SignPL.Value))
                             select new
                             {
                                 AccountCode = dimAcc.AccountCode + " (" + dimAcc.AccountDesc + ")",
                                 AccountGroupID = am.AccountGroupID,
                                 AccountGroupDesc = dimAccGroup.AccountGroupDesc,
                                 AccountID = am.AccountID,
                                 ChannelCode = dimChannel.ChannelCode + " (" + dimChannel.ChannelDesc + ")",
                                 ChannelID = am.ChannelID,
                                 CreateDate = am.CreateDate,
                                 Dept = dimDept.Dept,
                                 DeptID = am.DeptID,
                                 ItemID = am.ItemID,
                                 ItemUAN = dimItem.UAN,
                                 PrintDigital = am.PrintDigital,
                                 ProductCode = dimProduct.ProductCode + " (" + dimProduct.ProductDesc + ")",
                                 ProductID = am.ProductID,
                                 RowId = am.RowId,
                                 SignMapping = am.SignMapping,
                                 SignPL = am.SignPL
                             });

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case AccountMappingSortTypes.Dept:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.Dept) : query.OrderByDescending(p => p.Dept);
                            break;
                        case AccountMappingSortTypes.Item:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.ItemUAN) : query.OrderByDescending(p => p.ItemUAN);
                            break;
                        case AccountMappingSortTypes.PrintDigital:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PrintDigital) : query.OrderByDescending(p => p.PrintDigital);
                            break;
                        case AccountMappingSortTypes.AccountGroup:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountGroupDesc) : query.OrderByDescending(p => p.AccountGroupDesc);
                            break;
                        case AccountMappingSortTypes.Account:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.AccountCode) : query.OrderByDescending(p => p.AccountCode);
                            break;
                        case AccountMappingSortTypes.Product:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.ProductCode) : query.OrderByDescending(p => p.ProductCode);
                            break;
                        case AccountMappingSortTypes.Channel:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.ChannelCode) : query.OrderByDescending(p => p.ChannelCode);
                            break;
                        case AccountMappingSortTypes.SignMapping:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.SignMapping) : query.OrderByDescending(p => p.SignMapping);
                            break;
                        case AccountMappingSortTypes.SignPL:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.SignPL) : query.OrderByDescending(p => p.SignPL);
                            break;
                        case AccountMappingSortTypes.CreateDate:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.CreateDate) : query.OrderByDescending(p => p.CreateDate);
                            break;
                        case AccountMappingSortTypes.RowId:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.RowId) : query.OrderByDescending(p => p.RowId);
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
                    return (await query.ToListAsync()).Select(p => new AccountMappingViewModel
                    {
                        AccountCode = p.AccountCode,
                        AccountGroupDesc = p.AccountGroupDesc,
                        AccountGroupID = p.AccountGroupID,
                        AccountID = p.AccountID,
                        ChannelCode = p.ChannelCode,
                        ChannelID = p.ChannelID,
                        CreateDate = p.CreateDate,
                        Dept = p.Dept,
                        DeptID = p.DeptID,
                        ItemID = p.ItemID,
                        ItemUAN = p.ItemUAN,
                        PrintDigital = p.PrintDigital,
                        ProductCode = p.ProductCode,
                        ProductID = p.ProductID,
                        RowId = p.RowId,
                        SignMapping = p.SignMapping,
                        SignPL = p.SignPL
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting account mappings", ex);
                }
            }
        }

        protected override async Task<IList<AccountMappingRestriction>> GetRestrictions(FinancialStatementContext db)
        {
            return await _restrictionsRepository.GetAccountMappingRestrictions(db);
        }
        protected override bool CheckRestrictionMatchesModel(AccountMappingRestriction restriction, AccountMappingViewModel model)
        {
            return (restriction.AccountGroupIdIsSet
                        || restriction.AccountIdIsSet
                        || restriction.ChannelIdIsSet
                        || restriction.DeptIdIsSet
                        || restriction.ItemIdIsSet
                        || restriction.PrintDigitalIsSet
                        || restriction.ProductIdIsSet
                        || restriction.SignMappingIsSet
                        || restriction.SignPlIsSet)
                    && (!restriction.AccountGroupIdIsSet || restriction.AccountGroupID == model.AccountGroupID)
                    && (!restriction.AccountIdIsSet || restriction.AccountID == model.AccountID)
                    && (!restriction.ChannelIdIsSet || restriction.ChannelID == model.ChannelID)
                    && (!restriction.DeptIdIsSet || restriction.DeptID == model.DeptID)
                    && (!restriction.ItemIdIsSet || restriction.ItemID == model.ItemID)
                    && (!restriction.PrintDigitalIsSet || restriction.PrintDigital == model.PrintDigital)
                    && (!restriction.ProductIdIsSet || restriction.ProductID == model.ProductID)
                    && (!restriction.SignMappingIsSet || restriction.SignMapping == model.SignMapping)
                    && (!restriction.SignPlIsSet || restriction.SignPL == model.SignPL);
        }

        protected override void RemoveMappingFunc(FinancialStatementContext db, AccountMapping existingMapping)
        {
            db.AccountMappings.Remove(existingMapping);
        }

        protected override void CheckRestrictionsOnEditFunc(AccountMappingRestriction restriction, AccountMappingViewModel existingModel, AccountMappingViewModel model, IList<string> errorList)
        {
            if(restriction.AccountGroupIdIsSet && existingModel.AccountGroupID != model.AccountGroupID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("AccountGroupID", existingModel.RowId));
            }
            if (restriction.AccountIdIsSet && existingModel.AccountID != model.AccountID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("AccountID", existingModel.RowId));
            }
            if (restriction.ChannelIdIsSet && existingModel.ChannelID != model.ChannelID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("ChannelID", existingModel.RowId));
            }
            if (restriction.DeptIdIsSet && existingModel.DeptID != model.DeptID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("DeptID", existingModel.RowId));
            }
            if (restriction.ItemIdIsSet && existingModel.ItemID != model.ItemID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("ItemID", existingModel.RowId));
            }
            if (restriction.PrintDigitalIsSet && existingModel.PrintDigital != model.PrintDigital)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("PrintDigital", existingModel.RowId));
            }
            if (restriction.ProductIdIsSet && existingModel.ProductID != model.ProductID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("ProductID", existingModel.RowId));
            }
            if (restriction.SignMappingIsSet && existingModel.SignMapping != model.SignMapping)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("SignMapping", existingModel.RowId));
            }
            if (restriction.SignPlIsSet && existingModel.SignPL != model.SignPL)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("SignPL", existingModel.RowId));
            }
        }
        protected override IList<LogParams> EditMappingFunc(FinancialStatementContext db, AccountMapping existingMapping, AccountMappingViewModel existingModel, AccountMappingViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingMapping.AccountGroupID != model.AccountGroupID)
            {
                AddPropertyChangeLogEntry(
                    logEntries, 
                    model, 
                    "(AccountGroupID) AccountGroupDesc",
                    existingModel.AccountGroupID,
                    existingModel.AccountGroupDesc,
                    model.AccountGroupID,
                    model.AccountGroupDesc);
                existingMapping.AccountGroupID = model.AccountGroupID;
            }
            if (existingMapping.AccountID != model.AccountID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(AccountID) AccountCode",
                    existingModel.AccountID,
                    existingModel.AccountCode,
                    model.AccountID,
                    model.AccountCode);
                existingMapping.AccountID = model.AccountID;
            }
            if (existingMapping.ChannelID != model.ChannelID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(ChannelID) ChannelCode",
                    existingModel.ChannelID,
                    existingModel.ChannelCode,
                    model.ChannelID,
                    model.ChannelCode);
                existingMapping.ChannelID = model.ChannelID;
            }
            if (existingMapping.DeptID != model.DeptID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(DeptID) Dept",
                    existingModel.DeptID,
                    existingModel.Dept,
                    model.DeptID,
                    model.Dept);
                existingMapping.DeptID = model.DeptID;
            }
            if (existingMapping.ItemID != model.ItemID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(ItemID) ItemUAN",
                    existingModel.ItemID,
                    existingModel.ItemUAN,
                    model.ItemID,
                    model.ItemUAN);
                existingMapping.ItemID = model.ItemID;
            }
            if (existingMapping.PrintDigital != model.PrintDigital)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "PrintDigital",
                    existingModel.PrintDigital,
                    model.PrintDigital);
                existingMapping.PrintDigital = model.PrintDigital;
            }
            if (existingMapping.ProductID != model.ProductID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(ProductID) ProductCode",
                    existingModel.ProductID,
                    existingModel.ProductCode,
                    model.ProductID,
                    model.ProductCode);
                existingMapping.ProductID = model.ProductID;
            }
            if (existingMapping.SignMapping != model.SignMapping)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "SignMapping",
                    existingModel.SignMapping + "",
                    model.SignMapping + "");
                existingMapping.SignMapping = model.SignMapping;
            }
            if (existingMapping.SignPL != model.SignPL)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "SignPL",
                    existingModel.SignPL + "",
                    model.SignPL + "");
                existingMapping.SignPL = model.SignPL;
            }

            return logEntries;
        }
        protected override void AddMappingFunc(FinancialStatementContext db, AccountMappingViewModel model)
        {
            var newMapping = TinyMapper.Map<AccountMapping>(model);
            newMapping.CreateDate = DateTime.Now;
            db.AccountMappings.Add(newMapping);
        }

        protected override async Task<AccountMapping> GetSingle(FinancialStatementContext db, long rowId)
        {
            return await db.AccountMappings.FirstOrDefaultAsync(p => p.RowId == rowId);
        }
        protected override IQueryable<AccountMappingViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId)
        {
            return from am in db.AccountMappings
                   from dimDept in db.DimDepts.Where(p => p.DeptID == am.DeptID).DefaultIfEmpty()
                   from dimItem in db.DimItems.Where(p => p.ItemID == am.ItemID).DefaultIfEmpty()
                   from dimAccGroup in db.DimAccountGroups.Where(p => p.AccountGroupID == am.AccountGroupID).DefaultIfEmpty()
                   from dimAcc in db.DimAccounts.Where(p => p.AccountID == am.AccountID).DefaultIfEmpty()
                   from dimProduct in db.DimProducts.Where(p => p.ProductID == am.ProductID).DefaultIfEmpty()
                   from dimChannel in db.DimChannels.Where(p => p.ChannelID == am.ChannelID).DefaultIfEmpty()
                   where am.RowId == rowId
                   select new AccountMappingViewModel
                   {
                       AccountCode = dimAcc.AccountCode,
                       AccountGroupID = am.AccountGroupID,
                       AccountGroupDesc = dimAccGroup.AccountGroupDesc,
                       AccountID = am.AccountID,
                       ChannelCode = dimChannel.ChannelCode,
                       ChannelID = am.ChannelID,
                       CreateDate = am.CreateDate,
                       Dept = dimDept.Dept,
                       DeptID = am.DeptID,
                       ItemID = am.ItemID,
                       ItemUAN = dimItem.UAN,
                       PrintDigital = am.PrintDigital,
                       ProductCode = dimProduct.ProductCode,
                       ProductID = am.ProductID,
                       RowId = am.RowId,
                       SignMapping = am.SignMapping,
                       SignPL = am.SignPL
                   };
        }

        protected override string GetDetailsForLog(AccountMappingViewModel model)
        {
            return $"AccountGroupID: {model.AccountGroupID}; " +
                   $"AccountGroupDesc: {model.AccountGroupDesc}; " +
                   $"AccountID: {model.AccountID}; " +
                   $"AccountCode: {model.AccountCode}; " +
                   $"ChannelID: {model.ChannelID}; " +
                   $"ChannelCode: {model.ChannelCode}; " +
                   $"DeptID: {model.DeptID}; " +
                   $"Dept: {model.Dept}; " +
                   $"ItemID: {model.ItemID}; " +
                   $"ItemUAN: {model.ItemUAN}; " +
                   $"PrintDigital: {model.PrintDigital}; " +
                   $"ProductID: {model.ProductID}; " +
                   $"ProductCode: {model.ProductCode}; " +
                   $"SignMapping: {model.SignMapping}; " +
                   $"SignPL: {model.SignPL}";
        }
    }
}
