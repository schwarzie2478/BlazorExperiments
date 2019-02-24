using Example01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example01.Data
{
    public class ExampleDbContext: DbContext
    {
        public DbSet<Application> Applications { get; set; }

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options ):base(options)
        {
        }
    }
}
