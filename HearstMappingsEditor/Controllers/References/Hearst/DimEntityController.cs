using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimEntityController : BaseReferenceSimpleController<DimEntity, DimEntityFilter, IReferenceRepository<DimEntity, DimEntityFilter>>
    {
        protected override string EntityName => "DimEntity";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimEntity/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimEntity/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimEntity/_ListItem.cshtml";

        public DimEntityController(IReferenceRepository<DimEntity, DimEntityFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}