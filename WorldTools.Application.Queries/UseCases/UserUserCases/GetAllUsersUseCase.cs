using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Application.Queries.UseCases.UserUserCases
{
    public class GetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserQueryVm>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
