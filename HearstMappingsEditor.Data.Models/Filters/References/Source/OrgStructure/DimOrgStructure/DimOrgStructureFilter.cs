using System;

namespace HearstMappingsEditor.Data.Models
{
    [Obsolete("Table splitted due to normalization")]
    public class DimOrgStructureFilter : BaseFilter<DimOrgStructureSortTypes>
    {
        public string AllOrgStructure { get; set; }
        public string PerimeterLawDesc { get; set; }
        public string PerimeterLawCode { get; set; }
        public string PerimeterDesc { get; set; }
        public string PerimeterCode { get; set; }
        public string PerimeterCurrency { get; set; }
        public string CompanyDesc { get; set; }
        public string CompanyCode { get; set; }
        public DateTime? DateStartFrom { get; set; }
        public DateTime? DateStartTo { get; set; }
        public DateTime? DateEndFrom { get; set; }
        public DateTime? DateEndTo { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}