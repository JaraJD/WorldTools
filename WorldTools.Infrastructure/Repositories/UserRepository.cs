using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.DTO;

namespace WorldTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context dbContext)
        {
            _context = dbContext;
        }

        public Task<string> RegisterBranchAsync(RegisterUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
