using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Avatrans.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // public DbSet<SomeEntity> SomeEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            // model.Entity<someEntity>().ToTable("SomeTable");
        }
    }
}