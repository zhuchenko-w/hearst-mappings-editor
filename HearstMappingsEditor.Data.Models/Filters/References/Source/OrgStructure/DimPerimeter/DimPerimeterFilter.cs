using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimPerimeterFilter : BaseFilter<DimPerimeterSortTypes>
    {
        public string PerimeterDesc { get; set; }
        public string PerimeterCode { get; set; }
        public string PerimeterCurrency { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}