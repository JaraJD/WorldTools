using WorldTools.Domain.Entities;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IStoredEventRepository
    {
        Task<String> RegisterEvent(StoredEvent storedEvent);
    }
}
