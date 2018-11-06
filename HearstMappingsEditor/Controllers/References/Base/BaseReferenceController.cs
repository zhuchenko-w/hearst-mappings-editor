using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public abstract class BaseReferenceController<TReference, TFilter, TReferenceRepository> 
        : BaseEntityController<
            TReference, 
            TFilter, 
            TReferenceRepository, 
            ReferencesFilterableViewModel<TReference, TFilter>, 
            IList<TReference>, 
            TReference>
        where TReference : class, IReference
        where TFilter : class, IFilter, new()
        where TReferenceRepository : class, IReferenceRepository<TReference, TFilter>
    {
        public BaseReferenceController(TReferenceRepository repository, 
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
        }

        protected override async Task<IList<TReference>> GetListViewModel(TFilter filter)
        {
            return await GetFilteredItems(filter);
        }
        protected override async Task<TReference> GetListItemViewModel(TReference item)
        {
            return item;
        }
        protected override async Task<ReferencesFilterableViewModel<TReference, TFilter>> GetListModel(TFilter filter, bool throwOnException = true)
        {
            var model = new ReferencesFilterableViewModel<TReference, TFilter>
            {
                InitialLoadPageSize = _initialLoadPageSize,
                PageSize = _pageSize
            };
            var errorMessage = $"Failed to get {EntityName} items list";

            try
            {
                model.ReferenceItems = await GetFilteredItems(filter);
            }
            catch (PublicException ex)
            {
                if (throwOnException)
                {
                    throw;
                }
                _logger.Error(errorMessage, ex);
                model.Error = ex.Message;
            }
            catch (Exception ex)
            {
                if (throwOnException)
                {
                    throw;
                }
                _logger.Error(errorMessage, ex);
                model.Error = errorMessage;
            }

            model.Filter = filter;

            return model;
        }
    }
}