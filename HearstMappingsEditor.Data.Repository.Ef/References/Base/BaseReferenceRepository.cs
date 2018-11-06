using HearstMappingsEditor.Common;
using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using System;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public abstract class BaseReferenceRepository<TReference, TFilter> : BaseEntityRepository<TReference, TFilter>
        where TReference : class, IReference
        where TFilter : class, IFilter
    {
        protected abstract void AddFunc(FinancialStatementContext db, TReference item);
        protected async Task Add(FinancialStatementContext db, TReference item, LogParams logParams)
        {
            try
            {
                AddFunc(db, item);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (SqlExceptionHelper.IsUniqueConstraintError(ex))
                {
                    throw new PublicException(UniqueConstraintErrorText, ex);
                }
                throw new PublicException(CreatingErrorText, ex);
            }

            logParams.ActionType = CreatingActionType;
            logParams.Details = GetDetailsForLog(item);
            await SaveLog(db, logParams);
        }

        protected string GetStringContainedFilterPattern(string value)
        {
            var escapedValue = value.Replace("/", "//")
                .Replace("_", "/_")
                .Replace("%", "/%")
                .Replace("[", "/[");
            return $"%{escapedValue}%";
        }
    }
}
