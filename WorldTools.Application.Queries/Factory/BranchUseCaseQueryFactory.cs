using Microsoft.Extensions.DependencyInjection;
using WorldTools.Domain.Factory;
using WorldTools.Domain.Ports;

namespace WorldTools.Application.Queries.Factory
{
    public class BranchUseCaseQueryFactory : IBranchUseCaseQueryFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BranchUseCaseQueryFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IBranchUseCaseQuery Create()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IBranchUseCaseQuery>();
        }
    }

}
