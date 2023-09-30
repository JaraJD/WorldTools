using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Application.Gateway
{
    public interface IBranchUseCase
    {
        Task<BranchResponseVm> RegisterBranch(RegisterBranchCommand branch);
    }
}
