using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.UserCommands;

namespace WorldTools.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _repository;

        public UserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<string> RegisterUser(RegisterUserCommand user)
        {
            throw new NotImplementedException();
        }
    }
}
