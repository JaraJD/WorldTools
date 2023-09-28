using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.DTO;

namespace WorldTools.Application.Gateway
{
    public interface IProductUseCase
    {
        Task<string> RegisterProduct(RegisterProductCommand product);

        Task<string> RegisterProductInventoryStock(RegisterProductInventoryCommand product);

        Task<string> RegisterProductFinalCustomerSale(RegisterSaleProductCommand product);

        Task<string> RegisterResellerSale(RegisterSaleProductCommand product);
    }
}
