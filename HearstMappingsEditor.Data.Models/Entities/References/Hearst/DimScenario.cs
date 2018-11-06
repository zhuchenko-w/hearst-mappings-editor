using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimScenario", Schema = "dbo")]
    public class DimScenario : BaseEntity, IReference
    {
        [ExcelExport(Order = 0)]
        [Key]
        public long ScenarioID { get; set; }

        public long? AllScenariosID { get; set; }

        [MaxLength(255)]
        public string AllScenariosDesc { get; set; }

        [ExcelExport(Order = 1)]
        [MaxLength(255)]
        public string ScenarioDesc { get; set; }

        [ExcelExport(Order = 2)]
        [MaxLength(10)]
        public string ScenarioCode { get; set; }

        [ExcelExport(Order = 3)]
        public int? MonthNum { get; set; }

        [ExcelExport(Order = 5)]
        [MaxLength(50)]
        public string InputCode { get; set; }

        [ExcelExport(Order = 5)]
        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => ScenarioID;
    }
}
