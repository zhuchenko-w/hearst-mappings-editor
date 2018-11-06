using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimCompany", Schema = "Source")]
    public class DimCompany : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long CompanyID { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(256)]
        public string CompanyDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(64)]
        public string CompanyCode { get; set; }

        [ExcelExport(Order = 3)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => CompanyID;
    }
}
