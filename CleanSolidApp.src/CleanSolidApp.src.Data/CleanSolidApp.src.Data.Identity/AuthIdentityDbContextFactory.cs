using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CleanSolidApp.src.Data.Identity;

public class AuthIdentityDbContextFactory : IDesignTimeDbContextFactory<AuthIdentityDbContext>
{
    public AuthIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AuthIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultIdentityConnection");

        builder.UseSqlServer(connectionString);

        return new AuthIdentityDbContext(builder.Options);
    }
}