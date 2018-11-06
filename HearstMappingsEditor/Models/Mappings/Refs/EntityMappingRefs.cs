using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;
using HearstMappingsEditor.Data.Models.References;

namespace HearstMappingsEditor.Models
{
    public class EntityMappingRefs
    {
        public IList<PerimeterEntity> Perimeters { get; set; }
        public IList<DimEntity> DimEntities { get; set; }
    }
}