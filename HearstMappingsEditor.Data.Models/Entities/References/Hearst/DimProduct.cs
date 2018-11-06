using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimProduct", Schema = "dbo")]
    public class DimProduct : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long ProductID { get; set; }

        public long? AllProductsID { get; set; }

        [MaxLength(255)]
        public string AllProductsDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(255)]
        public string ProductDesc { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(10)]
        public string ProductCode { get; set; }

        [ExcelExport(Order = 3)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => ProductID;
    }
}
