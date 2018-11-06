using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimPerimeterLawFilter : BaseFilter<DimPerimeterLawSortTypes>
    {
        public string PerimeterLawDesc { get; set; }
        public string PerimeterLawCode { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}