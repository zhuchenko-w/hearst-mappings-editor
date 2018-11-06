using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Controllers
{
    public class DimScenarioController : BaseReferenceExtendedController<DimScenario, DimScenarioFilter, IReferenceRepository<DimScenario, DimScenarioFilter>, DimScenarioRefs>
    {
        protected override string EntityName => "DimScenario";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimScenario/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimScenario/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimScenario/_ListItem.cshtml";

        public DimScenarioController(IReferenceRepository<DimScenario, DimScenarioFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(DimScenarioRefs refs)
        {
            refs.MonthNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };//TODO: move to settings
        }
    }
}