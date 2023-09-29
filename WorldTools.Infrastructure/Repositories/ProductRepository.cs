using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.DTO;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> RegisterProductAsync(ProductEntity product)
        {
            var productToRegister = new RegisterProductDTO(
                product.ProductName.ProductName,
                product.ProductDescription.ProductDescription,
                product.ProductPrice.ProductPrice,
                product.ProductInventoryStock.ProductInventoryStock,
                product.ProductCategory.ToString(),
                product.BranchId
                );

            _context.Add(productToRegister);
            await _context.SaveChangesAsync();

            return productToRegister.ProductId;
        }

        public Task<string> RegisterProductFinalCustomerSaleAsync(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterProductInventoryStockAsync(RegisterProductInventoryCommand product)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegisterResellerSaleAsync(RegisterSaleProductCommand product)
        {
            throw new NotImplementedException();
        }
    }
}
