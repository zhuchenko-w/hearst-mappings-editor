using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class BrandMappingRestriction : BaseMappingRestriction
    {
        [RestrictionValue(IsSetFlagName = "LOBDetailIdIsSet")]
        public long? LOBDetailID { get; set; }
        public bool LOBDetailIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "ProjectIdIsSet")]
        public long? ProjectID { get; set; }
        public bool ProjectIdIsSet { get; set; }
    }
}
