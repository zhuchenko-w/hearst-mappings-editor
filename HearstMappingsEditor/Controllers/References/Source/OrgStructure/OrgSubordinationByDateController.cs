using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Controllers
{
    public class OrgSubordinationByDateController
        : BaseReferenceExtendedController<
            OrgSubordinationByDateViewModel, 
            OrgSubordinationByDateFilter, 
            IReferenceRepository<OrgSubordinationByDateViewModel, OrgSubordinationByDateFilter>, 
            OrgSubordinationByDateRefs>
    {
        private readonly IReferencesRepository _referencesRepository;

        protected override string EntityName => "OrgSubordinationByDate";
        protected override string IndexViewPath => "~/Views/References/Source/OrgStructure/OrgSubordinationByDate/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/OrgStructure/OrgSubordinationByDate/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/OrgStructure/OrgSubordinationByDate/_ListItem.cshtml";

        public OrgSubordinationByDateController(IReferenceRepository<OrgSubordinationByDateViewModel, OrgSubordinationByDateFilter> repository,
            IReferencesRepository referencesRepository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
            _referencesRepository = referencesRepository;
        }

        protected override async Task FillReferences(OrgSubordinationByDateRefs refs)
        {
            refs.DimAllOrgStructures = await _referencesRepository.GetAllAllOrgStructures();
            refs.DimCompanies = await _referencesRepository.GetAllCompanies();
            refs.DimPerimeters = await _referencesRepository.GetAllPerimeters();
            refs.DimPerimeterLaws = await _referencesRepository.GetAllPerimeterLaws();
        }
    }
}