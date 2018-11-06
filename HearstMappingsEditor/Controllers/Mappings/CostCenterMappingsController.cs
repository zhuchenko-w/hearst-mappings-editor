using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public class CostCenterMappingsController : BaseMappingController<
        CostCenterMappingViewModel,
        CostCenterMappingFilter,
        IMappingRepository<CostCenterMappingViewModel, CostCenterMappingFilter, CostCenterMappingRestriction>,
        CostCenterMappingRestriction,
        CostCenterMappingRefs>
    {
        protected override string EntityName => "CostCenterMapping";
        protected override string IndexViewPath => "~/Views/Mappings/CostCenterMappings/Index.cshtml";
        protected override string ListViewPath => "~/Views/Mappings/CostCenterMappings/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/Mappings/CostCenterMappings/_ListItem.cshtml";

        public CostCenterMappingsController(IMappingRepository<CostCenterMappingViewModel, CostCenterMappingFilter, CostCenterMappingRestriction> mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger,
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, referencesRepository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(CostCenterMappingRefs refs)
        {
            refs.DimCostCenters = await _referencesRepository.GetAllCostCenters();
            refs.DimDepts = await _referencesRepository.GetAllDepts();
            refs.PrintDigitals = new List<string>() { "Print", "Digital" };//move to settings
        }
    }
}