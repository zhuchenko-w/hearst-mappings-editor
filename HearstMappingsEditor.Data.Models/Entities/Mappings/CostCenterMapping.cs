using HearstMappingsEditor.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("CostCenterMapping", Schema = "Config")]
    public class CostCenterMapping : BaseEntity, IMapping
    {
        public long DeptID { get; set; }//--> Source.DimDept.DeptID

        [ExcelExport(Order = 1)]
        [MaxLength(32)]
        public string PrintDigital { get; set; }//('Print','Digital')

        public long CostCenterID { get; set; }//--> DimCostCenter.CostCenterID

        public DateTime? CreateDate { get; set; }

        [Key]
	    public long RowId { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedUser { get; set; }

        [NotMapped]
        public long Id => RowId;
    }
}
