using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class ItemPLKindsRefs
    {
        public IList<DimDept> Depts { get; set; }
        public IList<DimItem> Items { get; set; }
        public IList<DimPLKind> PLKinds { get; set; }
    }
}