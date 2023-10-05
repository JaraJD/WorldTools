using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using WorldTools.API.Utils.Encrypt;
using WorldTools.Application.UseCases.UserUseCases;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;

        public UserController(RegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        [HttpPost("register")]
        public async Task<UserResponseVm> RegisterUser([FromBody] RegisterUserCommand command)
        {
            string salt = PasswordEncryption.GenerateSalt();
            string hashedPassword = PasswordEncryption.EncryptPassword(command.UserPassword, salt);

            command.UserPassword = hashedPassword;
            return await _registerUserUseCase.RegisterUser(command);
        }
    }
}
