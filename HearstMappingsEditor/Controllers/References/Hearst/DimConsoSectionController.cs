using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimConsoSectionController : BaseReferenceSimpleController<DimConsoSection, DimConsoSectionFilter, IReferenceRepository<DimConsoSection, DimConsoSectionFilter>>
    {
        protected override string EntityName => "DimConsoSection";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimConsoSection/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimConsoSection/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimConsoSection/_ListItem.cshtml";

        public DimConsoSectionController(IReferenceRepository<DimConsoSection, DimConsoSectionFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}