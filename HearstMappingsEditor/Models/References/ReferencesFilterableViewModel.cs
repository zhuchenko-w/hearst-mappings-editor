using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class ReferencesFilterableViewModel<TReference, TFilter>
        where TReference: class
        where TFilter : class, IFilter, new()
    {
        public ReferencesFilterableViewModel()
        {
            ReferenceItems = new List<TReference>();
        }

        public TFilter Filter { get; set; }
        public IList<TReference> ReferenceItems { get; set; }
        public int InitialLoadPageSize { get; set; }
        public int PageSize { get; set; }
        public string Error { get; set; }
    }
}