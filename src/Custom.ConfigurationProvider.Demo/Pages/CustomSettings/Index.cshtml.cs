using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Custom.Configuration.Provider.Demo.Pages.CustomSettings
{
    public class IndexModel : PageModel
    {

        private readonly IAppSettingsCustomRepository _appSettingsCustomRepository;

        public IndexModel(IAppSettingsCustomRepository appSettingsCustomRepository)
        {
            _appSettingsCustomRepository = appSettingsCustomRepository ?? throw new ArgumentNullException(nameof(AppSettingsCustomRepository));
        }

        public IEnumerable<AppSettingsCustomEntity> AppSettingsCustomEntities { get; set; } = new List<AppSettingsCustomEntity>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                await _appSettingsCustomRepository.SetDefaultAsync(id.Value).ConfigureAwait(false);
            }
            AppSettingsCustomEntities = await _appSettingsCustomRepository.GetAsync().ConfigureAwait(false);
            return Page();
        }
    }
}