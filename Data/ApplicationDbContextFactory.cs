using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avatrans.Data;
using Microsoft.EntityFrameworkCore;

namespace Avatrans.Data
{
    public class ApplicationDbContextFactory
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var m = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "avatrans.db");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlite($"Data Source={m}");

            return new ApplicationDbContext(options.Options);
        }

        public ApplicationDbContext CreateDbContext() => CreateDbContext(new string[0]);
    }
}