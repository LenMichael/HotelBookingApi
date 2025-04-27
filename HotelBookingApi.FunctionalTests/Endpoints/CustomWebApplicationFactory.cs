//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.EntityFrameworkCore;
//using HotelBookingApi.Data;

//namespace HotelBookingApi.FunctionalTests.Endpoints
//{
//    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
//    {
//        protected override void ConfigureWebHost(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices(services =>
//            {
//                // Remove the existing DbContext registration
//                var descriptor = services.SingleOrDefault(
//                    d => d.ServiceType == typeof(DbContextOptions<ApiContext>));
//                if (descriptor != null)
//                {
//                    services.Remove(descriptor);
//                }

//                // Add an in-memory database for testing
//                services.AddDbContext<ApiContext>(options =>
//                {
//                    options.UseInMemoryDatabase("InMemoryTestDatabase");
//                });

//                // Build the service provider
//                var serviceProvider = services.BuildServiceProvider();

//                // Ensure the database is created
//                using (var scope = serviceProvider.CreateScope())
//                {
//                    var db = scope.ServiceProvider.GetRequiredService<ApiContext>();
//                    db.Database.EnsureCreated();
//                }
//            });
//        }
//    }
//}
