using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimDeptFilter : BaseFilter<DimDeptSortTypes>
    {
        public string Dept { get; set; }
        public string DeptDesc { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
