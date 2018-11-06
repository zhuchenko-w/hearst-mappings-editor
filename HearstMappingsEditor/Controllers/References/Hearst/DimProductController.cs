using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimProductController : BaseReferenceSimpleController<DimProduct, DimProductFilter, IReferenceRepository<DimProduct, DimProductFilter>>
    {
        protected override string EntityName => "DimProduct";
        protected override string IndexViewPath => "~/Views/References/Hearst/DimProduct/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Hearst/DimProduct/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Hearst/DimProduct/_ListItem.cshtml";

        public DimProductController(IReferenceRepository<DimProduct, DimProductFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}