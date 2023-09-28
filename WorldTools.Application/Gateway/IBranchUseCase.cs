using WorldTools.Domain.Commands.BranchCommands;

namespace WorldTools.Application.Gateway
{
    public interface IBranchUseCase
    {
        Task<string> RegisterBranch(RegisterBranchCommand branch);
    }
}
