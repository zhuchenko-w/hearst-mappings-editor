using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimYear", Schema = "dbo")]
    public class DimYear : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int YearID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(4)]
        [Required]
        public string YearCode { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(64)]
        public string YearDesc { get; set; }

        [NotMapped]
        public long Id => YearID;
    }
}
