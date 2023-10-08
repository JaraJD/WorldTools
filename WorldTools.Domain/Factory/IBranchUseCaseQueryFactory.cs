using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports;

namespace WorldTools.Domain.Factory
{
    public interface IBranchUseCaseQueryFactory
    {
        IBranchUseCaseQuery Create();
    }
}
