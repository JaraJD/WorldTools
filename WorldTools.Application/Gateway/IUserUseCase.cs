using WorldTools.Domain.Commands.UserCommands;

namespace WorldTools.Application.Gateway
{
    public interface IUserUseCase
    {
        Task<int> RegisterUser(RegisterUserCommand user);
    }
}
