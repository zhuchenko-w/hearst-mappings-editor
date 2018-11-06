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
    public abstract class BaseMappingController<TViewModel, TFilter, TMappingRepository, TMappingRestriction, TRefs> 
        : BaseEntityController<
            TViewModel, 
            TFilter, 
            TMappingRepository, 
            MappingFilterableViewModel<TViewModel, TFilter, TMappingRestriction, TRefs>,
            Tuple<IList<TViewModel>, IList<TMappingRestriction>>,
            Tuple<TViewModel, IList<TMappingRestriction>>>
        where TViewModel : class, IMapping
        where TFilter : class, IFilter, new()
        where TMappingRepository : class, IMappingRepository<TViewModel, TFilter, TMappingRestriction>
        where TMappingRestriction : class
        where TRefs : class, new()
    {
        protected readonly IReferencesRepository _referencesRepository;

        public BaseMappingController(TMappingRepository mappingRepository, 
            IReferencesRepository referencesRepository, 
            ISettingsManager settingsManager,
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic) : base(mappingRepository, settingsManager, logger, mappingSyncLogic)
        {
            _referencesRepository = referencesRepository;
        }

        protected override async Task<Tuple<IList<TViewModel>, IList<TMappingRestriction>>> GetListViewModel(TFilter filter)
        {
            return new Tuple<IList<TViewModel>, IList<TMappingRestriction>>(await GetFilteredItems(filter), await _entityRepository.GetRestrictions()); ;
        }
        protected override async Task<Tuple<TViewModel, IList<TMappingRestriction>>> GetListItemViewModel(TViewModel item)
        {
            return new Tuple<TViewModel, IList<TMappingRestriction>>(item, await _entityRepository.GetRestrictions());
        }

        protected override async Task<MappingFilterableViewModel<TViewModel, TFilter, TMappingRestriction, TRefs>> GetListModel(TFilter filter, bool throwOnException = true)
        {
            var model = new MappingFilterableViewModel<TViewModel, TFilter, TMappingRestriction, TRefs>
            {
                InitialLoadPageSize = _initialLoadPageSize,
                PageSize = _pageSize,
            };
            var errorMessage = $"Failed to get {EntityName} items list";

            try
            {
                model.Restrictions = await _entityRepository.GetRestrictions();
                model.MappingData.Mappings = await GetFilteredItems(filter);
                await FillReferences(model.Refs);
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

            await GetMappingSyncState(model, throwOnException);

            model.Filter = filter;

            return model;
        }

        protected abstract Task FillReferences(TRefs refs);
    }
}