using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Application.Queries.UseCases.SaleUseCases;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.Sale;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/v1/branch/")]
    [ApiController]
    public class BranchQueriesController : ControllerBase
    {
        private readonly GetBrachByIdUseCase _getBrachByIdUseCase;
        private readonly GetAllBranchUseCase _getAllBranch;
        private readonly GetAllSalesByBranchIdUseCase _getSalesByBranch;

        public BranchQueriesController(
            GetBrachByIdUseCase getBrachByIdUseCase,
            GetAllBranchUseCase getAllBranch,
            GetAllSalesByBranchIdUseCase getSalesByBranch)
        {
            _getBrachByIdUseCase = getBrachByIdUseCase;
            _getAllBranch = getAllBranch;
            _getSalesByBranch = getSalesByBranch;
        }

        [HttpGet("GetBranch/{id}")]
        public async Task<BranchQueryVm> GetBranchById(Guid id)
        {
            return await _getBrachByIdUseCase.GetBranchById(id);
        }

        [HttpGet("GetAllBranch")]
        public async Task<List<BranchQueryVm>> GetAllBranch()
        {
            return await _getAllBranch.GetAllBranches();
        }

        [HttpGet("GetSalesBranch/{branchId}")]
        public async Task<List<SaleResponseVm>> GetSalesByBranch(Guid branchId)
        {
            return await _getSalesByBranch.GetAllSalesByBranchId(branchId);
        }
    }
}
