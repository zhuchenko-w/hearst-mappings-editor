using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimAllOrgStructureFilter : BaseFilter<DimAllOrgStructureSortTypes>
    {
        public string AllOrgStructure { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}