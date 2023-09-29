using AutoMapper;
using MongoDB.Driver;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Entities;
using WorldTools.MongoAdapter.DTO;
using WorldTools.MongoAdapter.Interfaces;

namespace WorldTools.MongoAdapter.Repositories
{
    public class StoredEventRepository : IStoredEventRepository
    {
        private readonly IMongoCollection<StoredEventDTO> _storedEvents;
        private readonly IMapper _mapper;

        public StoredEventRepository(IContextMongo dbContextMongo, IMapper mapper)
        {
            _storedEvents = dbContextMongo.StoredEvent;
            _mapper = mapper;
        }

        public async Task<string> RegisterEvent(StoredEvent storedEvent)
        {
            var eventToRegister = _mapper.Map<StoredEventDTO>(storedEvent);
            await _storedEvents.InsertOneAsync(eventToRegister);
            return "Event Registered";
        }
    }
}
