using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Application.Queries.UseCases.UserUserCases;
using WorldTools.Application.UseCases.UserUseCases;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserQueriesController : ControllerBase
    {
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly GetAllUsersUseCase _getAllUsersUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;

        public UserQueriesController(GetUserByIdUseCase getUserByIdUseCase, GetAllUsersUseCase getAllUsersUseCase, LoginUserUseCase loginUserUseCase)
        {
            _getUserByIdUseCase = getUserByIdUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpGet("GetUser/{id}")]
        [Authorize]
        public async Task<UserQueryVm> GetUserById(Guid id)
        {
            return await _getUserByIdUseCase.GetUserById(id);
        }

        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<List<UserQueryVm>> GetAllUsers()
        {
            return await _getAllUsersUseCase.GetAllUsers();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var response = await _loginUserUseCase.LoginUser(command);
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
