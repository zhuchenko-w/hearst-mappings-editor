using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimDept", Schema = "Source")]
    public class DimDept : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long DeptID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(20)]
        public string Dept { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(100)]
        public string DeptDesc { get; set; }

        [ExcelExport(Order = 3)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => DeptID;
    }
}
