using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("AccountProductChannelMapping", Schema = "Config")]
    public class AccountMapping : BaseEntity, IMapping
    {
        [ExcelExport(Order = 0)]
        [Column("RowID")]
        [Key]
        public long RowId { get; set; }

        public long? DeptID { get; set; }//--> Source.DimDept.DeptID

        public long ItemID { get; set; }//--> Source.DimItem.ItemID

        [ExcelExport(Order = 2)]
        [MaxLength(50)]
        public string PrintDigital { get; set; }//('eEdition','Print','Digital')

        public long? AccountGroupID { get; set; }//--> DimAccountGroup.AccountGroupID

        public long AccountID { get; set; }//--> DimAccount.AccountID

        public long ProductID { get; set; }//--> DimProduct.ProductID

        public long ChannelID { get; set; }//--> DimChannel.ChannelID

        [ExcelExport(Order = 8)]
        public short? SignMapping { get; set; }//(+1/-1)

        [ExcelExport(Order = 9)]
        public short? SignPL { get; set; }//(+1/-1)

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedUser { get; set; }

        [NotMapped]
        public long Id => RowId;
    }
}
