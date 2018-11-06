using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("BrandMapping", Schema = "Config")]
    public class BrandMapping : BaseEntity, IMapping
    {
        public long ProjectID { get; set; }//--> Source.DimProject.ProjectID

        public long LOBDetailID { get; set; }//--> DimBrand.LOBDetailID

        public DateTime? CreateDate { get; set; }

        [Key]
		public long RowId { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedUser { get; set; }

        [NotMapped]
        public long Id => RowId;
    }
}
