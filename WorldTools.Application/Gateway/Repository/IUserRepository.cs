using WorldTools.Domain.Entities;

namespace WorldTools.Application.Gateway.Repository
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(UserEntity user);
    }
}
