﻿using WorldTools.Domain.Entities;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ResponseVm.User;

namespace WorldTools.Domain.Ports.UserPorts
{
    public interface IUserRepository
    {
        Task<UserEntity> RegisterUserAsync(UserEntity user);

        Task<UserQueryVm> GetUserByIdAsync(Guid UserId);

        Task<List<UserQueryVm>> GetAllUsersAsync();
    }
}