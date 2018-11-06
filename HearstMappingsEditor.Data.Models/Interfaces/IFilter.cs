
namespace HearstMappingsEditor.Data.Models
{
    public interface IFilter
    {
        int? Skip { get; set; }
        int? Take { get; set; }
    }
}
