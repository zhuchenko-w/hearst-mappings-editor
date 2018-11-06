using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class DimPLKindFilter : BaseFilter<DimPLKindSortTypes>
    {
        public string PLKindName { get; set; }
        public IList<long> PLGroupIDs { get; set; }

        public DimPLKindFilter()
        {
            PLGroupIDs = new List<long>();
        }
    }
}
