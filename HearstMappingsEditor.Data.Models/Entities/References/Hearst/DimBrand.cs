using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimBrand", Schema = "dbo")]
    public class DimBrand : BaseEntity, IReference
    {
        public long BrandID { get; set; }

        public long? AllBrandsID { get; set; }

        [MaxLength(255)]
        public string AllBrandDesc { get; set; }

        [MaxLength(255)]
        public string BrandDesc { get; set; }

        [MaxLength(50)]
        public string BrandCode { get; set; }

        [Key]
        public long LOBDetailID { get; set; }

        [MaxLength(255)]
        public string LOBDetailDesc { get; set; }

        [MaxLength(50)]
        public string LOBDetailCode { get; set; }

        [MaxLength(50)]
        public string ProjectType { get; set; }

        [MaxLength(50)]
        public string SheetName { get; set; }

        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => LOBDetailID;
    }
}
