using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Obsolete("Table splitted due to normalization")]
    [Table("DimOrgStructure", Schema = "Source")]
    public class DimOrgStructure : BaseEntity, IReference
    {
        public long? AllOrgStructureID { get; set; }

        [MaxLength(256)]
        public string AllOrgStructure { get; set; }

        public long? PerimeterLawID { get; set; }

        [MaxLength(256)]
        public string PerimeterLawDesc { get; set; }

        [MaxLength(64)]
        public string PerimeterLawCode { get; set; }

        public long? PerimeterID { get; set; }

        [MaxLength(256)]
        public string PerimeterDesc { get; set; }

        [MaxLength(64)]
        public string PerimeterCode { get; set; }

        [MaxLength(64)]
        public string PerimeterCurrency { get; set; }

        [Key]
        public long CompanyID { get; set; }

        [MaxLength(256)]
        public string CompanyDesc { get; set; }

        [MaxLength(64)]
        public string CompanyCode { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => CompanyID;
    }
}
