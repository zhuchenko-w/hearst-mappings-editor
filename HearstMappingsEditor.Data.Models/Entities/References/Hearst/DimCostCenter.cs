using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("DimCostCenter", Schema = "dbo")]
    public class DimCostCenter : BaseEntity, IReference
    {
        [Key]
        public long CostCenterID { get; set; }

        public long? AllCostCentersID { get; set; }

        [MaxLength(255)]
        public string AllCostCentersDesc { get; set; }

        [MaxLength(255)]
        public string CostCenterDesc { get; set; }

        [MaxLength(50)]
        public string CostCenterCode { get; set; }

        public DateTime? CreateDate { get; set; }

        [NotMapped]
        public long Id => CostCenterID;
    }
}
