using Example01.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example01BackEnd
{


    public class ExampleDbContextFactory : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            var builder = new DbContextOptionsBuilder<ExampleDbContext>();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=Example01;Trusted_Connection=True;ConnectRetryCount=0";
            builder.UseSqlServer(connection);
            return new ExampleDbContext(builder.Options);
        }
    }
}
