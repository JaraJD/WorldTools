using Newtonsoft.Json;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Application.UseCases.BranchUseCases
{
    public class RegisterBranchUseCase
    {
        private readonly IBranchRepository _repository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterBranchUseCase(IBranchRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public async Task<BranchResponseVm> RegisterBranch(RegisterBranchCommand branch)
        {
            var branchName = new BranchValueObjectName(branch.BranchName);
            var branchLocation = new BranchValueObjectLocation(branch.BranchLocation.Country, branch.BranchLocation.City);
            var branchEntity = new BranchEntity(branchName, branchLocation);
            var branchResponse = await _repository.RegisterBranchAsync(branchEntity);

            var responseVm = new BranchResponseVm();
            responseVm.BranchId = branchResponse.BranchId;
            responseVm.BranchLocation = $"{branchResponse.BranchLocation.City}, {branchResponse.BranchLocation.Country}";
            responseVm.BranchName = branchResponse.BranchName.BranchName;

            // Registro del evento
            await RegisterAndPersistEvent("BranchRegistered", branchResponse.BranchId, branch);

            return responseVm;
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, RegisterBranchCommand eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
