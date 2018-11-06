using HearstMappingsEditor.Data.Models;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IReferenceRepository<TReference, TFilter> : IEntityRepository<TReference, TFilter>
        where TReference : class, IReference
        where TFilter : class, IFilter
    {
    }
}
