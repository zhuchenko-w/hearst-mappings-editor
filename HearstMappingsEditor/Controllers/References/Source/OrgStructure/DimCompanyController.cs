using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimCompanyController : BaseReferenceSimpleController<DimCompany, DimCompanyFilter, IReferenceRepository<DimCompany, DimCompanyFilter>>
    {
        protected override string EntityName => "DimCompany";
        protected override string IndexViewPath => "~/Views/References/Source/OrgStructure/DimCompany/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/OrgStructure/DimCompany/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/OrgStructure/DimCompany/_ListItem.cshtml";

        public DimCompanyController(IReferenceRepository<DimCompany, DimCompanyFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}