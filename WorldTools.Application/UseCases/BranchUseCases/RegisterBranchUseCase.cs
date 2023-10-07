﻿using Newtonsoft.Json;
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
        private readonly IPublishEventRepository _publishEventRepository;
        private readonly IStoredEventRepository _storedEvent;

        public RegisterBranchUseCase(IBranchRepository repository, IStoredEventRepository storedEvent, IPublishEventRepository publishEventRepository)
        {
            _repository = repository;
            _storedEvent = storedEvent;
            _publishEventRepository = publishEventRepository;
        }

        public async Task<BranchResponseVm> RegisterBranch(RegisterBranchCommand branch)
        {
            var branchName = new BranchValueObjectName(branch.BranchName);
            var branchLocation = new BranchValueObjectLocation(branch.BranchLocation.Country, branch.BranchLocation.City);
            var branchEntity = new BranchEntity(branchName, branchLocation);

            var responseVm = new BranchResponseVm();
            responseVm.BranchId = branchEntity.BranchId;
            responseVm.BranchLocation = $"{branchEntity.BranchLocation.City}, {branchEntity.BranchLocation.Country}";
            responseVm.BranchName = branchEntity.BranchName.BranchName;

            // Registro del evento
            await RegisterAndPersistEvent("BranchRegistered", branchEntity.BranchId, branchEntity);

            _publishEventRepository.Publish(branchEntity);

            return responseVm;
        }

        public async Task RegisterAndPersistEvent(string eventName, Guid aggregateId, Object eventBody)
        {
            var storedEvent = new StoredEvent(eventName, aggregateId, JsonConvert.SerializeObject(eventBody));

            await _storedEvent.RegisterEvent(storedEvent);
        }
    }
}
