using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    [Table("EntityMapping", Schema = "Config")]
    public class EntityMapping : BaseEntity, IMapping
    {
        public long? PerimeterID { get; set; }//--> Source.DimOrgStructure.PerimeterID. Либо использовать вьюшку vPerimeter, будет удобнее.

        public long? EntityID { get; set; }//--> DimEntity.EntityID

        [Key]
		public long RowId { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedUser { get; set; }

        [NotMapped]
        public long Id => RowId;
    }
}
