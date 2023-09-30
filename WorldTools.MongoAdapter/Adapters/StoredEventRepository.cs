using AutoMapper;
using MongoDB.Driver;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.MongoAdapter.Interfaces;
using WorldTools.MongoAdapter.MongoEntities;

namespace WorldTools.MongoAdapter.Adapters
{
    public class StoredEventRepository : IStoredEventRepository
    {
        private readonly IMongoCollection<StoredEventMongoEntity> _storedEvents;
        private readonly IMapper _mapper;

        public StoredEventRepository(IContextMongo dbContextMongo, IMapper mapper)
        {
            _storedEvents = dbContextMongo.StoredEvent;
            _mapper = mapper;
        }

        public async Task<string> RegisterEvent(StoredEvent storedEvent)
        {
            var eventToRegister = _mapper.Map<StoredEventMongoEntity>(storedEvent);
            await _storedEvents.InsertOneAsync(eventToRegister);
            return "Event Registered";
        }
    }
}
