using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(UserEntity user);
    }
}
