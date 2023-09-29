using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.DTO;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly Context _context;

        public BranchRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> RegisterBranchAsync(BranchEntity branch)
        {
            var branchToCreate = new RegisterBranchDTO(branch.BranchName.BranchName, branch.BranchLocation.Country, branch.BranchLocation.City);

            _context.Add(branchToCreate);
            await _context.SaveChangesAsync();

            return branchToCreate.BranchId;
        }
    }
}
