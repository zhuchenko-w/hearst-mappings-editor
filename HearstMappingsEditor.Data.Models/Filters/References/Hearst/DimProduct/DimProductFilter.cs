using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimProductFilter : BaseFilter<DimProductSortTypes>
    {
        public string ProductDesc { get; set; }
        public string ProductCode { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
