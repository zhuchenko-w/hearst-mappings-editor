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
    public abstract class BaseReferenceSimpleRepository<TReference, TFilter> : BaseReferenceRepository<TReference, TFilter>
        where TReference : class, IReference
        where TFilter : class, IFilter
    {
        public abstract IQueryable<TReference> GetListQuery(FinancialStatementContext db, TFilter filter);
        public override async Task<IList<TReference>> GetList(TFilter filter)
        {
            using (var db = new FinancialStatementContext())
            {
                var query = GetListQuery(db, filter);

                if (filter.Skip.HasValue)
                {
                    query = query.Skip(filter.Skip.Value);
                }
                if (filter.Take.HasValue)
                {
                    query = query.Take(filter.Take.Value);
                }

                try
                {
                    return await query.ToListAsync();
                }
                catch (Exception ex)
                {
                    throw new PublicException(GetListErrorText, ex);
                }
            }
        }

        protected abstract void RemoveFunc(FinancialStatementContext db, TReference existingItem);
        public async Task Remove(long id, LogParams logParams)//TODO: transaction?
        {
            using (var db = new FinancialStatementContext())
            {
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
                    logParams.Details = GetDetailsForLog(existingItem);
                    await SaveLog(db, logParams);
                }
            }
        }

        public async Task<SaveResult> Save(TReference item, LogParams logParams)//TODO: transaction?
        {
            using (var db = new FinancialStatementContext())
            {
                var existingItem = item.IsNew.HasValue && item.IsNew.Value ? null : await GetSingle(db, item.Id);
                if (existingItem == null)
                {
                    await Add(db, item, logParams);
                    return SaveResult.Added;
                }
                else
                {
                    await Edit(db, existingItem, item, logParams);
                    return SaveResult.Edited;
                }
            }
        }
        protected abstract IList<LogParams> EditFunc(FinancialStatementContext db, TReference existingItem, TReference item);
        private async Task Edit(FinancialStatementContext db, TReference existingItem, TReference item, LogParams logParams)
        {
            var logEntries = new List<LogParams>();

            try
            {
                logEntries.AddRange(EditFunc(db, existingItem, item));

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

        protected abstract Task<TReference> GetSingle(FinancialStatementContext db, long id);
    }
}
