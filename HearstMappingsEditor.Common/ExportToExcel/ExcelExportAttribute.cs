using System;

namespace HearstMappingsEditor.Common
{
    public class ExcelExportAttribute : Attribute
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public ExcelExportAttribute() { }
    }
}
