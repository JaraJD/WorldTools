using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.DTO;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IProductRepository
    {
        Task<string> RegisterProductAsync(RegisterProductDTO product);

        Task<string> RegisterProductInventoryStockAsync(RegisterProductInventoryCommand product);

        Task<string> RegisterProductFinalCustomerSaleAsync(RegisterSaleProductCommand product);

        Task<string> RegisterResellerSaleAsync(RegisterSaleProductCommand product);
    }
}
