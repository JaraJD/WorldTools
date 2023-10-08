using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Application.Queries.UseCases.UserUserCases;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.API.Queries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQueriesController : ControllerBase
    {
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly GetAllUsersUseCase _getAllUsersUseCase;

        public UserQueriesController(GetUserByIdUseCase getUserByIdUseCase, GetAllUsersUseCase getAllUsersUseCase)
        {
            _getUserByIdUseCase = getUserByIdUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<UserQueryVm> GetUserById(Guid id)
        {
            return await _getUserByIdUseCase.GetUserById(id);
        }

        [HttpGet("GetAllUsers")]
        public async Task<List<UserQueryVm>> GetAllUsers()
        {
            return await _getAllUsersUseCase.GetAllUsers();
        }
    }
}
