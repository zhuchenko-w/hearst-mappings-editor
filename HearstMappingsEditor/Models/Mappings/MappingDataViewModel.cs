using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class MappingDataViewModel<TViewModel> where TViewModel : class, IMapping
    {
        public MappingDataViewModel()
        {
            Mappings = new List<TViewModel>();
        }

        public IList<TViewModel> Mappings { get; set; }
        public bool IsSynced { get; set; }
    }
}