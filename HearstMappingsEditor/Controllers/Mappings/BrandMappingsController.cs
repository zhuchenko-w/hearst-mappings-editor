using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public class BrandMappingsController : BaseMappingController<
        BrandMappingViewModel,
        BrandMappingFilter,
        IMappingRepository<BrandMappingViewModel, BrandMappingFilter, BrandMappingRestriction>,
        BrandMappingRestriction,
        BrandMappingRefs>
    {
        protected override string EntityName => "BrandMapping";
        protected override string IndexViewPath => "~/Views/Mappings/BrandMappings/Index.cshtml";
        protected override string ListViewPath => "~/Views/Mappings/BrandMappings/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/Mappings/BrandMappings/_ListItem.cshtml";

        public BrandMappingsController(IMappingRepository<BrandMappingViewModel, BrandMappingFilter, BrandMappingRestriction> mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, referencesRepository, settingsManager, logger, mappingSyncLogic)
        {
        }
        
        protected override async Task FillReferences(BrandMappingRefs refs)
        {
            refs.DimBrands = await _referencesRepository.GetAllBrands();
            refs.DimProjects = await _referencesRepository.GetAllProjects ();
        }
    }
}