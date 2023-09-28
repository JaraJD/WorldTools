using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Application.Gateway;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Domain.Commands.BranchCommands;

namespace WorldTools.Application.UseCase
{
    public class BranchUseCase : IBranchUseCase
    {
        private readonly IBranchRepository _repository;

        public BranchUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }

        public Task<string> RegisterBranch(RegisterBranchCommand branch)
        {
            throw new NotImplementedException();
        }
    }
}
