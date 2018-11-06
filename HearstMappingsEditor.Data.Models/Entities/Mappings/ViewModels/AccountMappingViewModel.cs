using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class AccountMappingViewModel : AccountMapping, IMapping
    {
        [ExcelExport(Order = 1)]
        public string Dept { get; set; }

        [ExcelExport(Order = 2)]
        public string ItemUAN { get; set; }

        [ExcelExport(Order = 4)]
        public string AccountGroupDesc { get; set; }

        [ExcelExport(Order = 5)]
        public string AccountCode { get; set; }

        [ExcelExport(Order = 6)]
        public string ProductCode { get; set; }

        [ExcelExport(Order = 7)]
        public string ChannelCode { get; set; }
    }
}
