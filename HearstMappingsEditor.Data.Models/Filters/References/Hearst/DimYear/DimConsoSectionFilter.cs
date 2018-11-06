using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimYearFilter : BaseFilter<DimYearSortTypes>
    {
        public string YearCode { get; set; }
        public string YearDesc { get; set; }
    }
}
