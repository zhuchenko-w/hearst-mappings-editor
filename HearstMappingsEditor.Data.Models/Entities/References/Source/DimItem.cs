using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimItem", Schema = "Source")]
    public class DimItem : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long ItemID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(200)]
        public string UAN { get; set; }

        [ExcelExport(Order = 3)]
        [MaxLength(2)]
        public string Ic3p { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(32)]
        public string WGO { get; set; }

        [ExcelExport(Order = 4)]
        public short? ItemSign { get; set; }

        [ExcelExport(Order = 5)]
        public short? SignMR { get; set; }

        [ExcelExport(Order = 6)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => ItemID;
    }
}
