using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;
using WorldTools.SqlAdapter;
using WorldTools.SqlAdapter.Common.Secrets;
using WorldTools.SqlAdapter.DataEntity;
using WorldTools.SqlAdapter.Utils.Encrypt;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextSql _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserRepository(ContextSql dbContext, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = dbContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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

        public async Task<AuthResponse> LoginUser(LoginUserModel user)
        {
            AuthResponse authResponse = new AuthResponse();
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

            authResponse.UserEmail = userResponse.Email;
            authResponse.Token = GetToken(userResponse);
            authResponse.UserId = userResponse.UserId;
            authResponse.UserName = userResponse.Name;
            authResponse.Role = userResponse.Role;
            authResponse.BranchId = userResponse.BranchId;

            return authResponse;
        }

        public async Task<bool> EmailExists(string email)
        {
            using (var context = new ContextSql())
            {
                return await context.Users.AnyAsync(u => u.Email == email);
            }
        }

        private string GetToken(RegisterUserData userResponse)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userResponse.UserId.ToString()),
                        new Claim(ClaimTypes.Email, userResponse.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
