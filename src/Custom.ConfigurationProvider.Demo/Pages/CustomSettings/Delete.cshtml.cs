using System;
using System.Threading.Tasks;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Custom.Configuration.Provider.Demo.Pages.CustomSettings
{
    public class DeleteModel : PageModel
    {
        private readonly IAppSettingsCustomRepository _appSettingsCustomRepository;

        public DeleteModel(IAppSettingsCustomRepository appSettingsCustomRepository)
        {
            _appSettingsCustomRepository = appSettingsCustomRepository ?? throw new ArgumentNullException(nameof(appSettingsCustomRepository));
        }

        [BindProperty]
        public AppSettingsCustomEntity AppSettingsCustom { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppSettingsCustom = await _appSettingsCustomRepository.GetByIdAsync(id.Value);
            if (AppSettingsCustom == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _appSettingsCustomRepository.DeleteAsync(AppSettingsCustom);
            return RedirectToPage("./Index");
        }
    }
}