using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IBranchRepository
    {
        Task<int> RegisterBranchAsync(BranchEntity branch);
    }
}
