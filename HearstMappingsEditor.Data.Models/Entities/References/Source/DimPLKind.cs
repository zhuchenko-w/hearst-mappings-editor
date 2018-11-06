using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimPLKinds", Schema = "Source")]
    public class DimPLKind : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long PLKindID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(500), Required]
        public string PLKindName { get; set; }

        public long? PLGroupID { get; set; }

        [NotMapped]
        public long Id => PLKindID;
    }
}
