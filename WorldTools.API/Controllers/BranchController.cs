using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.BranchCommands;

namespace WorldTools.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchUseCase _branchUseCase;

        public BranchController(IBranchUseCase branchUseCase)
        {
            _branchUseCase = branchUseCase;
        }

        [HttpPost]
        public async Task<string> RegisterBranch([FromBody] RegisterBranchCommand command)
        {
            return await _branchUseCase.RegisterBranch(command);
        }
    }
}
