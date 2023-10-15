using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.Application.Queries.UseCases.ProductUseCases
{
    public class GetProductsByBranchIdUseCaseQuery
    {
        private readonly IProductRepository _repository;

        public GetProductsByBranchIdUseCaseQuery(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponseVm>> GetAllProductsByBranchId(Guid branchId)
        {
            return await _repository.GetProductByBranchIdAsync(branchId);
        }
    }
}
