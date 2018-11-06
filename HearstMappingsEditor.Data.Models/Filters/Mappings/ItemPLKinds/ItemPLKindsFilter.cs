using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class ItemPLKindsFilter : BaseFilter<ItemPLKindsSortTypes>
    {
        public IList<long> DeptIDs { get; set; }
        public IList<long> ItemIDs { get; set; }
        public IList<long> PLKindIDs { get; set; }

        public ItemPLKindsFilter()
        {
            DeptIDs = new List<long>();
            ItemIDs = new List<long>();
            PLKindIDs = new List<long>();
        }
    }
}
