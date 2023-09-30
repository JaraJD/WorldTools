using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IUserRepository
    {
        Task<UserEntity> RegisterUserAsync(UserEntity user);
    }
}
