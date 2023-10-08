using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Infrastructure;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
{
    public class SaleRepository : ISaleProductRepository
    {
        private readonly Context _context;

        public SaleRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<SaleEntity> RegisterSaleAsync(SaleEntity sale)
        {

            using (var context = new Context())
            {
                var saleToCreate = new RegisterSaleData(
                sale.SaleValueNumber.Number,
                sale.SaleValueQuantity.Quantity,
                sale.saleValueObjectTotal.TotalPrice,
                sale.saleValueObjectType.SaleType,
                sale.BranchId
                );

                context.Add(saleToCreate);
                await context.SaveChangesAsync();


                return sale;

            }
            
        }
    }
}
