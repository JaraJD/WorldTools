using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Factory;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.Ports.UserPorts;

namespace WorldTools.Application.Queries.Factory
{
    public class UserUseCaseQueryFactory : IUserUseCaseQueryFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UserUseCaseQueryFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IUserRegisterUseCase Create()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IUserRegisterUseCase>();
        }
    }
}
