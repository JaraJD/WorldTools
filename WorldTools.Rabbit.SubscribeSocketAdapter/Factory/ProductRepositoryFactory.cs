using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Factory;
using WorldTools.Domain.Ports.ProductPorts;

namespace WorldTools.Rabbit.SubscribeSocketAdapter.Factory
{
    public class ProductRepositoryFactory : IProductRepositoryFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProductRepositoryFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IProductRepository GetProductByIdAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IProductRepository>();
        }
    }
}
