using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.DTO;

namespace WorldTools.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly Context _context;

        public BranchRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public Task<string> RegisterBranchAsync(RegisterBranchDTO branch)
        {
            throw new NotImplementedException();
        }
    }
}
