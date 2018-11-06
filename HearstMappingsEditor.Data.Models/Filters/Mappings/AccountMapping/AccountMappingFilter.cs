using System;
using System.Collections.Generic;

namespace HearstMappingsEditor.Data.Models
{
    public class AccountMappingFilter : BaseFilter<AccountMappingSortTypes>
    {
        public IList<long> DeptIDs { get; set; }
        public IList<long> ItemIDs { get; set; }
        public IList<string> PrintDigitals { get; set; }
        public IList<long> AccountGroupIDs { get; set; }
        public IList<long> AccountIDs { get; set; }
        public IList<long> ProductIDs { get; set; }
        public IList<long> ChannelIDs { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public IList<short> SignMappings { get; set; }
        public IList<short> SignPLs { get; set; }

        public AccountMappingFilter()
        {
            DeptIDs = new List<long>();
            ItemIDs = new List<long>();
            PrintDigitals = new List<string>();
            AccountGroupIDs = new List<long>();
            AccountIDs = new List<long>();
            ProductIDs = new List<long>();
            ChannelIDs = new List<long>();
            SignMappings = new List<short>();
            SignPLs = new List<short>();
        }
    }
}
