using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class BrandMappingFilter : BaseFilter<BrandMappingSortTypes>
    {
        public IList<long> ProjectIDs { get; set; }
        public IList<long> LOBDetailIDs { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public BrandMappingFilter()
        {
            ProjectIDs = new List<long>();
            LOBDetailIDs = new List<long>();
        }
    }
}
