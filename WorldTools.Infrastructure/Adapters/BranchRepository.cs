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

        public Task<BranchEntity> GetBranchIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BranchEntity> RegisterBranchAsync(BranchEntity branch)
        {
            var branchToCreate = new RegisterBranchData(branch.BranchName.BranchName, branch.BranchLocation.Country, branch.BranchLocation.City);
            _context.Add(branchToCreate);
            await _context.SaveChangesAsync();


            branch.BranchId = branchToCreate.BranchId;
            return branch;
        }
    }
}
