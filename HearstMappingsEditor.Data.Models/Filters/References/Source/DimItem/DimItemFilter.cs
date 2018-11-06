using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class DimItemFilter : BaseFilter<DimItemSortTypes>
    {
        public string UAN { get; set; }
        public string Ic3p { get; set; }
        public string WGO { get; set; }
        public IList<short> ItemSigns { get; set; }
        public IList<short> SignMRs { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public DimItemFilter()
        {
            ItemSigns = new List<short>();
            SignMRs = new List<short>();
        }
    }
}
