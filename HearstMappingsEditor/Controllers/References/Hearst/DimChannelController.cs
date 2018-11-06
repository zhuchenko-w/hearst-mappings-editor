using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimChannelController : BaseReferenceSimpleController<DimChannel, DimChannelFilter, IReferenceRepository<DimChannel, DimChannelFilter>>
    {
        protected override string EntityName => "DimChannel";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimChannel/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimChannel/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimChannel/_ListItem.cshtml";

        public DimChannelController(IReferenceRepository<DimChannel, DimChannelFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}