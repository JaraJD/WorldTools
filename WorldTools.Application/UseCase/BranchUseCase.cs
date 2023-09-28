using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.BranchCommands;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Application.UseCase
{
    public class BranchUseCase : IBranchUseCase
    {
        private readonly IBranchRepository _repository;
        private readonly IStoredEventRepository _storedEvent;
        public static event EventHandler MyEvent;

        public BranchUseCase(IBranchRepository repository, IStoredEventRepository storedEvent)
        {
            _repository = repository;
            _storedEvent = storedEvent;
        }

        public Task<string> RegisterBranch(RegisterBranchCommand branch)
        {
            var branchName = new BranchValueObjectName(branch.BranchName);
            var branchLocation = new BranchValueObjectLocation(branch.BranchCountry, branch.BranchCity);
            var branchEntity = new BranchEntity(branchName, branchLocation);

            //Observable.FromEventPattern(
            //    e => MyEvent += e,
            //    e => MyEvent -= e
            //    );
            RegisterEvent(branch);
            return _repository.RegisterBranchAsync(branchEntity);
        }

        public Task<string> RegisterEvent(RegisterBranchCommand branch)
        {
            var eventToRegister = new StoredEvent();

            eventToRegister.StoredName = "Branch created";
            eventToRegister.EventBody = JsonConvert.SerializeObject(branch);

            return _storedEvent.RegisterEvent(eventToRegister);
        }
    }
}