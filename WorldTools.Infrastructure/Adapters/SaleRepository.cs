using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
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
            var saleToCreate = new RegisterSaleData(
                sale.SaleValueNumber.Number,
                sale.SaleValueQuantity.Quantity,
                sale.saleValueObjectTotal.TotalPrice,
                sale.saleValueObjectType.SaleType,
                sale.BranchId
                );

            _context.Add(saleToCreate);
            await _context.SaveChangesAsync();


            return sale;
        }
    }
}
