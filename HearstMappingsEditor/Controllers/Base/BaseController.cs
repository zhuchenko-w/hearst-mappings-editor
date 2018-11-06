using HearstMappingsEditor.Common;
using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using HearstMappingsEditor.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HearstMappingsEditor.Controllers
{
    public class BaseController : Controller//todo: move common code here, use generics
    {
        private const string ErrorPageUrl = "/Error";
        private const string NotFoundPageUrl = "/NotFoundPage";

        protected readonly ILogger _logger;
        protected readonly int _initialLoadPageSize;
        protected readonly int _pageSize;
        protected readonly string _fileStoragePath;
        protected readonly IMappingSyncLogic _mappingSyncLogic;

        public BaseController(ISettingsManager settingsManager, ILogger logger, IMappingSyncLogic mappingSyncLogic)
        {
            _logger = logger;
            _initialLoadPageSize = settingsManager.GetValue<int>("InitialLoadPageSize");
            _pageSize = settingsManager.GetValue<int>("PageSize");
            _fileStoragePath = settingsManager.GetValue<string>("FileStoragePath");
            _mappingSyncLogic = mappingSyncLogic;
        }

        [HttpPost]
        public async Task<JsonResult> SyncMapping()
        {
            return await RunWithResult(async () =>
            {
                return await _mappingSyncLogic.Sync();
            }, Constants.FailedToSyncMappingStateMessage);
        }
        [HttpPost]
        public async Task<JsonResult> CheckMappingSyncState()
        {
            return await RunWithResult(async () =>
            {
                return await _mappingSyncLogic.GetSyncMappingState();
            }, Constants.FailedToGetMappingSyncStateMessage);
        }
        [HttpGet]
        public ActionResult DownloadFile(Guid fileGuid, string fileName) 
        {
            try
            {
                var path = ExcelExportHelper.GetFilePath(fileGuid, _fileStoragePath);
                if (!System.IO.File.Exists(path))
                {
                    _logger.Error(Constants.FileNotFoundMessage);
                    return Redirect(NotFoundPageUrl);
                }

                var bytes = System.IO.File.ReadAllBytes(path);
                return File(bytes, "application/vnd.ms-excel", fileName);
            }
            catch (Exception ex)
            {
                _logger.Error(Constants.FailedToDownloadFileMessage, ex);
                return Redirect(ErrorPageUrl);
            }
        }

        protected async Task GetMappingSyncState<TViewModel, TFilter, TRestriction, TReferences>(MappingFilterableViewModel<TViewModel, TFilter, TRestriction, TReferences> model, bool throwOnException) 
            where TViewModel : class, IMapping
            where TFilter : class, IFilter, new()
            where TRestriction : class
            where TReferences : class, new()
        {
            try
            {
                model.MappingData.IsSynced = await _mappingSyncLogic.GetSyncMappingState();
            }
            catch (PublicException ex)
            {
                if (throwOnException)
                {
                    throw;
                }
                _logger.Error(Constants.FailedToGetMappingSyncStateMessage, ex);
                model.Error = ex.Message;
            }
            catch (Exception ex)
            {
                if (throwOnException)
                {
                    throw;
                }
                var message = Constants.FailedToGetMappingSyncStateMessage;
                _logger.Error(message, ex);
                model.Error = message;
            }
        }
        protected async Task<JsonResult> RunWithResult(Func<Task<object>> func, string errorMessage)
        {
            try
            {
                return new JsonResult
                {
                    Data = new
                    {
                        data = await func()
                    },
                    MaxJsonLength = Int32.MaxValue
                };
            }
            catch (PublicException ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = errorMessage
                    }
                };
            }
        }
        protected JsonResult RunWithResult(Func<object> func, string errorMessage)
        {
            try
            {
                return new JsonResult
                {
                    Data = new
                    {
                        data = func()
                    },
                    MaxJsonLength = Int32.MaxValue
                };
            }
            catch (PublicException ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = errorMessage
                    }
                };
            }
        }
        protected async Task<JsonResult> Run(Func<Task> func, string errorMessage)
        {
            try
            {
                await func();
                return new JsonResult
                {
                    Data = new
                    {
                        data = string.Empty
                    }
                };
            }
            catch (PublicException ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = ex.Message
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(errorMessage, ex);
                return new JsonResult
                {
                    Data = new
                    {
                        error = errorMessage
                    }
                };
            }
        }
        protected string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}