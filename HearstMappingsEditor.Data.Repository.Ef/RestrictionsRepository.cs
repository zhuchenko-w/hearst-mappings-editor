using HearstMappingsEditor.Common.Interfaces;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HearstMappingsEditor.Common.Exceptions;
using System.Data;
using System.Linq;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class RestrictionsRepository : IRestrictionsRepository
    {
        private const string AccountMappingRestrictionsCacheKey = "accountMappingRestrictions";
        private const string BrandMappingRestrictionsCacheKey = "brandMappingRestrictions";
        private const string CostCenterMappingRestrictionsCacheKey = "costCenterMappingRestrictions";
        private const string EntityMappingRestrictionsCacheKey = "entityMappingRestrictions";

        private const string AccountMappingRestrictionsSpName = "pLockedAccountMapping";
        private const string BrandMappingRestrictionsSpName = "pLockedBrandMapping";
        private const string CostCenterMappingRestrictionsSpName = "pLockedCostCenterMapping";
        private const string EntityMappingRestrictionsSpName = "pLockedEntityMapping";

        private readonly TimeSpan _cacheExpiration;
        private readonly ICache _cache;

        public RestrictionsRepository(ICache cache, ISettingsManager settingsManager)
        {
            _cache = cache;
            _cacheExpiration = TimeSpan.FromHours(settingsManager.GetValue<int>("RestrictionsCacheExpirationHours"));
        }

        public async Task<IList<AccountMappingRestriction>> GetAccountMappingRestrictions(FinancialStatementContext db)
        {
            return await _cache.GetItem(AccountMappingRestrictionsCacheKey, _cacheExpiration, async () => {
                return await GetRestrictions<AccountMappingRestriction>(db, AccountMappingRestrictionsSpName, "Failed to get locked account mappings");
            });
        }

        public async Task<IList<BrandMappingRestriction>> GetBrandMappingRestrictions(FinancialStatementContext db)
        {
            return await _cache.GetItem(BrandMappingRestrictionsCacheKey, _cacheExpiration, async () => {
                return await GetRestrictions<BrandMappingRestriction>(db, BrandMappingRestrictionsSpName, "Failed to get locked brand mappings");
            });
        }

        public async Task<IList<CostCenterMappingRestriction>> GetCostCenterMappingRestrictions(FinancialStatementContext db)
        {
            return await _cache.GetItem(CostCenterMappingRestrictionsCacheKey, _cacheExpiration, async () => {
                return await GetRestrictions<CostCenterMappingRestriction>(db, CostCenterMappingRestrictionsSpName, "Failed to get locked perimeter mappings");
            });
        }

        public async Task<IList<EntityMappingRestriction>> GetEntityMappingRestrictions(FinancialStatementContext db)
        {
            return await _cache.GetItem(EntityMappingRestrictionsCacheKey, _cacheExpiration, async () => {
                return await GetRestrictions<EntityMappingRestriction>(db, EntityMappingRestrictionsSpName, "Failed to get locked entity mappings");
            });
        }

        private async Task<IList<T>> GetRestrictions<T>(FinancialStatementContext db, string storedProcedureName, string errorMessage) where T: class
        {
            try
            {
                using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        return await ReadValues(command, (reader) => {
                            var restrictionType = typeof(T);
                            var restriction = (T)Activator.CreateInstance(restrictionType);

                            var propertyNames = typeof(T)
                                .GetProperties()
                                .Where(p => Attribute.IsDefined(p, typeof(RestrictionValueAttribute)))
                                .Select(p => new
                                {
                                    Property = p,
                                    Attribute = (RestrictionValueAttribute)Attribute.GetCustomAttribute(p, typeof(RestrictionValueAttribute), true)
                                })
                                .ToDictionary(p => p.Property.Name, p => p.Attribute.IsSetFlagName);
                            foreach (var name in propertyNames.Keys)
                            {
                                if (reader.HasColumn(name))
                                {
                                    var isSetFlagProperty = restrictionType.GetProperty(propertyNames[name]);
                                    if (isSetFlagProperty != null)
                                    {
                                        var columnIndex = reader.GetOrdinal(name);
                                        var value = reader.IsDBNull(columnIndex) ? null : reader[columnIndex];
                                        var valueProperty = restrictionType.GetProperty(name);
                                        valueProperty.SetValue(restriction, value);
                                        isSetFlagProperty.SetValue(restriction, true);
                                    }
                                }
                            }

                            return restriction;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new PublicException(errorMessage, ex);
            }
        }

        private async Task<IList<T>> ReadValues<T>(SqlCommand command, Func<SqlDataReader, T> readFunc) where T : class
        {
            var values = new List<T>();

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    values.Add(readFunc(reader));
                }
            }

            return values;
        }
    }
}
