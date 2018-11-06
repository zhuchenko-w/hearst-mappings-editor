using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Controllers
{
    public class DimDeptController : BaseReferenceSimpleController<DimDept, DimDeptFilter, IReferenceRepository<DimDept, DimDeptFilter>>
    {
        protected override string EntityName => "DimDept";
        protected override string IndexViewPath => "~/Views/References/Source/DimDept/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/DimDept/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/DimDept/_ListItem.cshtml";

        public DimDeptController(IReferenceRepository<DimDept, DimDeptFilter> repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }
    }
}