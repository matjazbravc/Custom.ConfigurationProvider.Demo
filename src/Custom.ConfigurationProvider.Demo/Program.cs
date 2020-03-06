using Custom.Configuration.Provider.Demo.Configuration.Sources;
using Custom.Configuration.Provider.Demo.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace Custom.Configuration.Provider.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.Sources.Clear();
                    config.SetBasePath(env.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    // Rebuild configuration
                    var configuration = config.Build();
                    var sqlServerOptions = configuration.GetSection(nameof(SqlServerOptions));
                    config.Add(new AppSettingsCustomEntityConfigurationSource(configuration)
                    {
                        OptionsAction = options => options.UseSqlite(sqlServerOptions[nameof(SqlServerOptions.SqlServerConnection)]),
                        //OptionsAction = options => options.UseInMemoryDatabase("db", new InMemoryDatabaseRoot())),
                        ReloadOnChange = true
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                // Because we are accessing a Scoped service via the IOptionsSnapshot provider,
                // we must disable the dependency injection scope validation feature:
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext());
    }
}