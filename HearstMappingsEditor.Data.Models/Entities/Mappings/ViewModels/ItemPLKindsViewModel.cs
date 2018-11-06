using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class ItemPLKindsViewModel : ItemPLKinds, IMapping
    {
        [ExcelExport(Order = 1)]
        public string Dept { get; set; }

        [ExcelExport(Order = 2)]
        public string ItemUAN { get; set; }

        [ExcelExport(Order = 3)]
        public string PLKindName { get; set; }
    }
}
