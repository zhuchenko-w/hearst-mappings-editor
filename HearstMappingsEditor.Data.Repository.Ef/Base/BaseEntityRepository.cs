using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public abstract class BaseEntityRepository<TModel, TFilter>
        where TModel : class
        where TFilter : class, IFilter
    {
        private const string ExecuteLogMappingProcedureQuery = "pLogMappingChange @createdOn, @actionType, @adUsername, @remoteAddress, @oldData, @newData, @details";
        protected const string UniqueConstraintErrorText = "Item primary key is not unique!";

        protected abstract string SingularEntityName { get; }
        protected abstract string PluralEntityName { get; }

        protected string GetListErrorText => $"An error occured while getting {PluralEntityName}";
        protected string CreatingErrorText => $"An error occured while creating new {SingularEntityName}";
        protected string CreatingActionType => $"Create {SingularEntityName}";
        protected string EditingErrorText => $"An error occured while editing {SingularEntityName}";
        protected string EditingActionType => $"Edit {SingularEntityName}";
        protected string RemovingErrorText => $"An error occured while removing {SingularEntityName}";
        protected string RemovingActionType => $"Remove {SingularEntityName}";

        public abstract Task<IList<TModel>> GetList(TFilter filter);

        protected abstract string GetDetailsForLog(TModel model);
        protected void AddPropertyChangeLogEntry(IList<LogParams> logEntries, TModel model, string propertyName, string oldData, string newData)
        {
            logEntries.Add(new LogParams
            {
                OldData = oldData,
                NewData = newData,
                Details = $"{GetDetailsForLog(model)}. {propertyName} was changed from {oldData} to {newData}",
            });
        }
        protected async Task SaveLog(FinancialStatementContext db, LogParams logParams)
        {
            try
            {
                logParams.CreatedOn = DateTime.Now;

                var sqlParams = new[]
                {
                    new SqlParameter { ParameterName = "@createdOn",  Value = logParams.CreatedOn , Direction = ParameterDirection.Input, SqlDbType = SqlDbType.DateTime, Size = 8},
                    new SqlParameter { ParameterName = "@actionType",  Value = logParams.ActionType, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = 100 },
                    new SqlParameter { ParameterName = "@adUsername",  Value = logParams.AdUsername, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = 100 },
                    new SqlParameter { ParameterName = "@remoteAddress",  Value = logParams.RemoteAddress, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = 50 },
                    new SqlParameter { ParameterName = "@oldData",  Value = logParams.OldData, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = 1000 },
                    new SqlParameter { ParameterName = "@newData",  Value = logParams.NewData, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = 1000 },
                    new SqlParameter { ParameterName = "@details",  Value = logParams.Details, Direction = ParameterDirection.Input, SqlDbType = SqlDbType.NVarChar, Size = -1 },
                };

                CheckParamValues(sqlParams);

                try
                {
                    await db.Database.ExecuteSqlCommandAsync(ExecuteLogMappingProcedureQuery, sqlParams);
                }
                catch (Exception ex)
                {
                    var nl = Environment.NewLine;
                    var message = $"CreatedOn: {logParams.CreatedOn}; " + nl +
                        $"ActionType: {logParams.ActionType}; " + nl +
                        $"AdUsername: {logParams.AdUsername}; " + nl +
                        $"RemoteAddress: {logParams.RemoteAddress}; " + nl +
                        $"OldData: {logParams.OldData}; " + nl +
                        $"NewData: {logParams.NewData}; " + nl +
                        $"Details: {logParams.Details} ";
                    throw new LogException($"Failed to write to log: {nl + message}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new PublicException("An error occured while writing to log", ex);
            }
        }

        private void CheckParamValues(SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Value + "" == "")
                {
                    parameters[i].Value = DBNull.Value;
                }
            }
        }
    }
}
