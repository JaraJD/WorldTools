using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.DTO;

namespace WorldTools.Application.Gateway
{
    public interface IUserUseCase
    {
        Task<string> RegisterUser(RegisterUserCommand user);
    }
}
