using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class CostCenterMappingViewModel : CostCenterMapping, IMapping
    {
        [ExcelExport(Order = 0)]
        public string Dept { get; set; }

        [ExcelExport(Order = 2)]
        public string CostCenterDesc { get; set; }
    }
}
