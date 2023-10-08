using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Branch;

namespace WorldTools.Application.Queries.UseCases.BranchUseCases
{
    public class GetBrachByIdUseCase
    {
        private readonly IBranchRepository _repository;

        public GetBrachByIdUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<BranchQueryVm> GetBranchById(Guid branchId)
        {
            return await _repository.GetBranchByIdAsync(branchId);
        }
    }
}
