using System;

namespace HearstMappingsEditor.Data.Models
{
    public class BaseFilter<TSortTypes> : IFilter
         where TSortTypes : struct, IConvertible, IComparable, IFormattable //Enum //lang version >= 7.3 only
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public BaseSortMode<TSortTypes> SortMode { get; set; }
    }
}
