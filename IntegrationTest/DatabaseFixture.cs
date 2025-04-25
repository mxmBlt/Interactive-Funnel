using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;


// Rajouter bogus
// Seeder à part
namespace IntegrationTest
{
    internal class DatabaseFixture
    {
        private readonly MsSqlContainer _dbContainer;
        public ApplicationDbContext dbContext { get; }
        public DatabaseFixture()
        {
            _dbContainer = new MsSqlBuilder()
             .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
             .WithPassword("frfgvnjikgt345!")
             .Build();
            _dbContainer.StartAsync().Wait();
            var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseSqlServer(_dbContainer.GetConnectionString())
                 .UseSeeding((context, _) =>
                 {
                     context.Comments.Add(new Comment
                     {
                         Content = "Test comment",
                     });
                     context.SaveChanges();

                 })
                 .Options);
            dbContext.Database.Migrate();
        }
        
    }
}
