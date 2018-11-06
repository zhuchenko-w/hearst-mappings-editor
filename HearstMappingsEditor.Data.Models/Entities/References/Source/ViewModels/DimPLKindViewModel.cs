using HearstMappingsEditor.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [NotMapped]
    public class DimPLKindViewModel : DimPLKind, IReference
    {
        [ExcelExport(Order = 2)]
        public string PLGroupName { get; set; }
    }
}
