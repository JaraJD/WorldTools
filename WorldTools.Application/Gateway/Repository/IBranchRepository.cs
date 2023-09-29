using WorldTools.Domain.Entities;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IBranchRepository
    {
        Task<int> RegisterBranchAsync(BranchEntity branch);
    }
}
