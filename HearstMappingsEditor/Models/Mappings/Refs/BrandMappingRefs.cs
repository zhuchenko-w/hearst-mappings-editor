using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class BrandMappingRefs
    {
        public IList<DimProject> DimProjects { get; set; }
        public IList<DimBrand> DimBrands { get; set; }
    }
}