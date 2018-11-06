using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimAccountGroup", Schema = "dbo")]
    public class DimAccountGroup : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public int AccountGroupID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(500)]
        public string AccountGroupDesc { get; set; }

        [NotMapped]
        public long Id => AccountGroupID;
    }
}
