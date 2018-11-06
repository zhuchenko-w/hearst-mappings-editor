using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimPLGroupController : BaseReferenceSimpleController<DimPLGroup, DimPLGroupFilter, IReferenceRepository<DimPLGroup, DimPLGroupFilter>>
    {
        protected override string EntityName => "DimPLGroup";
        protected override string IndexViewPath => "~/Views/References/Source/DimPLGroup/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/DimPLGroup/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/DimPLGroup/_ListItem.cshtml";

        public DimPLGroupController(IReferenceRepository<DimPLGroup, DimPLGroupFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}