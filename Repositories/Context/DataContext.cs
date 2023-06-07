using EasyInvoice.API.Entities.Clients;
using EasyInvoice.API.Entities.Invoices;
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
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=easy-invoices-db.postgres.database.azure.com;Database=postgres;User Id=useradmdb;Password=33sf89xxYY*@;Ssl Mode=Require;Port=5432;TrustServerCertificate=True;");
        }
    }
}
