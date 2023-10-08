using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Factory;
using WorldTools.Domain.Ports.ProductPorts;

namespace WorldTools.Application.Queries.Factory
{
    public class ProductUseCaseQueryFactory : IProductUseCaseQueryFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProductUseCaseQueryFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IProductUseCaseQuery Create()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IProductUseCaseQuery>();
        }

        public IProductCustomerSaleUseCaseQuery RegisterCustomerSale()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IProductCustomerSaleUseCaseQuery>();
        }

        public IProductResellerSaleUseCaseQuery RegisterResellerSale()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IProductResellerSaleUseCaseQuery>();
        }

        public IProductRegisterStockUseCaseQuery RegisterProductStock()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IProductRegisterStockUseCaseQuery>();
        }
    }
}
