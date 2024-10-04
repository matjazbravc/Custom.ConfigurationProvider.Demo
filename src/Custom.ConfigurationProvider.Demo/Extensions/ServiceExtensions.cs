using Custom.Configuration.Provider.Demo.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Custom.Configuration.Provider.Demo.Extensions;

public static class ServiceExtensions
{
  public static void AppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);
    services.Configure<SqlServerOptions>(configuration.GetSection(nameof(SqlServerOptions)));
    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
    services.Configure<AppSettingsCustom>(configuration.GetSection(nameof(AppSettingsCustom)));
  }
}
