using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.ResponseVm.Sale;

namespace WorldTools.Domain.Ports.ProductPorts
{
    public interface IProductCustomerSaleUseCaseQuery
    {
        Task<SaleResponseVm> RegisterProductFinalCustomerSale(string product);
    }
}
