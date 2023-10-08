using Newtonsoft.Json;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Domain.ValueObjects.BranchValueObjects;

namespace WorldTools.Application.Queries.UseCases.BranchUseCases
{
    public class RegisterBranchUseCaseQuery : IBranchUseCaseQuery
    {
        private readonly IBranchRepository _repository;

        public RegisterBranchUseCaseQuery(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<BranchResponseVm> RegisterBranch(string branch)
        {
            BranchEntity branchToCreate = JsonConvert.DeserializeObject<BranchEntity>(branch);
            var branchName = new BranchValueObjectName(branchToCreate.BranchName.BranchName);
            var branchLocation = new BranchValueObjectLocation(branchToCreate.BranchLocation.Country, branchToCreate.BranchLocation.City);
            var branchEntity = new BranchEntity(branchName, branchLocation);
            await _repository.RegisterBranchAsync(branchEntity);

            var responseVm = new BranchResponseVm();
            responseVm.BranchId = branchEntity.BranchId;
            responseVm.BranchLocation = $"{branchEntity.BranchLocation.City}, {branchEntity.BranchLocation.Country}";
            responseVm.BranchName = branchEntity.BranchName.BranchName;

            return responseVm;
        }

    }
}
