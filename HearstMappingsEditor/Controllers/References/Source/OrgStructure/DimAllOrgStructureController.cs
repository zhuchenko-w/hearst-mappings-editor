using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimAllOrgStructureController : BaseReferenceSimpleController<DimAllOrgStructure, DimAllOrgStructureFilter, IReferenceRepository<DimAllOrgStructure, DimAllOrgStructureFilter>>
    {
        protected override string EntityName => "DimAllOrgStructure";
        protected override string IndexViewPath => "~/Views/References/Source/OrgStructure/DimAllOrgStructure/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/OrgStructure/DimAllOrgStructure/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/OrgStructure/DimAllOrgStructure/_ListItem.cshtml";

        public DimAllOrgStructureController(IReferenceRepository<DimAllOrgStructure, DimAllOrgStructureFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}