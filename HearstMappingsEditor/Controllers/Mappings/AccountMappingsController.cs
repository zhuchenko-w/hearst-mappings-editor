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
    public class AccountMappingsController : BaseMappingController<
        AccountMappingViewModel, 
        AccountMappingFilter, 
        IMappingRepository<AccountMappingViewModel, AccountMappingFilter, AccountMappingRestriction>, 
        AccountMappingRestriction, 
        AccountMappingRefs>
    {
        protected override string EntityName => "AccountMapping";
        protected override string IndexViewPath => "~/Views/Mappings/AccountMappings/Index.cshtml";
        protected override string ListViewPath => "~/Views/Mappings/AccountMappings/_List.cshtml";
        protected override string ListItemViewPath => "~/Views/Mappings/AccountMappings/_ListItem.cshtml";

        public AccountMappingsController(IMappingRepository<AccountMappingViewModel, AccountMappingFilter, AccountMappingRestriction> mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, referencesRepository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task FillReferences(AccountMappingRefs refs)
        {
            refs.AccountGroups = await _referencesRepository.GetAllAccountGroups();
            refs.Accounts = await _referencesRepository.GetAllAccounts();
            refs.Channels = await _referencesRepository.GetAllChannels();
            refs.Depts = await _referencesRepository.GetAllDepts();
            refs.Items = await _referencesRepository.GetAllItems();
            refs.PrintDigitals = new List<string> { "eEdition", "Print", "Digital" };//TODO: move to settings
            refs.Products = await _referencesRepository.GetAllProducts();
            refs.SignMappings = new List<short> { -1, 1 };//TODO: move to settings
            refs.SignPLs = new List<short> { -1, 1 };//TODO: move to settings
        }
    }
}