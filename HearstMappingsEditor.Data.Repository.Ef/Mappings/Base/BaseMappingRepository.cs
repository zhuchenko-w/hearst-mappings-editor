using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HearstMappingsEditor.Common;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public abstract class BaseMappingRepository<TMapping, TViewModel, TFilter, TRestriction> : BaseEntityRepository<TViewModel, TFilter>
        where TMapping : class, IMapping
        where TViewModel : class, IMapping
        where TFilter : class, IFilter
    {
        protected readonly IRestrictionsRepository _restrictionsRepository;

        public BaseMappingRepository(IRestrictionsRepository restrictionsRepository)
        {
            _restrictionsRepository = restrictionsRepository;
        }

        public async Task<IList<TRestriction>> GetRestrictions()
        {
            using (var db = new FinancialStatementContext())
            {
                return await GetRestrictions(db);
            }
        }
        protected abstract Task<IList<TRestriction>> GetRestrictions(FinancialStatementContext db);
        protected abstract bool CheckRestrictionMatchesModel(TRestriction restriction, TViewModel model);
        private async Task<IList<string>> CheckRestrictions(FinancialStatementContext db, TViewModel existingModel, TViewModel model, Action<TRestriction, TViewModel, TViewModel, IList<string>> checkRestrictionNotViolatedFunc)
        {
            var errorList = new List<string>();

            var restrictions = await GetRestrictions(db);
            foreach (var restriction in restrictions)
            {
                if (CheckRestrictionMatchesModel(restriction, existingModel))
                {
                    checkRestrictionNotViolatedFunc(restriction, existingModel, model, errorList);
                }
            }

            return errorList;
        }

        private async Task<IList<string>> CheckRestrictionsOnRemove(FinancialStatementContext db, TViewModel existingModel)
        {
            return await CheckRestrictions(
                db, 
                existingModel, 
                null,
                (restriction, existingModelParam, newModelParam, errorList) => errorList.Add($"mapping with RowId = {existingModelParam.RowId} is locked by fields: {restriction.ToString()}"));
        }
        protected abstract void RemoveMappingFunc(FinancialStatementContext db, TMapping existingMapping);
        public async Task Remove(long rowId, LogParams logParams)//TODO: transaction?
        {
            using (var db = new FinancialStatementContext())
            {
                var model = await GetSingleModel(db, rowId);
                var existingMapping = await GetSingle(db, rowId);
                if (model != null && existingMapping != null)
                {
                    var checkErrors = await CheckRestrictionsOnRemove(db, model);
                    if (checkErrors.Any())
                    {
                        throw new PublicException("Mapping remove failed due to locks: " + JoinErrors(checkErrors));
                    }

                    try
                    {
                        RemoveMappingFunc(db, existingMapping);
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
                var existingMapping = model.IsNew.HasValue && model.IsNew.Value ? null : await GetSingle(db, model.RowId);
                if (existingMapping == null)
                {
                    await AddMapping(db, model, logParams);
                    return SaveResult.Added;
                }
                else
                {
                    await EditMapping(db, existingMapping, model, logParams);
                    return SaveResult.Edited;
                }
            }
        }

        protected abstract void CheckRestrictionsOnEditFunc(TRestriction restriction, TViewModel existingModel, TViewModel model, IList<string> errorList);
        protected string CreateRestrictionViolatedOnEditMessage(string editedFieldName, long mappingRowId)
        {
            return $"changing of {editedFieldName} for mapping with RowId = {mappingRowId} is locked";
        }
        private async Task<IList<string>> CheckRestrictionsOnEdit(FinancialStatementContext db, TViewModel existingModel, TViewModel model)
        {
            return await CheckRestrictions(
                db,
                existingModel,
                model,
                CheckRestrictionsOnEditFunc);
        }
        protected abstract IList<LogParams> EditMappingFunc(FinancialStatementContext db, TMapping existingMapping, TViewModel existingModel, TViewModel model);
        private async Task EditMapping(FinancialStatementContext db, TMapping existingMapping, TViewModel model, LogParams logParams)
        {
            var logEntries = new List<LogParams>();

            try
            {
                var existingModel = await GetSingleModel(db, model.RowId);

                var checkErrors = await CheckRestrictionsOnEdit(db, existingModel, model);
                if (checkErrors.Any())
                {
                    throw new PublicException("Mapping edit failed due to locks: " + JoinErrors(checkErrors));
                }

                logEntries.AddRange(EditMappingFunc(db, existingMapping, existingModel, model));

                existingMapping.ModifiedOn = DateTime.Now;
                existingMapping.ModifiedUser = logParams.AdUsername;

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
        protected abstract void AddMappingFunc(FinancialStatementContext db, TViewModel model);
        private async Task AddMapping(FinancialStatementContext db, TViewModel model, LogParams logParams)
        {
            try
            {
                model.ModifiedOn = DateTime.Now;
                model.ModifiedUser = logParams.AdUsername;

                AddMappingFunc(db, model);
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
            logParams.Details = GetDetailsForLog(model);
            await SaveLog(db, logParams);
        }

        protected abstract IQueryable<TViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId);
        protected abstract Task<TMapping> GetSingle(FinancialStatementContext db, long rowId);
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

        private string JoinErrors(IList<string> errorList)
        {
            var splitter = Environment.NewLine + "- ";
            return errorList != null && errorList.Any()
                ? errorList.Count == 1
                    ? errorList[0]
                    : splitter + string.Join(";" + splitter, errorList)
                : "";
        }
    }
}
