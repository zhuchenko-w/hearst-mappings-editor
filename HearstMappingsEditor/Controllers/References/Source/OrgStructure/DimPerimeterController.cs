using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimPerimeterController : BaseReferenceSimpleController<DimPerimeter, DimPerimeterFilter, IReferenceRepository<DimPerimeter, DimPerimeterFilter>>
    {
        protected override string EntityName => "DimPerimeter";
        protected override string IndexViewPath => "~/Views/References/Source/OrgStructure/DimPerimeter/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/OrgStructure/DimPerimeter/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/OrgStructure/DimPerimeter/_ListItem.cshtml";

        public DimPerimeterController(IReferenceRepository<DimPerimeter, DimPerimeterFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}