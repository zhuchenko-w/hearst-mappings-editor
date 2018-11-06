using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IRestrictionsRepository
    {
        Task<IList<AccountMappingRestriction>> GetAccountMappingRestrictions(FinancialStatementContext db);
        Task<IList<BrandMappingRestriction>> GetBrandMappingRestrictions(FinancialStatementContext db);
        Task<IList<CostCenterMappingRestriction>> GetCostCenterMappingRestrictions(FinancialStatementContext db);
        Task<IList<EntityMappingRestriction>> GetEntityMappingRestrictions(FinancialStatementContext db);
    }
}
