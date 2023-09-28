using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.DTO;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IUserRepository
    {
        Task<string> RegisterBranchAsync(RegisterUserDTO user);
    }
}
