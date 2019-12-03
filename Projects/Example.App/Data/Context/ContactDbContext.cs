using Example.App.Data.Models;
using Example.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Example.App.Data.Context
{
    public interface IContactsDbContext : IDbContext
    {
        DbSet<Contact> Contacts { get; set; }
    }

    public class ContactDbContext : TransactionalDbContext, IContactsDbContext
    {
        // dotnet ef migrations add InitialCreate --context ContactDbContext

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Pet> Pets { get; set; }

        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfigurator());
            modelBuilder.ApplyConfiguration(new AddressConfigurator());

            modelBuilder.Entity<Contact>();
            modelBuilder.Entity<Address>();
            modelBuilder.Entity<Pet>();
        }
    }
}
