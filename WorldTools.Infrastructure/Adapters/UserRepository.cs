using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.User;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserRepository(Context dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserQueryVm>> GetAllUsersAsync()
        {
            var users = await _context.User.ToListAsync();

            var userResponseList = users.Select(user => _mapper.Map<UserQueryVm>(user)).ToList();

            return userResponseList;
        }

        public async Task<UserQueryVm> GetUserByIdAsync(Guid UserId)
        {
            var existingUser = await _context.User.FindAsync(UserId);

            if (existingUser == null)
            {
                throw new ArgumentNullException("El usuario no se encontro.");
            }

            return _mapper.Map<UserQueryVm>(existingUser);
        }

        public async Task<UserEntity> RegisterUserAsync(UserEntity user)
        {
            using (var context = new Context())
            {
                var userToRegistered = new RegisterUserData(
                $"{user.Name.FirstName} {user.Name.LastName}",
                user.UserPassword.UserPassword,
                user.Email.UserEmail,
                user.Role.Role,
                user.BranchId);

                context.Add(userToRegistered);
                await context.SaveChangesAsync();

                user.UserId = userToRegistered.UserId;
                return user;
            }
        }


    }
}
