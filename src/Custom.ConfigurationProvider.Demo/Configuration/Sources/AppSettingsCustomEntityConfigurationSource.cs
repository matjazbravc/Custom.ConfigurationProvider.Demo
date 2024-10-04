using Custom.Configuration.Provider.Demo.Configuration.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Custom.Configuration.Provider.Demo.Configuration.Sources;

public class AppSettingsCustomEntityConfigurationSource : IConfigurationSource
{
  public AppSettingsCustomEntityConfigurationSource(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; private set; }

  public Action<DbContextOptionsBuilder> OptionsAction { get; set; }

  /// <summary>
  ///     Number of milliseconds that ConfigurationProvider will wait before calling Load method.
  ///     This helps avoid triggering a reload before a change is saved.
  /// </summary>
  public int ReloadDelay { get; set; } = 500;

  public bool ReloadOnChange { get; set; }

  public IConfigurationProvider Build(IConfigurationBuilder builder)
  {
    return new AppSettingsCustomEntityConfigurationProvider(this);
  }
}
