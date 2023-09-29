using WorldTools.Domain.Entities;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IUserRepository
    {
        Task<string> RegisterBranchAsync(UserEntity user);
    }
}
