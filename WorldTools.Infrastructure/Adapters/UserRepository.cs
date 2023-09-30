using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> RegisterUserAsync(UserEntity user)
        {
            var userToRegistered = new RegisterUserData(
                user.Name.UserName,
                user.UserPassword.UserPassword,
                user.Email.UserEmail,
                user.Role,
                user.BranchId);

            _context.Add(userToRegistered);
            await _context.SaveChangesAsync();

            return userToRegistered.UserId;
        }
    }
}
