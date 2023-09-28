using WorldTools.Domain.DTO;
using WorldTools.Domain.Entities;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IBranchRepository
    {
        Task<string> RegisterBranchAsync(BranchEntity branch);
    }
}
