using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.Application.Queries.UseCases.ProductUseCases
{
    public class GetProductByIdUseCaseQuery
    {
        private readonly IProductRepository _repository;

        public GetProductByIdUseCaseQuery(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponseVm> GetProductById(Guid productId)
        {
            return await _repository.GetProductByIdAsync(productId);
        }
    }
}
