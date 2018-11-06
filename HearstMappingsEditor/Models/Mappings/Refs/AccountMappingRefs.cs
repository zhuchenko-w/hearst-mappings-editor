using HearstMappingsEditor.Data.Models;
using System.Collections.Generic;

namespace HearstMappingsEditor.Models
{
    public class AccountMappingRefs
    {
        public IList<DimDept> Depts { get; set; }
        public IList<DimItem> Items { get; set; }
        public IList<DimAccountGroup> AccountGroups { get; set; }
        public IList<DimAccount> Accounts { get; set; }
        public IList<DimProduct> Products { get; set; }
        public IList<DimChannel> Channels { get; set; }
        public IList<string> PrintDigitals { get; set; }
        public IList<short> SignPLs { get; set; }
        public IList<short> SignMappings { get; set; }
    }
}