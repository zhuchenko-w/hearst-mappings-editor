using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("ItemPLKinds", Schema = "Config")]
    public class ItemPLKinds : BaseEntity, IMapping
    {
        [ExcelExport(Order = 0, Name = "ItemPLKindID")]
        [Column("ItemPLKindID")]
        [Key]
        public long RowId { get; set; }

        public long ItemID { get; set; }//--> Source.DimItem.ItemID

        public long? DeptID { get; set; }//--> Source.DimDept.DeptID

        public long PLKindID { get; set; }//--> Source.DimPLKinds.PLKindID

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedUser { get; set; }

        [NotMapped]
        public long Id => RowId;
    }
}
