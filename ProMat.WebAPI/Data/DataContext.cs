using Microsoft.EntityFrameworkCore;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<QualifiedQueue> QualifiedQueues { get; set; }
        public DbSet<DisqualifiedQueue> DisqualifiedQueues { get; set; }
    }
}
