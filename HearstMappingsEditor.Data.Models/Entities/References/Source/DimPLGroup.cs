using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimPLGroups", Schema = "Source")]
    public class DimPLGroup : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long PLGroupID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(500)]
        public string PLGroupName { get; set; }

        [NotMapped]
        public long Id => PLGroupID;
    }
}
