using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class MappingFilterableViewModel<TViewModel, TFilter, TMappingRestriction, TRefs>
        where TViewModel: class, IMapping
        where TFilter : class, IFilter, new()
        where TMappingRestriction : class
        where TRefs : class, new()
    {
        public MappingFilterableViewModel()
        {
            MappingData = new MappingDataViewModel<TViewModel>();
            Restrictions = new List<TMappingRestriction>();
            Refs = new TRefs();
        }

        public MappingDataViewModel<TViewModel> MappingData { get; set; }
        public TFilter Filter { get; set; }
        public IList<TMappingRestriction> Restrictions { get; set; }
        public TRefs Refs { get; set; }
        public int InitialLoadPageSize { get; set; }
        public int PageSize { get; set; }
        public string Error { get; set; }
    }
}