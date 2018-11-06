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
    public abstract class BaseReferenceSimpleController<TReference, TFilter, TReferenceRepository>
        : BaseReferenceController<TReference, TFilter, TReferenceRepository>
        where TReference : class, IReference
        where TFilter : class, IFilter, new()
        where TReferenceRepository : class, IReferenceRepository<TReference, TFilter>
    {
        public BaseReferenceSimpleController(TReferenceRepository repository, 
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(repository, settingsManager, logger, mappingSyncLogic)
        {
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