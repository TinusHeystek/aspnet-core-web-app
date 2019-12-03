using System;
using System.Linq;
using Example.App;
using Example.App.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Example.IntegrationTests._SetUp
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the App DbContext registration.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ContactDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // var serviceProvider = new ServiceCollection()
                //     .AddEntityFrameworkInMemoryDatabase()
                //     .BuildServiceProvider();

                // services.AddDbContext<ContactDbContext>(options =>
                // {
                //     options.UseInMemoryDatabase("InMemoryDatabase");
                //     options.UseInternalServiceProvider(serviceProvider);
                // });

                services.AddDbContext<ContactDbContext>((options, context) =>
                {
                    var database = "MyContacts_IntegrationTests";
                    context.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={database};Trusted_Connection=True;MultipleActiveResultSets=true");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    try
                    {
                        appContext.Database.Migrate();

                        // Seed the database with some specific test data.
                        // SeedData.PopulateTestData(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}