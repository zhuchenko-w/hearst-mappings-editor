using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HearstMappingsEditor.Common;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public abstract class BaseReferenceExtendedRepository<TReference, TViewModel, TFilter> : BaseReferenceRepository<TViewModel, TFilter>
        where TReference : class, IReference
        where TViewModel : class, IReference
        where TFilter : class, IFilter
    {
        protected abstract void RemoveFunc(FinancialStatementContext db, TReference existingItem);
        public async Task Remove(long id, LogParams logParams)//TODO: transaction?
        {
            using (var db = new FinancialStatementContext())
            {
                var model = await GetSingleModel(db, id);
                var existingItem = await GetSingle(db, id);
                if (existingItem != null)
                {
                    try
                    {
                        RemoveFunc(db, existingItem);
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new PublicException(RemovingErrorText, ex);
                    }

                    logParams.ActionType = RemovingActionType;
                    logParams.Details = GetDetailsForLog(model);
                    await SaveLog(db, logParams);
                }
            }
        }

        public async Task<SaveResult> Save(TViewModel model, LogParams logParams)//TODO: transaction?
        {
            using (var db = new FinancialStatementContext())
            {
                var existingItem = model.IsNew.HasValue && model.IsNew.Value ? null : await GetSingle(db, model.Id);
                if (existingItem == null)
                {
                    await Add(db, model, logParams);
                    return SaveResult.Added;
                }
                else
                {
                    await Edit(db, existingItem, model, logParams);
                    return SaveResult.Edited;
                }
            }
        }
        protected abstract IList<LogParams> EditFunc(FinancialStatementContext db, TReference existingItem, TViewModel existingModel, TViewModel model);
        private async Task Edit(FinancialStatementContext db, TReference existingItem, TViewModel model, LogParams logParams)
        {
            var logEntries = new List<LogParams>();

            try
            {
                var existingModel = await GetSingleModel(db, model.Id);

                logEntries.AddRange(EditFunc(db, existingItem, existingModel, model));

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (SqlExceptionHelper.IsUniqueConstraintError(ex))
                {
                    throw new PublicException(UniqueConstraintErrorText, ex);
                }
                throw new PublicException(EditingErrorText, ex);
            }

            foreach (var logEntry in logEntries)
            {
                logEntry.ActionType = EditingActionType;
                logEntry.AdUsername = logParams.AdUsername;
                logEntry.RemoteAddress = logParams.RemoteAddress;
                await SaveLog(db, logEntry);
            }
        }

        protected abstract IQueryable<TViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId);
        protected abstract Task<TReference> GetSingle(FinancialStatementContext db, long id);
        protected async Task<TViewModel> GetSingleModel(FinancialStatementContext db, long rowId)
        {
            return await (GetSingleModelQuery(db, rowId)).FirstOrDefaultAsync();
        }

        protected void AddPropertyChangeLogEntry(IList<LogParams> logEntries, TViewModel model,
            string propertyDesc, long? oldDataId, string oldDataDesc, long? newDataId, string newDataDesc)
        {
            logEntries.Add(new LogParams
            {
                OldData = oldDataId + "",
                NewData = newDataId + "",
                Details = $"{GetDetailsForLog(model)}. {propertyDesc} was changed from ({oldDataId}) {oldDataDesc} to ({newDataId}) {newDataDesc}",
            });
        }
    }
}
