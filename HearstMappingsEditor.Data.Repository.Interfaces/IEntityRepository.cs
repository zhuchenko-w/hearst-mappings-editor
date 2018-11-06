using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IEntityRepository<TEntity, TFilter>
        where TEntity : class
        where TFilter : class, IFilter
    {
        Task<IList<TEntity>> GetList(TFilter filter);
        Task Remove(long id, LogParams logParams);
        Task<SaveResult> Save(TEntity item, LogParams logParams);
    }
}
