using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Controllers
{
    public class DimPLKindController : BaseReferenceExtendedController<DimPLKindViewModel, DimPLKindFilter, IReferenceRepository<DimPLKindViewModel, DimPLKindFilter>, DimPLKindRefs>
    {
        private readonly IReferencesRepository _referencesRepository;

        protected override string EntityName => "DimPLKind";
        protected override string IndexViewPath => "~/Views/References/Source/DimPLKind/Index.cshtml";
        protected override string ListViewPath => "~/Views/References/Source/DimPLKind/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/References/Source/DimPLKind/_ListItem.cshtml";

        public DimPLKindController(IReferenceRepository<DimPLKindViewModel, DimPLKindFilter> repository,
            IReferencesRepository referencesRepository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
            _referencesRepository = referencesRepository;
        }

        protected override async Task FillReferences(DimPLKindRefs refs)
        {
            refs.DimPLGroups = await _referencesRepository.GetAllPLGroups();
        }
    }
}