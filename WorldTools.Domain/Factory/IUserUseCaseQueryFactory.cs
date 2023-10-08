using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.Ports.UserPorts;

namespace WorldTools.Domain.Factory
{
    public interface IUserUseCaseQueryFactory
    {
        IUserRegisterUseCase Create();
    }
}
