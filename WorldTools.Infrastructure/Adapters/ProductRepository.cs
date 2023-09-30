using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Infrastructure;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<ProductEntity> RegisterProductAsync(ProductEntity product)
        {
            var productToRegister = new RegisterProductData(
                product.ProductName.ProductName,
                product.ProductDescription.ProductDescription,
                product.ProductPrice.ProductPrice,
                product.ProductInventoryStock.ProductInventoryStock,
                product.ProductCategory,
                product.BranchId
                );

            _context.Add(productToRegister);
            await _context.SaveChangesAsync();

            product.ProductId = productToRegister.ProductId;
            return product;
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
