using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("OrgSubordinationByDate", Schema = "Source")]
    public class OrgSubordinationByDate : BaseEntity, IReference
    {
        [ExcelExport(Order = 0, Name = "ID")]
        [Column("ID")]
        [Key]
        public long OrgSubordinationByDateID { get; set; }

        public long? AllOrgStructureID { get; set; }

        public long? PerimeterLawID { get; set; }

        public long? PerimeterID { get; set; }

        public long CompanyID { get; set; }

        [ExcelExport(Order = 5)]
        public DateTime? DateStart { get; set; }

        [ExcelExport(Order = 6)]
        public DateTime? DateEnd { get; set; }

        [ExcelExport(Order = 7)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => OrgSubordinationByDateID;
    }
}
