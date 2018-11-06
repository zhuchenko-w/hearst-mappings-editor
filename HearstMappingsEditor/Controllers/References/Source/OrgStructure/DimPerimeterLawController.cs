using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimPerimeterLawController : BaseReferenceSimpleController<DimPerimeterLaw, DimPerimeterLawFilter, IReferenceRepository<DimPerimeterLaw, DimPerimeterLawFilter>>
    {
        protected override string EntityName => "DimPerimeterLaw";
        protected override string IndexViewPath => "~/Views/References/Source/OrgStructure/DimPerimeterLaw/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/OrgStructure/DimPerimeterLaw/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/OrgStructure/DimPerimeterLaw/_ListItem.cshtml";

        public DimPerimeterLawController(IReferenceRepository<DimPerimeterLaw, DimPerimeterLawFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}