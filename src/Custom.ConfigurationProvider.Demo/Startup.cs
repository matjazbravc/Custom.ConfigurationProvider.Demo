using Custom.Configuration.Provider.Demo.Configuration;
using Custom.Configuration.Provider.Demo.Data;
using Custom.Configuration.Provider.Demo.Extensions;
using Custom.Configuration.Provider.Demo.Middleware;
using Custom.Configuration.Provider.Demo.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Custom.Configuration.Provider.Demo;

public class Startup(IConfiguration configuration)
{
  public IConfiguration Configuration { get; } = configuration;

  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
    // Add services required for using options
    services.AddOptions();

    services.AddRazorPages();

    // Configure AppSettings
    services.AppSettingsConfiguration(Configuration);

    var sqlServerOptions = Configuration.GetSection(nameof(SqlServerOptions));
    services.AddDbContext<DatabaseContext>(options => options
      .UseSqlite(sqlServerOptions[nameof(SqlServerOptions.SqlServerConnection)]));

    // Add services
    services.AddScoped<IAppSettingsCustomRepository, AppSettingsCustomRepository>();

    services.AddMvc(options => options.EnableEndpointRouting = false);
  }

  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
  {
    // Configure Serilog support
    loggerFactory.AddSerilog();

    // Handling Errors Globally with the Custom Middleware
    app.UseMiddleware<ExceptionMiddleware>();

    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
    }

    app.UseStaticFiles();
    app.UseRouting();
    app.UseCors();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapRazorPages();
    });

    app.UseMvc();
  }
}
