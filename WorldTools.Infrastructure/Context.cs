using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WorldTools.Domain.Entities;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) 
        { 
        }

        public DbSet<RegisterBranchData> Branch { get; set; }
        public DbSet<RegisterProductData> Product { get; set; }
        public DbSet<RegisterUserData> User { get; set; }
        public DbSet<RegisterSaleData> Sale { get; set; }

    }
}
