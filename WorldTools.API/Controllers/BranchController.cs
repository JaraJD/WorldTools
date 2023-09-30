using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchUseCase _branchUseCase;

        public BranchController(IBranchUseCase branchUseCase)
        {
            _branchUseCase = branchUseCase;
        }

        [HttpPost("register")]
        public async Task<BranchResponseVm> RegisterBranch([FromBody] RegisterBranchCommand command)
        {
            return await _branchUseCase.RegisterBranch(command);
        }
    }
}
