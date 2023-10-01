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

        public async Task<UserEntity> RegisterUserAsync(UserEntity user)
        {
            var userToRegistered = new RegisterUserData(
                $"{user.Name.FirstName} {user.Name.LastName}",
                user.UserPassword.UserPassword,
                user.Email.UserEmail,
                user.Role.Role,
                user.BranchId);

            _context.Add(userToRegistered);
            await _context.SaveChangesAsync();

            user.UserId = userToRegistered.UserId;
            return user;
        }
    }
}
