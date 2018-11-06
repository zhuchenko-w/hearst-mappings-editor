using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public class ItemPLKindsController : BaseMappingController<
        ItemPLKindsViewModel, 
        ItemPLKindsFilter, 
        IMappingRepository<ItemPLKindsViewModel, ItemPLKindsFilter, ItemPLKindsRestriction>, 
        ItemPLKindsRestriction, 
        ItemPLKindsRefs>
    {
        protected override string EntityName => "ItemPLKinds";
        protected override string IndexViewPath => "~/Views/Mappings/ItemPLKinds/Index.cshtml";
        protected override string ListViewPath => "~/Views/Mappings/ItemPLKinds/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/Mappings/ItemPLKinds/_ListItem.cshtml";

        public ItemPLKindsController(IMappingRepository<ItemPLKindsViewModel, ItemPLKindsFilter, ItemPLKindsRestriction> mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, referencesRepository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(ItemPLKindsRefs refs)
        {
            refs.Depts = await _referencesRepository.GetAllDepts();
            refs.Items = await _referencesRepository.GetAllItems();
            refs.PLKinds = await _referencesRepository.GetAllPLKinds();
        }
    }
}