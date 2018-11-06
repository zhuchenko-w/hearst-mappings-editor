using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimPerimeter", Schema = "Source")]
    public class DimPerimeter : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long PerimeterID { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(256)]
        public string PerimeterDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(64)]
        public string PerimeterCode { get; set; }

        [ExcelExport(Order = 3)]
        [MaxLength(64)]
        public string PerimeterCurrency { get; set; }

        [ExcelExport(Order = 4)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => PerimeterID;
    }
}
