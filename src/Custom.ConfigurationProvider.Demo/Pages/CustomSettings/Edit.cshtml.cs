using System;
using System.Threading.Tasks;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Custom.Configuration.Provider.Demo.Pages.CustomSettings
{
    public class EditModel : PageModel
    {
        private readonly IAppSettingsCustomRepository _appSettingsCustomRepository;

        public EditModel(IAppSettingsCustomRepository appSettingsCustomRepository)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _appSettingsCustomRepository.UpdateAsync(AppSettingsCustom);
                await _appSettingsCustomRepository.SetDefaultAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AppSettingsCustomExistsAsync(AppSettingsCustom.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private async Task<bool> AppSettingsCustomExistsAsync(int id)
        {
            var product = await _appSettingsCustomRepository.GetByIdAsync(id).ConfigureAwait(false);
            return product != null;
        }
    }
}