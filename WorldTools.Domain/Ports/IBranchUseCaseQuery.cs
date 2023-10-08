using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Domain.Ports
{
    public interface IBranchUseCaseQuery
    {
        Task<BranchResponseVm> RegisterBranch(string branch);
    }
}
