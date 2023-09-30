using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.Application.Gateway
{
    public interface IProductUseCase
    {
        Task<RegisterProductCommand> RegisterProduct(RegisterProductCommand product);

        Task<ProductResponseVm> RegisterProductInventoryStock(RegisterProductInventoryCommand product, string idProduct);

        Task<ProductResponseVm> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product, string idProduct);

        Task<ProductResponseVm> RegisterResellerSale(RegisterSaleProductCommand product, string idProduct);
    }
}
