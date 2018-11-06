using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Used for entities with non-auto-increment PK. 
        /// When such entity is being saved, value of IsNew is checked, and if it's True, 
        /// the repository will take an attempt to add the entity as a new one
        /// </summary>
        [NotMapped]
        public bool? IsNew { get; set; }
    }
}
