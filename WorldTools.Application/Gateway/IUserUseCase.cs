using WorldTools.Domain.Commands.UserCommands;

namespace WorldTools.Application.Gateway
{
    public interface IUserUseCase
    {
        Task<string> RegisterUser(RegisterUserCommand user);
    }
}
