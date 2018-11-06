using System;

namespace HearstMappingsEditor.Data.Models
{
    public class BaseSortMode<TSortTypes> where TSortTypes: struct, IConvertible, IComparable, IFormattable //Enum //lang version >= 7.3 only
    {
        public bool Ascending { get; set; }
        public TSortTypes SortType { get; set; }
    }
}
