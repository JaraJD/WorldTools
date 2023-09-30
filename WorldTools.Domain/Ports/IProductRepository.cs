using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IProductRepository
    {
        Task<int> RegisterProductAsync(ProductEntity product);

        Task<string> RegisterProductInventoryStockAsync(RegisterProductInventoryCommand product);

        Task<string> RegisterProductFinalCustomerSaleAsync(RegisterSaleProductCommand product);

        Task<string> RegisterResellerSaleAsync(RegisterSaleProductCommand product);
    }
}
