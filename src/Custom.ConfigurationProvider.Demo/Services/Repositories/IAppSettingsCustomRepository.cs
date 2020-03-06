using Custom.Configuration.Provider.Demo.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Custom.Configuration.Provider.Demo.Services.Repositories
{
    public interface IAppSettingsCustomRepository
    {
        Task<AppSettingsCustomEntity> AddAsync(AppSettingsCustomEntity entity);

        Task DeleteAsync(AppSettingsCustomEntity entity);

        Task<IEnumerable<AppSettingsCustomEntity>> GetAsync();

        Task<AppSettingsCustomEntity> GetByIdAsync(int id);

        Task SetDefaultAsync(int id);

        Task UpdateAsync(AppSettingsCustomEntity entity);
    }
}
