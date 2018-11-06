using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IMappingRepository<TViewModel, TFilter, TRestriction> : IEntityRepository<TViewModel, TFilter>
        where TViewModel : class, IMapping
        where TFilter : class, IFilter
        where TRestriction : class
    {
        Task<IList<TRestriction>> GetRestrictions();
    }
}
