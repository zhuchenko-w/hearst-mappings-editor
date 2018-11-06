using System;

namespace HearstMappingsEditor.Data.Models
{
    public interface IMapping : IEntity
    {
        long RowId { get; set; }
        DateTime? ModifiedOn { get; set; }
        string ModifiedUser { get; set; }
    }
}
