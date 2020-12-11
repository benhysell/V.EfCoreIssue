using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace V.EfCoreIssueNet50
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    optional: true)
                .Build();
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                   options.MigrationsAssembly(migrationsAssembly).CommandTimeout(int.MaxValue).EnableRetryOnFailure(int.MaxValue)
                   ), ServiceLifetime.Transient);

            serviceCollection.AddLogging();

            var services = serviceCollection.BuildServiceProvider();
            using (var scope = services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                applicationDb.Database.Migrate();

                var organizationId = Guid.Empty;

                var foundOrg = (from organizations in applicationDb.Organizations.Where(x => x.Id == organizationId)
                                select organizations.HierarchyId);

                var hiearchies = (
                                        from organizationHiearchies in foundOrg.DefaultIfEmpty()
                                        from hierarchy in applicationDb.Hierarchies.Where(x => x.Id != organizationHiearchies)
                                        select hierarchy).ToList();

                foreach (var hierarchy in hiearchies)
                {
                    Console.WriteLine($"{hierarchy.Name}");
                }

            }


        }
    }
}

