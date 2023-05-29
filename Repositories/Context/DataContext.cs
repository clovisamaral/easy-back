using EasyInvoice.API.Entities.Clients;
using EasyInvoice.API.Entities.Providers;
using EasyInvoice.API.Entities.Relationships;
using Microsoft.EntityFrameworkCore;

namespace EasyInvoice.API.Repositories.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=easyInvoice.db");
        }
    }
}
