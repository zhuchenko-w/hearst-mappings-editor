using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class OrgSubordinationByDateFilter : BaseFilter<OrgSubordinationByDateSortTypes>
    {
        public IList<long?> AllOrgStructureIDs { get; set; }
        public IList<long> PerimeterLawIDs { get; set; }
        public IList<long> PerimeterIDs { get; set; }
        public IList<long> CompanyIDs { get; set; }
        public DateTime? DateStartFrom { get; set; }
        public DateTime? DateStartTo { get; set; }
        public DateTime? DateEndFrom { get; set; }
        public DateTime? DateEndTo { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }

        public OrgSubordinationByDateFilter()
        {
            AllOrgStructureIDs = new List<long?>();
            PerimeterLawIDs = new List<long>();
            PerimeterIDs = new List<long>();
            CompanyIDs = new List<long>();
        }
    }
}