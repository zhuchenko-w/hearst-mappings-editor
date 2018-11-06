using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimAccount", Schema = "dbo")]
    public class DimAccount : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long AccountID { get; set; }

        public long? AllAccountsID { get; set; }

        [MaxLength(255)]
        public string AllAccountsDesc { get; set; }

        [ExcelExport(Order = 3)]
        [MaxLength(255)]
        public string AccountDesc { get; set; }

        [ExcelExport(Order = 4)]
        [MaxLength(50)]
        public string AccountCode { get; set; }

        [ExcelExport(Order = 5)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => AccountID;
    }
}
