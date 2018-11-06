using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class BrandMappingViewModel : BrandMapping, IMapping
    {
        [ExcelExport(Order = 0)]
        public string ProjectCode { get; set; }

        [ExcelExport(Order = 1)]
        public string LOBDetailCode { get; set; }
    }
}
