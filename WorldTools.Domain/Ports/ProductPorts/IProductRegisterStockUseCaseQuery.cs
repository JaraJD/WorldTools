using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ResponseVm.Product;

namespace WorldTools.Domain.Ports.ProductPorts
{
    public interface IProductRegisterStockUseCaseQuery
    {
        Task<ProductResponseVm> RegisterProductInventoryStock(string product);
    }
}
