using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class CostCenterMappingRestriction : BaseMappingRestriction
    {
        [RestrictionValue(IsSetFlagName = "DeptIdIsSet")]
        public long? DeptID { get; set; }
        public bool DeptIdIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "PrintDigitalIsSet")]
        public string PrintDigital { get; set; }
        public bool PrintDigitalIsSet { get; set; }

        [RestrictionValue(IsSetFlagName = "CostCenterIdIsSet")]
        public long? CostCenterID { get; set; }
        public bool CostCenterIdIsSet { get; set; }
    }
}
