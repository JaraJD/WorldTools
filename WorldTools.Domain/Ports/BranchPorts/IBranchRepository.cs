using WorldTools.Domain.Entities;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Domain.Ports.BranchPorts
{
    public interface IBranchRepository
    {
        Task<BranchEntity> RegisterBranchAsync(BranchEntity branch);

        Task<BranchQueryVm> GetBranchByIdAsync(Guid branchId);

        Task<List<BranchQueryVm>> GetAllBranchesAsync();
    }
}
