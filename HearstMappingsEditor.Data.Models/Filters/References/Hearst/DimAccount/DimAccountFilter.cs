using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimAccountFilter : BaseFilter<DimAccountSortTypes>
    {
        public string AccountDesc { get; set; }
        public string AccountCode { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
