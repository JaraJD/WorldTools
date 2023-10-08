using WorldTools.Domain.Entities;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Domain.Ports
{
    public interface IBranchRepository
    {
        Task<BranchEntity> RegisterBranchAsync(BranchEntity branch);

        Task<BranchQueryVm> GetBranchByIdAsync(Guid branchId);
    }
}
