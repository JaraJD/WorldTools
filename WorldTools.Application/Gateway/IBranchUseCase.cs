using WorldTools.Domain.Commands.BranchCommands;

namespace WorldTools.Application.Gateway
{
    public interface IBranchUseCase
    {
        Task<int> RegisterBranch(RegisterBranchCommand branch);
    }
}
