using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Infrastructure;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
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
            var branchToCreate = new RegisterBranchData(branch.BranchName.BranchName, branch.BranchLocation.Country, branch.BranchLocation.City);
            _context.Add(branchToCreate);
            await _context.SaveChangesAsync();

            return branchToCreate.BranchId;
        }
    }
}
