using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HearstMappingsEditor.Data.Repository.Interfaces;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class MappingSyncLogic : IMappingSyncLogic
    {
        public async Task<bool> GetSyncMappingState()
        {
            return (await ExecuteStoredProcedure("pSyncMappingState", "Failed to get mapping sync state")) == 1;
        }

        public async Task<int> Sync()
        {
            return await ExecuteStoredProcedure("pSyncMapping", "Failed to sync mapping state");
        }

        private async Task<int> ExecuteStoredProcedure(string storedProcedureName, string errorMessage)
        {
            using (var db = new FinancialStatementContext())
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                            returnParameter.Direction = ParameterDirection.ReturnValue;

                            connection.Open();
                            await cmd.ExecuteNonQueryAsync();
                            return (int)returnParameter.Value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new PublicException(errorMessage, ex);
                }
            }
        }
    }
}
