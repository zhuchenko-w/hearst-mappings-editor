using System;

namespace HearstMappingsEditor.Data.Models
{
    public class DimCompanyFilter : BaseFilter<DimCompanySortTypes>
    {
        public string CompanyDesc { get; set; }
        public string CompanyCode { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }
}
