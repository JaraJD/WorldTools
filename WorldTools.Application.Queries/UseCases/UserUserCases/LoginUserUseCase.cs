using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Application.Queries.UseCases.UserUserCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponse> LoginUser(LoginUserCommand user)
        {
            return await _userRepository.LoginUser(user);
        }
    }
}
