using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ValueObjects.ProductValueObjects;

namespace WorldTools.Domain.Ports
{
    public interface IProductRepository
    {
        Task<ProductEntity> RegisterProductAsync(ProductEntity product);

        Task<ProductResponseVm> RegisterProductInventoryStockAsync(ProductValueObjectInventoryStock product, Guid productId);

        Task<ProductResponseVm> RegisterProductFinalCustomerSaleAsync(ProductSaleCommand product);

        Task<ProductResponseVm> RegisterResellerSaleAsync(ProductSaleCommand product);

        Task<ProductResponseVm> GetProductById(Guid productId);
    }
}
