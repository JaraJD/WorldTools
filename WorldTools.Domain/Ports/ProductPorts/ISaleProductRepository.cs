using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ResponseVm.Sale;

namespace WorldTools.Domain.Ports.ProductPorts
{
    public interface ISaleProductRepository
    {
        Task<SaleEntity> RegisterSaleAsync(SaleEntity sale);

        Task<List<SaleResponseVm>> GetAllSales();

        Task<List<SaleResponseVm>> GetAllSalesByBranchId(Guid branchId);
    }
}
