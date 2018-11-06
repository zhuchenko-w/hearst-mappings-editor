using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimPerimeterLaw", Schema = "Source")]
    public class DimPerimeterLaw : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long PerimeterLawID { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(256)]
        public string PerimeterLawDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(64)]
        public string PerimeterLawCode { get; set; }

        [ExcelExport(Order = 3)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => PerimeterLawID;
    }
}
