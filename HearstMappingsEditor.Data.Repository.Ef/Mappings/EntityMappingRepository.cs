using HearstMappingsEditor.Common.Exceptions;
using HearstMappingsEditor.Data.Context;
using HearstMappingsEditor.Data.Models;
using HearstMappingsEditor.Data.Repository.Interfaces;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Ef
{
    public class EntityMappingRepository : 
        BaseMappingRepository<EntityMapping, EntityMappingViewModel, EntityMappingFilter, EntityMappingRestriction>, 
        IMappingRepository<EntityMappingViewModel, EntityMappingFilter, EntityMappingRestriction>
    {
        protected override string SingularEntityName => "perimeter mapping";
        protected override string PluralEntityName => "perimeter mappings";

        public EntityMappingRepository(IRestrictionsRepository restrictionsRepository) : base(restrictionsRepository) { }

        public override async Task<IList<EntityMappingViewModel>> GetList(EntityMappingFilter filter)
        {
            using(var db = new FinancialStatementContext())
            {
                var entityIDsFilterEmpty = filter.EntityIDs == null || filter.EntityIDs.Count == 0;
                var perimeterIDsFilterEmpty = filter.PerimeterIDs == null || filter.PerimeterIDs.Count == 0;

                var query = from perimeter in db.Perimeters
							from entityMapping in db.EntityMappings.Where(em => em.PerimeterID == perimeter.PerimeterID)
							from dimEntity in db.DimEntities.Where(p => p.EntityID == entityMapping.EntityID).DefaultIfEmpty()
                            where (entityIDsFilterEmpty || entityMapping.EntityID.HasValue && filter.EntityIDs.Contains(entityMapping.EntityID.Value))
                                && (perimeterIDsFilterEmpty || entityMapping.PerimeterID.HasValue && filter.PerimeterIDs.Contains(entityMapping.PerimeterID.Value))
                            select new
                            {
                                EntityID = entityMapping.EntityID,
                                EntityCode = dimEntity.EntityCode,
                                PerimeterID = perimeter.PerimeterID,
                                PerimeterCode = perimeter.PerimeterCode,
								RowId = entityMapping.RowId
                            };

                if (filter.SortMode != null)
                {
                    switch (filter.SortMode.SortType)
                    {
                        case EntityMappingSortTypes.Entity:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.EntityCode) : query.OrderByDescending(p => p.EntityCode);
                            break;
                        case EntityMappingSortTypes.Perimeter:
                            query = filter.SortMode.Ascending ? query.OrderBy(p => p.PerimeterCode) : query.OrderByDescending(p => p.PerimeterCode);
                            break;
                        default:
                            query = query.OrderBy(p => p.EntityCode);
                            break;
                    }
                }
                else
                {
                    query = query.OrderBy(p => p.EntityCode);
                }

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
                    return (await query.ToListAsync()).Select(p => new EntityMappingViewModel
                    {
                        EntityID = p.EntityID,
                        EntityCode = p.EntityCode,
                        PerimeterID = p.PerimeterID,
                        PerimeterCode = p.PerimeterCode,
						RowId = p.RowId
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new PublicException("An error occured while getting perimeter mappings", ex);
                }
            }
        }

        protected override async Task<IList<EntityMappingRestriction>> GetRestrictions(FinancialStatementContext db)
        {
            return await _restrictionsRepository.GetEntityMappingRestrictions(db);
        }
        protected override bool CheckRestrictionMatchesModel(EntityMappingRestriction restriction, EntityMappingViewModel model)
        {
            return (restriction.EntityIdIsSet || restriction.PerimeterIdIsSet)
                    && (!restriction.EntityIdIsSet || restriction.EntityID == model.EntityID)
                    && (!restriction.PerimeterIdIsSet || restriction.PerimeterID == model.PerimeterID);
        }

        protected override void RemoveMappingFunc(FinancialStatementContext db, EntityMapping existingMapping)
        {
            db.EntityMappings.Remove(existingMapping);
        }

        protected override void CheckRestrictionsOnEditFunc(EntityMappingRestriction restriction, EntityMappingViewModel existingModel, EntityMappingViewModel model, IList<string> errorList)
        {
            if (restriction.EntityIdIsSet && existingModel.EntityID != model.EntityID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("EntityID", existingModel.RowId));
            }
            if (restriction.PerimeterIdIsSet && existingModel.PerimeterID != model.PerimeterID)
            {
                errorList.Add(CreateRestrictionViolatedOnEditMessage("PerimeterID", existingModel.RowId));
            }
        }
        protected override IList<LogParams> EditMappingFunc(FinancialStatementContext db, EntityMapping existingMapping, EntityMappingViewModel existingModel, EntityMappingViewModel model)
        {
            var logEntries = new List<LogParams>();

            if (existingMapping.EntityID != model.EntityID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(EntityID) Dept",
                    existingModel.EntityID,
                    existingModel.EntityCode,
                    model.EntityID,
                    model.EntityCode);
                existingMapping.EntityID = model.EntityID;
            }
            if (existingMapping.PerimeterID != model.PerimeterID)
            {
                AddPropertyChangeLogEntry(
                    logEntries,
                    model,
                    "(PerimeterID) CostCenterDesc",
                    existingModel.PerimeterID,
                    existingModel.PerimeterCode,
                    model.PerimeterID,
                    model.PerimeterCode);
                existingMapping.PerimeterID = model.PerimeterID;
            }

            return logEntries;
        }
        protected override void AddMappingFunc(FinancialStatementContext db, EntityMappingViewModel model)
        {
            var newMapping = TinyMapper.Map<EntityMapping>(model);
            db.EntityMappings.Add(newMapping);
        }

        protected override async Task<EntityMapping> GetSingle(FinancialStatementContext db, long rowId)
        {
            return await db.EntityMappings.FirstOrDefaultAsync(p => p.RowId == rowId);
        }
        protected override IQueryable<EntityMappingViewModel> GetSingleModelQuery(FinancialStatementContext db, long rowId)
        {
            return from em in db.EntityMappings
                   from dimOrgStructure in db.DimOrgStructures.Where(p => p.PerimeterID == em.PerimeterID).DefaultIfEmpty()
                   from dimEntity in db.DimEntities.Where(p => p.EntityID == em.EntityID).DefaultIfEmpty()
                   where em.RowId == rowId
                   select new EntityMappingViewModel
                   {
                       EntityID = em.EntityID,
                       EntityCode = dimEntity.EntityCode,
                       PerimeterID = em.PerimeterID,
                       PerimeterCode = dimOrgStructure.PerimeterCode,
	                   RowId = rowId
                   };
        }

        protected override string GetDetailsForLog(EntityMappingViewModel model)
        {
            return $"EntityID: {model.EntityID}; " +
                $"EntityCode: {model.EntityCode}; " +
                $"PerimeterID: {model.PerimeterID}; " +
                $"PerimeterCode: {model.PerimeterCode}";
        }
    }
}
