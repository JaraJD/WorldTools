using MongoDB.Bson.Serialization.Attributes;

namespace WorldTools.MongoAdapter.MongoEntities
{
    public class StoredEventMongoEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string StoredId { get; set; }

        public string StoredName { get; set; }

        public string AggregateId { get; set; }

        public string EventBody { get; set; }
    }
}
