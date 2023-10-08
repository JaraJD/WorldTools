using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Application.Queries.UseCases.UserUserCases
{
    public class GetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserQueryVm> GetUserById(Guid UserId)
        {
            return await _userRepository.GetUserByIdAsync(UserId);
        }
    }
}
