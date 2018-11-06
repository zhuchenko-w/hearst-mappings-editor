using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class AccountMappingRestriction : BaseMappingRestriction
    {
        [RestrictionValue(IsSetFlagName = "DeptIdIsSet")]
        public long? DeptID { get; set; }
        public bool DeptIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "ItemIdIsSet")]
        public long? ItemID { get; set; }
        public bool ItemIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "AccountGroupIdIsSet")]
        public long? AccountGroupID { get; set; }
        public bool AccountGroupIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "AccountIdIsSet")]
        public long? AccountID { get; set; }
        public bool AccountIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "ProductIdIsSet")]
        public long? ProductID { get; set; }
        public bool ProductIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "ChannelIdIsSet")]
        public long? ChannelID { get; set; }
        public bool ChannelIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "PrintDigitalIsSet")]
        public string PrintDigital { get; set; }
        public bool PrintDigitalIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "SignPlIsSet")]
        public short? SignPL { get; set; }
        public bool SignPlIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "SignMappingIsSet")]
        public short? SignMapping { get; set; }
        public bool SignMappingIsSet { get; set; }
    }
}
