using Newtonsoft.Json;
using WorldTools.Application.Gateway;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Application.UseCase
{
    public class BranchUseCase : IBranchUseCase
    {
        private readonly IBranchRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public BranchUseCase(IBranchRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<int> RegisterBranch(RegisterBranchCommand branch)
        {
            var branchName = new BranchValueObjectName(branch.BranchName);
            var branchLocation = new BranchValueObjectLocation(branch.BranchCountry, branch.BranchCity);
            var branchEntity = new BranchEntity(branchName, branchLocation);
            var branchId = await _repository.RegisterBranchAsync(branchEntity);

            // Registro del evento
            await RegisterAndPersistEvent("BranchRegistered", branchId, branch);

            return branchId;
        }

        public async Task RegisterAndPersistEvent(string eventName, int aggregateId, RegisterBranchCommand eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}