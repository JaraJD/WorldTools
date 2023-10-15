using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Authentication;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.Product;
using WorldTools.Domain.ResponseVm.User;
using WorldTools.SqlAdapter;
using WorldTools.SqlAdapter.DataEntity;
using WorldTools.SqlAdapter.Utils.Encrypt;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextSql _context;
        private readonly IMapper _mapper;

        public UserRepository(ContextSql dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserQueryVm>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            var userResponseList = users.Select(user => _mapper.Map<UserQueryVm>(user)).ToList();

            return userResponseList;
        }

        public async Task<UserQueryVm> GetUserByIdAsync(Guid UserId)
        {
            var existingUser = await _context.Users.FindAsync(UserId);

            if (existingUser == null)
            {
                throw new ArgumentNullException("El usuario no se encontro.");
            }

            return _mapper.Map<UserQueryVm>(existingUser);
        }

        public async Task<UserEntity> RegisterUserAsync(UserEntity user)
        {
            using (var context = new ContextSql())
            {
                var salt = PasswordEncryption.GenerateSalt();
                var userToRegistered = new RegisterUserData(
                $"{user.Name.FirstName} {user.Name.LastName}",
                user.UserPassword.UserPassword,
                user.Email.UserEmail,
                user.Role.Role,
                user.BranchId,
                user.Salt);

                context.Add(userToRegistered);
                await context.SaveChangesAsync();

                user.UserId = userToRegistered.UserId;
                return user;
            }
        }

        public async Task<UserQueryVm> LoginUser(LoginUserCommand user)
        {
            var userResponse = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.UserEmail);

            if (userResponse == null)
            {
                throw new ArgumentNullException("Usuario no encontrado.");
            }

            if (!PasswordEncryption.VerifyPassword(user.Password, userResponse.StoreSalt, userResponse.UserPassword))
            {
                throw new AuthenticationException("Usuario o Contraseña incorrecta.");
            }

            return _mapper.Map<UserQueryVm>(userResponse);
        }

        public async Task<bool> EmailExists(string email)
        {
            using (var context = new ContextSql())
            {
                return await context.Users.AnyAsync(u => u.Email == email);
            }
        }
    }
}
