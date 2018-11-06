using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class EntityMappingViewModel : EntityMapping, IMapping
    {
        [ExcelExport(Order = 0)]
        public string PerimeterCode { get; set; }

        [ExcelExport(Order = 1)]
        public string EntityCode { get; set; }
    }
}
