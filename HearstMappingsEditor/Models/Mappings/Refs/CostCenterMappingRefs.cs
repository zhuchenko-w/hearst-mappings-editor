using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class CostCenterMappingRefs
    {
        public IList<DimDept> DimDepts { get; set; }
        public IList<DimCostCenter> DimCostCenters { get; set; }
        public IList<string> PrintDigitals { get; set; }
    }
}