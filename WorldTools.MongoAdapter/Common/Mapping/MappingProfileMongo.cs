using AutoMapper;
using WorldTools.Domain.Entities;
using WorldTools.MongoAdapter.MongoEntities;

namespace WorldTools.MongoAdapter.Common.Mapping
{
    public class MappingProfileMongo : Profile
    {
        public MappingProfileMongo()
        {
            CreateMap<StoredEventMongoEntity, StoredEvent>().ReverseMap();
        }
    }
}
