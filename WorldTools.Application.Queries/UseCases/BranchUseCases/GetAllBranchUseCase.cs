using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Application.Queries.UseCases.BranchUseCases
{
    public class GetAllBranchUseCase
    {
        private readonly IBranchRepository _repository;

        public GetAllBranchUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BranchQueryVm>> GetAllBranches()
        {
            return await _repository.GetAllBranchesAsync();
        }
    }
}
