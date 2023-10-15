using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;
using WorldTools.Domain.ValueObjects.BranchValueObjects;
using WorldTools.SqlAdapter;
using WorldTools.SqlAdapter.Adapters;

namespace WorldTools.Infrastructure.Adapter
{
    public class BranchRepositoryTest
    {
        [Fact]
        public async Task RegisterBranchAsync_ShouldCreateBranchInDatabase()
        {
            var options = new DbContextOptionsBuilder<ContextSql>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            //using (var context = new Context(options))
            //{
            //    var repository = new BranchRepository(context);

            //    var branchToRegister = new BranchEntity
            //    {
            //        BranchName = new BranchValueObjectName("Sucursal de Prueba"),
            //        BranchLocation = new BranchValueObjectLocation("País de Prueba", "Ciudad de Prueba")
            //    };

            //    var registeredBranch = await repository.RegisterBranchAsync(branchToRegister);

            //    Assert.NotEqual(Guid.Empty, registeredBranch.BranchId);

            //}

        }
    }
}
