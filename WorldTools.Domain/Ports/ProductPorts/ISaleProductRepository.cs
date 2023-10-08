using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports.ProductPorts
{
    public interface ISaleProductRepository
    {
        Task<SaleEntity> RegisterSaleAsync(SaleEntity sale);
    }
}
