using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class OrgSubordinationByDateViewModel : OrgSubordinationByDate, IReference
    {
        [ExcelExport(Order = 1)]
        public string AllOrgStructure { get; set; }

        [ExcelExport(Order = 2)]
        public string PerimeterLaw { get; set; }

        [ExcelExport(Order = 3)]
        public string Perimeter { get; set; }

        [ExcelExport(Order = 4)]
        public string Company { get; set; }
    }
}
