using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimAllOrgStructure", Schema = "Source")]
    public class DimAllOrgStructure : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long AllOrgStructureID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(256)]
        public string AllOrgStructure { get; set; }

        [ExcelExport(Order = 2)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => AllOrgStructureID;
    }
}
