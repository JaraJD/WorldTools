using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorldTools.SqlAdapter.DataEntity;

namespace WorldTools.Infrastructure
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-2V18T6VP\\SERVIDORSQL;Database=WorldTools;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        public DbSet<RegisterBranchData> Branch { get; set; }
        public DbSet<RegisterProductData> Product { get; set; }
        public DbSet<RegisterUserData> User { get; set; }
        public DbSet<RegisterSaleData> Sale { get; set; }

    }
}
