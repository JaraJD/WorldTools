using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public Task<string> RegisterBranchAsync(UserEntity user)
        {
            throw new NotImplementedException();
        }
    }
}
