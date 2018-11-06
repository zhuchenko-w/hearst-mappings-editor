using System.Threading.Tasks;

namespace HearstMappingsEditor.Data.Repository.Interfaces
{
    public interface IMappingSyncLogic
    {
        Task<bool> GetSyncMappingState();
        Task<int> Sync();
    }
}
