using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimYearController : BaseReferenceSimpleController<DimYear, DimYearFilter, IReferenceRepository<DimYear, DimYearFilter>>
    {
        protected override string EntityName => "DimYear";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimYear/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimYear/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimYear/_ListItem.cshtml";

        public DimYearController(IReferenceRepository<DimYear, DimYearFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}