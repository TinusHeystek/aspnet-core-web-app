using Example.App.Data.Context;
using Example.UnitTests.Factories;
using Microsoft.EntityFrameworkCore;

namespace Example.UnitTests.Data
{
    public static class DbContextFactory
    {
        public static ContactDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase("TestContact")
                .Options;

            var context = new ContactDbContext(options);

            Seed(context);

            return context;
        }

        private static void Seed(ContactDbContext context)
        {
            var contacts = new [] {ContactFactory.GetContact()};
            context.AddRange(contacts);
            context.SaveChanges();
        }
    }
}
