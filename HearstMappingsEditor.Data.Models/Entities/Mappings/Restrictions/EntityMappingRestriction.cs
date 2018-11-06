using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class EntityMappingRestriction : BaseMappingRestriction
    {
        [RestrictionValue(IsSetFlagName = "EntityIdIsSet")]
        public long? EntityID { get; set; }
        public bool EntityIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "PerimeterIdIsSet")]
        public long? PerimeterID { get; set; }
        public bool PerimeterIdIsSet { get; set; }
    }
}
