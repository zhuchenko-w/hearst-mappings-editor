using HearstMappingsEditor.Common;
using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    [Authorize]
    public abstract class BaseEntityController<TEntity, TFilter, TRepository, TFilterableViewModel, TListViewModel, TListItemViewModel> : BaseController
        where TEntity : class, IEntity
        where TFilter : class, IFilter, new()
        where TRepository : class, IEntityRepository<TEntity, TFilter>
        where TFilterableViewModel: class
    {
        protected readonly TRepository _entityRepository;

        protected abstract string EntityName { get; }
        protected abstract string IndexViewPath { get; }
        protected abstract string ListViewPath { get; }
        protected abstract string ListItemViewPath { get; }

        public BaseEntityController(TRepository repository,
            ISettingsManager settingsManager, 
            ILogger logger, 
            IMappingSyncLogic mappingSyncLogic)
            : base(settingsManager, logger, mappingSyncLogic)
        {
            _entityRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index(TFilter filter)
        {
            var model = await GetListModel(filter);
            return View(IndexViewPath, model);
        }

        [HttpPost]
        public async Task<JsonResult> Save(long[] removed, TEntity[] addedOrEdited)
        {
            return await Run(async () =>
            {
                var username = Request.LogonUserIdentity.Name;
                var remoteAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (removed != null)
                {
                    foreach (var item in removed)
                    {
                        await _entityRepository.Remove(item, new LogParams
                        {
                            AdUsername = username,
                            RemoteAddress = remoteAddress
                        });
                    }
                }
                if (addedOrEdited != null)
                {
                    foreach (var item in addedOrEdited)
                    {
                        await _entityRepository.Save(item, new LogParams
                        {
                            AdUsername = username,
                            RemoteAddress = remoteAddress
                        });
                    }
                }
            }, $"Failed to save {EntityName} changes");
        }

        [HttpPost]
        public async Task<JsonResult> GetList(TFilter filter)
        {
            return await RunWithResult(async () =>
            {
                var model = await GetListViewModel(filter);
                return RenderViewToString(ListViewPath, model);
            }, $"Failed to get {EntityName} items list");
        }

        [HttpPost]
        public async Task<JsonResult> GetListItem(TEntity item)
        {
            return await RunWithResult( async() =>
            {
                var model = await GetListItemViewModel(item);
                return RenderViewToString(ListItemViewPath, model);
            }, $"Failed to create {EntityName} item view");
        }

        [HttpPost]
        public async Task<JsonResult> Export(TFilter filter, long[] removed, TEntity[] addedOrEdited)
        {
            return await RunWithResult(async () =>
            {
                var items = await GetFilteredItems(filter, true);
                var afterEdits = new List<TEntity>();
                var editedList = addedOrEdited == null ? new List<TEntity>() : addedOrEdited.Where(p => p.Id > 0);
                foreach (var item in items)
                {
                    if (removed == null || !removed.Contains(item.Id))
                    {
                        var edited = editedList.FirstOrDefault(p => p.Id == item.Id);
                        if (edited != null)
                        {
                            afterEdits.Add(edited);
                        }
                        else
                        {
                            afterEdits.Add(item);
                        }
                    }
                }

                if (addedOrEdited != null)
                {
                    afterEdits.InsertRange(0, addedOrEdited.Where(p => p.Id == 0).ToList());
                }

                return afterEdits.Any() ? ExcelExportHelper.SaveToExcel(afterEdits, _fileStoragePath) : null;
            }, $"Failed to export {EntityName} items to Excel file");
        }

        protected abstract Task<TListViewModel> GetListViewModel(TFilter filter);
        protected abstract Task<TListItemViewModel> GetListItemViewModel(TEntity item);
        protected abstract Task<TFilterableViewModel> GetListModel(TFilter filter, bool throwOnException = true);

        protected async Task<IList<TEntity>> GetFilteredItems(TFilter filter, bool takeAllIfFilterIsEmpty = false)
        {
            if (filter == null)
            {
                filter = new TFilter
                {
                    Take = takeAllIfFilterIsEmpty ? (int?)null : _initialLoadPageSize
                };
            }
            else if (!filter.Take.HasValue && !takeAllIfFilterIsEmpty)
            {
                filter.Take = _initialLoadPageSize;
            }
            return await _entityRepository.GetList(filter);
        }
    }
}