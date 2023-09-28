using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.ProductCommands;

namespace WorldTools.Application.UseCase
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IProductRepository _repository;

        public ProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<string> RegisterProduct(RegisterProductCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterProductInventoryStock(RegisterProductInventoryCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterResellerSale(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }
    }
}
