using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Domain.Ports.UserPorts
{
    public interface IUserRegisterUseCase
    {
        Task<UserResponseVm> RegisterUser(string user);
    }
}
