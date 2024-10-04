using System;
using System.Threading.Tasks;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Custom.Configuration.Provider.Demo.Pages.CustomSettings;

  public class CreateModel : PageModel
  {
      private readonly IAppSettingsCustomRepository _appSettingsCustomRepository;

      public CreateModel(IAppSettingsCustomRepository appSettingsCustomRepository)
      {
          _appSettingsCustomRepository = appSettingsCustomRepository ?? throw new ArgumentNullException(nameof(appSettingsCustomRepository));
      }

      [BindProperty]
      public AppSettingsCustomEntity AppSettingsCustom { get; set; }

      public IActionResult OnGet()
      {
          AppSettingsCustom = new AppSettingsCustomEntity();
          return Page();
      }

      public async Task<IActionResult> OnPostAsync()
      {
          if (!ModelState.IsValid)
          {
              return Page();
          }
          AppSettingsCustom = await _appSettingsCustomRepository.AddAsync(AppSettingsCustom);
          return RedirectToPage("./Index");
      }
  }