using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimAccountController : BaseReferenceSimpleController<DimAccount, DimAccountFilter, IReferenceRepository<DimAccount, DimAccountFilter>>
    {
        protected override string EntityName => "DimAccount";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimAccount/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimAccount/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimAccount/_ListItem.cshtml";

        public DimAccountController(IReferenceRepository<DimAccount, DimAccountFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}