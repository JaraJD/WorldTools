using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IStoredEventRepository
    {
        Task<string> RegisterEvent(StoredEvent storedEvent);
    }
}
