using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Application.Gateway
{
    public interface IUserUseCase
    {
        Task<UserResponseVm> RegisterUser(RegisterUserCommand user);
    }
}
