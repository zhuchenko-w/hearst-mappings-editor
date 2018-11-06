using HearstMappingsEditor.Data.Models;
using System.Data.Entity;
using HearstMappingsEditor.Data.Models.References;

namespace HearstMappingsEditor.Data.Context
{
    public class FinancialStatementContext : DbContext
    {
        public FinancialStatementContext() : base("FinancialStatement")
        {
            Database.SetInitializer<FinancialStatementContext>(null);
        }

        public DbSet<AccountMapping> AccountMappings { get; set; }
        public DbSet<BrandMapping> BrandMappings { get; set; }
        public DbSet<CostCenterMapping> CostCenterMappings { get; set; }
        public DbSet<EntityMapping> EntityMappings { get; set; }
        public DbSet<ItemPLKinds> ItemPLKinds { get; set; }

        public DbSet<DimAccount> DimAccounts { get; set; }
        public DbSet<DimAccountGroup> DimAccountGroups { get; set; }
        public DbSet<DimBrand> DimBrands { get; set; }
        public DbSet<DimChannel> DimChannels { get; set; }
        public DbSet<DimCostCenter> DimCostCenters { get; set; }
        public DbSet<DimEntity> DimEntities { get; set; }
        public DbSet<DimProduct> DimProducts { get; set; }
        public DbSet<DimScenario> DimScenarios { get; set; }
        public DbSet<DimConsoSection> DimConsoSections { get; set; }
        public DbSet<DimYear> DimYears { get; set; }


        public DbSet<DimDept> DimDepts { get; set; }
        public DbSet<DimItem> DimItems { get; set; }
        public DbSet<DimProject> DimProjects { get; set; }
        public DbSet<DimPLKind> DimPLKinds { get; set; }
        public DbSet<DimPLGroup> DimPLGroups { get; set; }
        public DbSet<DimOrgStructure> DimOrgStructures { get; set; }
        public DbSet<DimAllOrgStructure> DimAllOrgStructures { get; set; }
        public DbSet<OrgSubordinationByDate> OrgSubordinationByDates { get; set; }
        public DbSet<DimCompany> DimCompanies { get; set; }
        public DbSet<DimPerimeter> DimPerimeters { get; set; }
        public DbSet<DimPerimeterLaw> DimPerimeterLaws { get; set; }

        public DbSet<PerimeterEntity> Perimeters { get; set; }
    }
}
