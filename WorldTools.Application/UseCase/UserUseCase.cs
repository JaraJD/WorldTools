using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.ProductCommands;
using WorldTools.Domain.Commands.UserCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.UserValueObjects;

namespace WorldTools.Application.UseCase
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public UserUseCase(IUserRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<int> RegisterUser(RegisterUserCommand user)
        {
            var userName = new UserValueObjectName(user.Name);
            var userPassword = new UserValueObjectPassword(user.UserPassword);
            var userEmail = new UserValueObjectEmail(user.Email);
            var userEntity = new UserEntity(userName, userPassword, userEmail, user.Role, user.BranchId);

            var userId = await _repository.RegisterUserAsync(userEntity);
            await RegisterAndPersistEvent("UserRegistered", userId, user);

            return userId;
        }

        public async Task RegisterAndPersistEvent(string eventName, int aggregateId, RegisterUserCommand eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
