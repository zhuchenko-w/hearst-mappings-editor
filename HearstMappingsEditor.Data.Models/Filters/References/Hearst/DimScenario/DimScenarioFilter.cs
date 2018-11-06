using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class DimScenarioFilter : BaseFilter<DimScenarioSortTypes>
    {
        public string ScenarioDesc { get; set; }
        public string ScenarioCode { get; set; }
        public string InputCode { get; set; }
        public IList<int> MonthNums { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public DimScenarioFilter()
        {
            MonthNums = new List<int>();
        }
    }
}
