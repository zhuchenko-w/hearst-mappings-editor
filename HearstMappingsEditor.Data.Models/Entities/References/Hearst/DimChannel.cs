using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimChannel", Schema = "dbo")]
    public class DimChannel : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long ChannelID { get; set; }

        public long? AllChannelsID { get; set; }

        [MaxLength(255)]
        public string AllChannelsDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(255)]
        public string ChannelDesc { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(50)]
        public string ChannelCode { get; set; }

        [ExcelExport(Order = 3)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => ChannelID;
    }
}
