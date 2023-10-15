using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.ResponseVm.Sale;

namespace WorldTools.Application.Queries.UseCases.SaleUseCases
{
    public class GetAllSalesByBranchIdUseCase
    {
        private readonly ISaleProductRepository _repository;

        public GetAllSalesByBranchIdUseCase(ISaleProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SaleResponseVm>> GetAllSalesByBranchId(Guid branchId)
        {
            return await _repository.GetAllSalesByBranchId(branchId);
        }
    }
}
