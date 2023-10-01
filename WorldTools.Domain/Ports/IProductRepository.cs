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

        Task<ProductResponseVm> RegisterProductFinalCustomerSaleAsync(ProductValueObjectInventoryStock product, Guid productId);

        Task<ProductResponseVm> RegisterResellerSaleAsync(ProductValueObjectInventoryStock product, Guid productId);
    }
}
