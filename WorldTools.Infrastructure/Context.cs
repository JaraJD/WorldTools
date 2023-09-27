using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) 
        { 

        }

        public DbSet<BranchEntity> Branch { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<UserEntity> User { get; set; }

    }
}
