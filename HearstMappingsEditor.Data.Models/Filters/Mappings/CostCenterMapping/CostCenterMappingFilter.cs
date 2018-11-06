using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class CostCenterMappingFilter : BaseFilter<CostCenterMappingSortTypes>
    {
        public IList<long> DeptIDs { get; set; }
        public IList<long> CostCenterIDs { get; set; }
        public IList<string> PrintDigitals { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public CostCenterMappingFilter()
        {
            DeptIDs = new List<long>();
            CostCenterIDs = new List<long>();
            PrintDigitals = new List<string>();
        }
    }
}
