using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimChannelFilter : BaseFilter<DimChannelSortTypes>
    {
        public string ChannelDesc { get; set; }
        public string ChannelCode { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
