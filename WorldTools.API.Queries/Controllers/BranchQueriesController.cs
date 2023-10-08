using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/v1/branch/")]
    [ApiController]
    public class BranchQueriesController : ControllerBase
    {
        private readonly GetBrachByIdUseCase _getBrachByIdUseCase;
        private readonly GetAllBranchUseCase _getAllBranch;

        public BranchQueriesController(GetBrachByIdUseCase getBrachByIdUseCase, GetAllBranchUseCase getAllBranch)
        {
            _getBrachByIdUseCase = getBrachByIdUseCase;
            _getAllBranch = getAllBranch;
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
    }
}
