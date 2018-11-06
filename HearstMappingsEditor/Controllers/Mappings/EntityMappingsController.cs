using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public class EntityMappingsController : BaseMappingController<
        EntityMappingViewModel,
        EntityMappingFilter,
        IMappingRepository<EntityMappingViewModel, EntityMappingFilter, EntityMappingRestriction>,
        EntityMappingRestriction,
        EntityMappingRefs>
    {
        protected override string EntityName => "EntityMapping";
        protected override string IndexViewPath => "~/Views/Mappings/EntityMappings/Index.cshtml";
        protected override string ListViewPath => "~/Views/Mappings/EntityMappings/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/Mappings/EntityMappings/_ListItem.cshtml";

        public EntityMappingsController(IMappingRepository<EntityMappingViewModel, EntityMappingFilter, EntityMappingRestriction> mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger,
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, referencesRepository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(EntityMappingRefs refs)
        {
            refs.DimEntities = await _referencesRepository.GetAllEntities();
            refs.Perimeters = await _referencesRepository.GetAllViewPerimeters();
        }
    }
}