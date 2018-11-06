using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimConsoSectionFilter : BaseFilter<DimConsoSectionSortTypes>
    {
        public string ConsoSectionDesc { get; set; }
        public string ConsoSectionCode { get; set; }
    }
}
