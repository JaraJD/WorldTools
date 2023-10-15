
using Newtonsoft.Json;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Domain.ResponseVm.User;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Application.UseCases.UserUseCases
{
    public class RegisterUserUseCase
    {
        private readonly IPublishEventRepository _publishEventRepository;
        private readonly IStoredEventRepository _storedEvent;
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IPublishEventRepository publishEventRepository, IStoredEventRepository storedEvent, IUserRepository userRepository)
        {
            _publishEventRepository = publishEventRepository;
            _storedEvent = storedEvent;
            _userRepository = userRepository;
        }

        public async Task<UserResponseVm> RegisterUser(RegisterUserCommand user, string salt)
        {
            bool emailExists = await _userRepository.EmailExists(user.Email);

            if (emailExists)
            {
                throw new ArgumentException("El correo electrónico ya está registrado.");
            }

            var userName = new UserValueObjectName(user.Name.FirstName, user.Name.LastName);
            var userPassword = new UserValueObjectPassword(user.UserPassword);
            var userEmail = new UserValueObjectEmail(user.Email);
            var userRole = new UserValueObjectRole(user.Role);
            var userEntity = new UserEntity(userName, userPassword, userEmail, userRole, user.BranchId, salt);

            var responseVm = new UserResponseVm();

            responseVm.Name = $"{user.Name.FirstName} {user.Name.LastName}";
            responseVm.Email = user.Email;
            responseVm.UserPassword = user.UserPassword;
            responseVm.Role = user.Role;
            responseVm.BranchId = user.BranchId;
            responseVm.UserId = userEntity.UserId;

            var eventResponse = await RegisterAndPersistEvent("UserRegistered", userEntity.BranchId, userEntity);

            _publishEventRepository.PublishRegisterUser(eventResponse);

            return responseVm;
        }

        public async Task<StoredEvent> RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));
            await _storedEvent.RegisterEvent(storedEvent);
            return storedEvent;
        }
    }
}
