using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace V.EfCoreIssueNet50
{
    /// <summary>
    /// Used for power shell migrations
    /// </summary>    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)        
        .Build();

        /// <summary>
        /// Used for power shell migrations to create db context
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            builder.UseSqlServer(connectionString, options =>
            {
                options.MigrationsAssembly(migrationsAssembly);
            }
                ).EnableSensitiveDataLogging(true);

            ILoggerFactory loggerFactory = new LoggerFactory()
            //.AddConsole()
            //.AddDebug()
            ;
            

            return new ApplicationDbContext(builder.Options);
        }
    }
}
