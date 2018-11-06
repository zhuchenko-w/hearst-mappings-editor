using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimEntity", Schema = "dbo")]
    public class DimEntity : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long EntityID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(128)]
        public string EntityDesc { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(32)]
        public string EntityCode { get; set; }

        [ExcelExport(Order = 3)]
        [MaxLength(8)]
        public string EntityCurrency { get; set; }

        [ExcelExport(Order = 4)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => EntityID;
    }
}
