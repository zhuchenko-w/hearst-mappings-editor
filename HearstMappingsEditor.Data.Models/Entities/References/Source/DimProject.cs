using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
	[Table("DimProject", Schema = "Source")]
    public class DimProject : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long ProjectID { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(50)]
        public string ProjectCode { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(50)]
        public string ProjectGroup { get; set; }

        [ExcelExport(Order = 3)]
        [MaxLength(50)]
        public string ManagementProject { get; set; }

        [ExcelExport(Order = 4)]
        [MaxLength(50)]
        public string ManagementParent { get; set; }

        [ExcelExport(Order = 5)]
        [MaxLength(50)]
        public string ManagementBrand { get; set; }

        [ExcelExport(Order = 6)]
        [Column("Print Digital")]
        [MaxLength(50)]
        public string PrintDigital { get; set; }

        [ExcelExport(Order = 7)]
        [MaxLength(50)]
        public string Type { get; set; }

        [ExcelExport(Order = 8)]
        [Column("Описание")]
        [MaxLength(50)]
        public string Description { get; set; }

        [ExcelExport(Order = 9)]
        [MaxLength(50)]
        public string C1HypCode { get; set; }

        [ExcelExport(Order = 10)]
        [MaxLength(50)]
        public string C2HypCodeNew { get; set; }

        [ExcelExport(Order = 11)]
        [MaxLength(50)]
        public string C2Management { get; set; }

        [ExcelExport(Order = 12)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => ProjectID;
    }
}
