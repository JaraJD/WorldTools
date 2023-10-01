using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IBranchRepository
    {
        Task<BranchEntity> RegisterBranchAsync(BranchEntity branch);

        Task<BranchEntity> GetBranchIdAsync();
    }
}
