using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldTools.Domain.Entities;
using WorldTools.Domain.Ports;
using WorldTools.Domain.ResponseVm.Branch;
using WorldTools.Infrastructure;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.SqlAdapter.Adapters
{
    public class BranchRepository : IBranchRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public BranchRepository(Context dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<BranchQueryVm> GetBranchByIdAsync(Guid branchId)
        {
            RegisterBranchData branchWithRelatedData = await _context.Branch
                .Include(b => b.BranchProducts)
                .Include(b => b.BranchEmployees)
                .Include(b => b.BranchSales)
                .FirstOrDefaultAsync(b => b.BranchId == branchId);

            var branchQueryVm = _mapper.Map<BranchQueryVm>(branchWithRelatedData);

            return branchQueryVm;
        }

        public async Task<BranchEntity> RegisterBranchAsync(BranchEntity branch)
        {
            using (var context = new Context())
            {
                var branchToCreate = new RegisterBranchData(branch.BranchName.BranchName, branch.BranchLocation.Country, branch.BranchLocation.City);
                context.Add(branchToCreate);
                await context.SaveChangesAsync();

                branch.BranchId = branchToCreate.BranchId;
                return branch;
            }

        }

    }
}
