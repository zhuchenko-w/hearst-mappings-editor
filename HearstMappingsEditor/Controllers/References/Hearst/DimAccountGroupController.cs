using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimAccountGroupController : BaseReferenceSimpleController<DimAccountGroup, DimAccountGroupFilter, IReferenceRepository<DimAccountGroup, DimAccountGroupFilter>>
    {
        protected override string EntityName => "DimAccountGroup";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimAccountGroup/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimAccountGroup/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimAccountGroup/_ListItem.cshtml";

        public DimAccountGroupController(IReferenceRepository<DimAccountGroup, DimAccountGroupFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}