using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.Application.Gateway
{
    public interface IProductUseCase
    {
        Task<RegisterProductCommand> RegisterProduct(RegisterProductCommand product);

        Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product, Guid idProduct);

        Task<ProductResponseVm> RegisterProductFinalCustomerSale(ProductSaleCommand product, Guid idProduct);

        Task<ProductResponseVm> RegisterResellerSale(ProductSaleCommand product, Guid idProduct);
    }
}
