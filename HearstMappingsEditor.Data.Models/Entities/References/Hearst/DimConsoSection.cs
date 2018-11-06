using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimConsoSection", Schema = "dbo")]
    public class DimConsoSection : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public int ConsoSectionID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(50)]
        public string ConsoSectionDesc { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(50)]
        [Column("COnsoSectionCode")]
        public string ConsoSectionCode { get; set; }

        [NotMapped]
        public long Id => ConsoSectionID;
    }
}
