using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class DimProjectFilter : BaseFilter<DimProjectSortTypes>
    {
        public string ProjectCode { get; set; }
        public IList<string> ProjectGroups { get; set; }
        public string ManagementProject { get; set; }
        public string ManagementParent { get; set; }
        public string ManagementBrand { get; set; }
        public IList<string> PrintDigitals { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string C1HypCode { get; set; }
        public string C2HypCodeNew { get; set; }
        public string C2Management { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public DimProjectFilter()
        {
            ProjectGroups = new List<string>();
            PrintDigitals = new List<string>();
        }
    }
}
