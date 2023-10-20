using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.UseCases.BranchUseCases;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/branch")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly RegisterBranchUseCase _registerBranchUseCase;

        public BranchController(RegisterBranchUseCase branchUseCase)
        {
            _registerBranchUseCase = branchUseCase;
        }

        [HttpPost("register")]
        public async Task<BranchResponseVm> RegisterBranch([FromBody] RegisterBranchCommand command)
        {
            return await _registerBranchUseCase.RegisterBranch(command);
        }
    }
}
