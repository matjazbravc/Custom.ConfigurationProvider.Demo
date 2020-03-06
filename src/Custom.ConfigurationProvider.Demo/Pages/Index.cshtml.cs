using Custom.Configuration.Provider.Demo.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Custom.Configuration.Provider.Demo.Pages
{
    public class IndexModel : PageModel
    {
        // Note: Use IOptionsSnapshot to support reloading options!
        // Options are computed once per request when accessed and cached for the lifetime of the request.
        // Read more: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.0#reload-configuration-data-with-ioptionssnapshot
        public IndexModel(IOptions<AppSettings> appSettings, IOptionsSnapshot<AppSettingsCustom> appSettingsCustom)
        {
            AppSettings = appSettings.Value;
            AppSettingsCustom = appSettingsCustom.Value;
        }

        public AppSettings AppSettings { get; set; }
        
        public AppSettingsCustom AppSettingsCustom { get; set; }
    }
}
