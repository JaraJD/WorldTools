
using Newtonsoft.Json;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Application.UseCases.UserUseCases
{
    public class RegisterUserUseCaseQuery : IUserRegisterUseCase
    {
        private readonly IUserRepository _repository;

        public RegisterUserUseCaseQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseVm> RegisterUser(string user)
        {
            UserEntity userToCreate = JsonConvert.DeserializeObject<UserEntity>(user);
            var userName = new UserValueObjectName(userToCreate.Name.FirstName, userToCreate.Name.LastName);
            var userPassword = new UserValueObjectPassword(userToCreate.UserPassword.UserPassword);
            var userEmail = new UserValueObjectEmail(userToCreate.Email.UserEmail);
            var userRole = new UserValueObjectRole(userToCreate.Role.Role);
            var userEntity = new UserEntity(userName, userPassword, userEmail, userRole, userToCreate.BranchId, userToCreate.Salt);

            var userResponse = await _repository.RegisterUserAsync(userEntity);

            var responseVm = new UserResponseVm();
            responseVm.Name = $"{userResponse.Name.FirstName} {userResponse.Name.LastName}";
            responseVm.Email = userResponse.Email.UserEmail;
            responseVm.UserPassword = userResponse.UserPassword.UserPassword;
            responseVm.Role = userResponse.Role.Role;
            responseVm.BranchId = userResponse.BranchId;
            responseVm.UserId = userResponse.UserId;

            return responseVm;
        }

    }
}
