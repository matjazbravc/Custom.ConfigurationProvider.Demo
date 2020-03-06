using Custom.Configuration.Provider.Demo.Configuration.Sources;
using Custom.Configuration.Provider.Demo.Data.Entities.Common;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System;

namespace Custom.Configuration.Provider.Demo.Configuration.Providers
{
    public class AppSettingsCustomEntityConfigurationProvider : ConfigurationProvider
    {
        private readonly AppSettingsCustomEntityConfigurationSource _source;

        public AppSettingsCustomEntityConfigurationProvider(AppSettingsCustomEntityConfigurationSource source)
        {
            _source = source;
            if (_source.ReloadOnChange)
            {
                EntityChangeObserver.Instance.Changed += EntityChangeObserverChanged;
            }
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            _source.OptionsAction(builder);
            var context = new DatabaseContext(builder.Options);
            try
            {
                // Update AppSettingsCustom Data 
                // Configuration consists of a hierarchical list of name-value pairs in which the nodes are separated by a colon (:)
                // Read more: https://www.paraesthesia.com/archive/2018/06/20/microsoft-extensions-configuration-deep-dive/
                Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Ensure database is created
                context.Database.EnsureCreated();

                // Read default configuration from Data table
                var config = context.AppSettingsCustomItems.FirstOrDefault(cfg => cfg.Default == true);
                if (config == null)
                {
                    // Note: if there is no available configuration in the database then we read values from appsettings.json file
                    Data.Add($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingA)}", _source.Configuration.GetValue<string>($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingA)}"));
                    Data.Add($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingB)}", _source.Configuration.GetValue<string>($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingB)}"));
                }
                else
                {
                    if (config.CustomSettingA != null)
                    {
                        Data.Add($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingA)}", config.CustomSettingA);
                    }
                    if (config.CustomSettingB != null)
                    {
                        Data.Add($"{nameof(AppSettingsCustom)}:{nameof(AppSettingsCustom.CustomSettingB)}", config.CustomSettingB);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception occured");
                return;
            }
        }

        private void EntityChangeObserverChanged(object sender, EntityChangeEventArgs e)
        {
            if (e.Entry.Entity.GetType() != typeof(AppSettingsCustomEntity))
            {
                return;
            }
            // Make a small delay to avoid triggering a reload before a change is saved to the underlaying database
            Thread.Sleep(_source.ReloadDelay);
            Load();
        }
    }
}
