using System;

namespace HearstMappingsEditor.Data.Models
{
    public class LogParams
    {
        public DateTime CreatedOn { get; set; }
        public string ActionType { get; set; }
        public string AdUsername { get; set; }
        public string RemoteAddress { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string Details { get; set; }
    }
}
