using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HearstMappingsEditor.Data.Models.References;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IReferencesRepository
    {
        Task<IList<DimDept>> GetAllDepts();
        Task<IList<DimItem>> GetAllItems();
        Task<IList<DimOrgStructure>> GetAllOrgStructures();
        Task<IList<DimProject>> GetAllProjects();
        Task<IList<DimAccount>> GetAllAccounts();
        Task<IList<DimAccountGroup>> GetAllAccountGroups();
        Task<IList<DimBrand>> GetAllBrands();
        Task<IList<DimChannel>> GetAllChannels();
        Task<IList<DimCostCenter>> GetAllCostCenters();
        Task<IList<DimEntity>> GetAllEntities();
        Task<IList<DimProduct>> GetAllProducts();
        Task<IList<DimPLKind>> GetAllPLKinds();
        Task<IList<DimPLGroup>> GetAllPLGroups();
        Task<IList<DimAllOrgStructure>> GetAllAllOrgStructures();
        Task<IList<DimCompany>> GetAllCompanies();
        Task<IList<DimPerimeter>> GetAllPerimeters();
        Task<IList<DimPerimeterLaw>> GetAllPerimeterLaws();

        Task<IList<PerimeterEntity>> GetAllViewPerimeters();
	}
}
