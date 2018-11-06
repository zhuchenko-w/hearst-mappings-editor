using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimEntityFilter : BaseFilter<DimEntitySortTypes>
    {
        public string EntityDesc { get; set; }
        public string EntityCode { get; set; }
        public string EntityCurrency { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
