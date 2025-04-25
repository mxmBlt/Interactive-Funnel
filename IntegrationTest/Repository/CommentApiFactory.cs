using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using api;
using Microsoft.AspNetCore.TestHost;
internal class CommentApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
    public ApplicationDbContext DbContext { get; private set; }

    public CommentApiFactory()
    {
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("frfgvnjikgt345!")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(async services =>
        {
            await _dbContainer.StartAsync(); 
            DbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_dbContainer.GetConnectionString())
                .Options);
            services.RemoveAll(typeof(IApplicationDbContext));
            services.AddScoped<ApplicationDbContext>(_ => DbContext); 
        });
    }

    public string GetConnectionString() => _dbContainer.GetConnectionString(); // ✅ Expose Connection String
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeAsync() => await _dbContainer.StopAsync();
}
