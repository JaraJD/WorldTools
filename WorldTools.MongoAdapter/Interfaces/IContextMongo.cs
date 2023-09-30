using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.MongoAdapter.DTO;

namespace WorldTools.MongoAdapter.Interfaces
{
    public interface IContextMongo
    {
        public IMongoCollection<StoredEventMongoEntity> StoredEvent { get; }
    }
}
