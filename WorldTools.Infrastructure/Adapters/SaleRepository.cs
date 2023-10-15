using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Sale;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
{
    public class SaleRepository : ISaleProductRepository
    {
        private readonly ContextSql _context;
        private readonly IMapper _mapper;

        public SaleRepository(ContextSql dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SaleResponseVm>> GetAllSales()
        {
            var sales = await _context.Sale.ToListAsync();

            var saleResponseList = sales.Select(sale => _mapper.Map<SaleResponseVm>(sale)).ToList();

            return saleResponseList;
        }

        public async Task<List<SaleResponseVm>> GetAllSalesByBranchId(Guid branchId)
        {
            var sales = await _context.Sale
                .Where(sale => sale.BranchId == branchId)
                .ToListAsync();

            var saleResponseList = _mapper.Map<List<SaleResponseVm>>(sales);
            return saleResponseList;
        }

        public async Task<SaleEntity> RegisterSaleAsync(SaleEntity sale)
        {

            using (var context = new ContextSql())
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
