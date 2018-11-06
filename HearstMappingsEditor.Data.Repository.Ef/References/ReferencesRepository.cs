using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HearstMappingsEditor.Data.Models.References;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class ReferencesRepository : IReferencesRepository
    {
        private const string DeptsCacheKey = "depts";
        private const string ItemsCacheKey = "items";
        private const string OrgStructuresCacheKey = "orgStructs";
        private const string ProjectsCacheKey = "projects";
        private const string AccountsCacheKey = "accounts";
        private const string AccountGroupsCacheKey = "accountGroups";
        private const string BrandsCacheKey = "brands";
        private const string ChannelsCacheKey = "channels";
        private const string CostCentersCacheKey = "costCenters";
        private const string EntitiesCacheKey = "entites";
        private const string ProductsCacheKey = "products";
        private const string PLKindsCacheKey = "plkinds";
        private const string PLGroupsCacheKey = "plgroups";
	    private const string ViewPerimetersCacheKey = "viewPerimeters";
        private const string AllOrgStructuresCacheKey = "allOrgStructs";
        private const string CompaniesCacheKey = "companies";
        private const string PerimetersCacheKey = "perimeters";
        private const string PerimeterLawsCacheKey = "perimeterLaws";

        private readonly TimeSpan _cacheExpiration;
        private readonly ICache _cache;

        public ReferencesRepository(ICache cache, ISettingsManager settingsManager)
        {
            _cache = cache;
            _cacheExpiration = TimeSpan.FromHours(settingsManager.GetValue<int>("ReferencesCacheExpirationHours"));
        }

        public async Task<IList<DimDept>> GetAllDepts()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(DeptsCacheKey, _cacheExpiration, async () =>  await db.DimDepts.ToListAsync());
            }
        }
        public async Task<IList<DimItem>> GetAllItems()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(ItemsCacheKey, _cacheExpiration, async () => await db.DimItems.ToListAsync());
            }
        }
        public async Task<IList<DimOrgStructure>> GetAllOrgStructures()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(OrgStructuresCacheKey, _cacheExpiration, async () => await db.DimOrgStructures.ToListAsync());
            }
        }
        public async Task<IList<DimProject>> GetAllProjects()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(ProjectsCacheKey, _cacheExpiration, async () => await db.DimProjects.ToListAsync());
            }
        }
        public async Task<IList<DimAccount>> GetAllAccounts()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(AccountsCacheKey, _cacheExpiration, async () => await db.DimAccounts.ToListAsync());
            }
        }
        public async Task<IList<DimAccountGroup>> GetAllAccountGroups()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(AccountGroupsCacheKey, _cacheExpiration, async () => await db.DimAccountGroups.ToListAsync());
            }
        }
        public async Task<IList<DimBrand>> GetAllBrands()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(BrandsCacheKey, _cacheExpiration, async () => await db.DimBrands.ToListAsync());
            }
        }
        public async Task<IList<DimChannel>> GetAllChannels()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(ChannelsCacheKey, _cacheExpiration, async () => await db.DimChannels.ToListAsync());
            }
        }
        public async Task<IList<DimCostCenter>> GetAllCostCenters()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(CostCentersCacheKey, _cacheExpiration, async () => await db.DimCostCenters.ToListAsync());
            }
        }
        public async Task<IList<DimEntity>> GetAllEntities()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(EntitiesCacheKey, _cacheExpiration, async () => await db.DimEntities.ToListAsync());
            }
        }
        public async Task<IList<DimProduct>> GetAllProducts()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(ProductsCacheKey, _cacheExpiration, async () => await db.DimProducts.ToListAsync());
            }
        }
        public async Task<IList<DimPLKind>> GetAllPLKinds()
	    {
		    using (var db = new FinancialStatementContext())
		    {
			    return await _cache.GetItem(PLKindsCacheKey, _cacheExpiration, async () => await db.DimPLKinds.ToListAsync());
		    }
	    }
        public async Task<IList<DimPLGroup>> GetAllPLGroups()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(PLGroupsCacheKey, _cacheExpiration, async () => await db.DimPLGroups.ToListAsync());
            }
        }
	    public async Task<IList<PerimeterEntity>> GetAllViewPerimeters()
	    {
		    using (var db = new FinancialStatementContext())
		    {
			    return await _cache.GetItem(ViewPerimetersCacheKey, _cacheExpiration, async () => await db.Perimeters.ToListAsync());
		    }
	    }
        public async Task<IList<DimAllOrgStructure>> GetAllAllOrgStructures()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(OrgStructuresCacheKey, _cacheExpiration, async () => await db.DimAllOrgStructures.ToListAsync());
            }
        }
        public async Task<IList<DimCompany>> GetAllCompanies()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(CompaniesCacheKey, _cacheExpiration, async () => await db.DimCompanies.ToListAsync());
            }
        }
        public async Task<IList<DimPerimeter>> GetAllPerimeters()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(PerimetersCacheKey, _cacheExpiration, async () => await db.DimPerimeters.ToListAsync());
            }
        }
        public async Task<IList<DimPerimeterLaw>> GetAllPerimeterLaws()
        {
            using (var db = new FinancialStatementContext())
            {
                return await _cache.GetItem(PerimeterLawsCacheKey, _cacheExpiration, async () => await db.DimPerimeterLaws.ToListAsync());
            }
        }
    }
}
