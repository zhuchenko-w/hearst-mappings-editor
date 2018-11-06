using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Controllers
{
    public class DimProjectController : BaseReferenceExtendedController<DimProject, DimProjectFilter, IReferenceRepository<DimProject, DimProjectFilter>, DimProjectRefs>
    {
        protected override string EntityName => "DimItem";
        protected override string IndexViewPath => "~/Views/References/Source/DimProject/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/DimProject/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/DimProject/_ListItem.cshtml";

        public DimProjectController(IReferenceRepository<DimProject, DimProjectFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(DimProjectRefs refs)
        {
            refs.ProjectGroups = new List<string> { "MN", "WN", "RN" };//TODO: move to settings
            refs.PrintDigitals = new List<string> { "Print", "Digital", "eEdition" };//TODO: move to settings
        }
    }
}