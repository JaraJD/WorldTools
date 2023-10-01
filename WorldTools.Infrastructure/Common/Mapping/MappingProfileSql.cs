
using AutoMapper;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Common.Mapping
{
    public class MappingProfileSql : Profile
    {
        public MappingProfileSql()
        {
            CreateMap<RegisterProductData, ProductResponseVm>().ReverseMap();
        }
    }
}
