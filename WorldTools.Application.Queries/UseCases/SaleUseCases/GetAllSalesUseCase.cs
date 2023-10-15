using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.Sale;

namespace WorldTools.Application.Queries.UseCases.SaleUseCases
{
    public class GetAllSalesUseCase
    {
        private readonly ISaleProductRepository _repository;

        public GetAllSalesUseCase(ISaleProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SaleResponseVm>> GetAllSales()
        {
            return await _repository.GetAllSales();
        }
    }
}
