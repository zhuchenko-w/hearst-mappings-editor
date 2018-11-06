namespace HearstMappingsEditor.Data.Models
{
    public interface IEntity
    {
        long Id { get; }
        bool? IsNew { get; set; }
    }
}
