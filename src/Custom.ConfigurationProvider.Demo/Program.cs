using Custom.Configuration.Provider.Demo.Configuration.Sources;
using Custom.Configuration.Provider.Demo.Configuration;
using Custom.Configuration.Provider.Demo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

// Initial logging
Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .CreateBootstrapLogger();

using IHost app = Host
  .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder
      .ConfigureAppConfiguration((hostingContext, config) =>
      {
        config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
        config.AddJsonFile("appsettings.json", false, true);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
        // Rebuild configuration
        IConfigurationRoot configuration = config.Build();
        IConfigurationSection sqlServerOptions = configuration.GetSection(nameof(SqlServerOptions));
        config.Add(new AppSettingsCustomEntityConfigurationSource(configuration)
        {
          OptionsAction = options => options.UseSqlite(sqlServerOptions[nameof(SqlServerOptions.SqlServerConnection)]),
          //OptionsAction = options => options.UseInMemoryDatabase("db", new InMemoryDatabaseRoot())),
          ReloadOnChange = true
        });
      })
      .UseStartup<Startup>())
      .UseSerilog((hostingContext, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext(), writeToProviders: true)
  .Build();

await app.RunAsync();
