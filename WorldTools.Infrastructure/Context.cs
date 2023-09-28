using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WorldTools.Domain.DTO;
using WorldTools.Domain.Entities;

namespace WorldTools.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) 
        { 
        }

        public DbSet<RegisterBranchDTO> Branch { get; set; }
        public DbSet<RegisterProductDTO> Product { get; set; }
        public DbSet<RegisterUserDTO> User { get; set; }
        public DbSet<StoredEvent> StoredEvent { get; set; }

    }
}
