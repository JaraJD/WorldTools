using MongoDB.Driver;
using WorldTools.MongoAdapter.DTO;
using WorldTools.MongoAdapter.Interfaces;

namespace WorldTools.MongoAdapter
{
    public class ContextMongo : IContextMongo
    {
        private readonly IMongoDatabase _database;

        public ContextMongo(string stringConnection, string DBname)
        {
            MongoClient client = new MongoClient(stringConnection);
            _database = client.GetDatabase(DBname);
        }

        public IMongoCollection<StoredEventMongoEntity> StoredEvent => _database.GetCollection<StoredEventMongoEntity>("storedEevnt");
    }
}
