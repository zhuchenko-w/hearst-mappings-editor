using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class OrgSubordinationByDateRefs
    {
        public IList<DimAllOrgStructure> DimAllOrgStructures { get; set; }
        public IList<DimCompany> DimCompanies { get; set; }
        public IList<DimPerimeter> DimPerimeters { get; set; }
        public IList<DimPerimeterLaw> DimPerimeterLaws { get; set; }
    }
}