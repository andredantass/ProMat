using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProMat.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMat.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext() {
         
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<QualifiedLead> QualifiedLeads { get; set; }
        public DbSet<DisqualifiedLead> DisqualifiedLeads { get; set; }
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Webhook> Webhook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>().HasOne(web => web.WebHook);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.EnableSensitiveDataLogging(); for migrations
            IConfigurationRoot configuration = new ConfigurationBuilder()

                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("./appsettings.json")
                 .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            //connectionString = string.Format(connectionString, AppDomain.CurrentDomain.BaseDirectory);

            optionsBuilder.UseSqlite(connectionString);
           

            
        }
    }
}
