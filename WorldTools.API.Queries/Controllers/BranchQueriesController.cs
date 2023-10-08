using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchQueriesController : ControllerBase
    {
        private readonly GetBrachByIdUseCase _getBrachByIdUseCase;

        public BranchQueriesController(GetBrachByIdUseCase getBrachByIdUseCase)
        {
            _getBrachByIdUseCase = getBrachByIdUseCase;
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<BranchQueryVm> GetBranchById(Guid id)
        {
            return await _getBrachByIdUseCase.GetBranchById(id);
        }
    }
}
