using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using WorldTools.API.Utils.Encrypt;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpPost("register")]
        public async Task<UserResponseVm> RegisterUser([FromBody] RegisterUserCommand command)
        {
            string salt = PasswordEncryption.GenerateSalt();
            string hashedPassword = PasswordEncryption.EncryptPassword(command.UserPassword, salt);

            command.UserPassword = hashedPassword;
            return await _userUseCase.RegisterUser(command);
        }
    }
}
