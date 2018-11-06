using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Controllers
{
    public class DimItemController : BaseReferenceExtendedController<DimItem, DimItemFilter, IReferenceRepository<DimItem, DimItemFilter>, DimItemRefs>
    {
        protected override string EntityName => "DimItem";
        protected override string IndexViewPath => "~/Views/References/Source/DimItem/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/DimItem/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/DimItem/_ListItem.cshtml";

        public DimItemController(IReferenceRepository<DimItem, DimItemFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(DimItemRefs refs)
        {
            refs.Signs = new List<short> { -1, 0, 1 };//TODO: move to settings
        }
    }
}