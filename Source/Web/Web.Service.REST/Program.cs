using Infrastructure.DAL.EF;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Web.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                        .Build();
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<DatabaseContext>())
            {
                context.Database.EnsureCreated();
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
    }
}
