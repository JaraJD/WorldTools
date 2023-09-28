using WorldTools.Domain.DTO;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IBranchRepository
    {
        Task<string> RegisterBranchAsync(RegisterBranchDTO branch);
    }
}
