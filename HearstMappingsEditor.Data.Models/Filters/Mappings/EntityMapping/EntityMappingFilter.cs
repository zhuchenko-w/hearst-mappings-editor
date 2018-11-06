using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class EntityMappingFilter : BaseFilter<EntityMappingSortTypes>
    {
        public IList<long> PerimeterIDs { get; set; }
        public IList<long> EntityIDs { get; set; }

        public EntityMappingFilter()
        {
            PerimeterIDs = new List<long>();
            EntityIDs = new List<long>();
        }
    }
}
