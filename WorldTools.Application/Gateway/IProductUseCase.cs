using WorldTools.Domain.Commands.ProductCommands;

namespace WorldTools.Application.Gateway
{
    public interface IProductUseCase
    {
        Task<int> RegisterProduct(RegisterProductCommand product);

        Task<string> RegisterProductInventoryStock(RegisterProductInventoryCommand product);

        Task<string> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product);

        Task<string> RegisterResellerSale(RegisterSaleProductCommand product);
    }
}
