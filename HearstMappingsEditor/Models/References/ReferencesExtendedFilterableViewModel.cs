using HearstMappingsEditor.Data.Models;

namespace HearstMappingsEditor.Models
{
    public class ReferencesExtendedFilterableViewModel<TReference, TFilter, TRefs> : ReferencesFilterableViewModel<TReference, TFilter>
        where TReference: class
        where TFilter : class, IFilter, new()
        where TRefs : class, new()
    {
        public ReferencesExtendedFilterableViewModel()
        {
            Refs = new TRefs();
        }

        public TRefs Refs { get; set; }
    }
}