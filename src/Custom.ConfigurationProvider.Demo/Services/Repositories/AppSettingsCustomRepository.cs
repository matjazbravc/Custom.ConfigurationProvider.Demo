using Custom.Configuration.Provider.Demo.Data.Entities;
using Custom.Configuration.Provider.Demo.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Custom.Configuration.Provider.Demo.Services.Repositories
{
    public class AppSettingsCustomRepository : IAppSettingsCustomRepository
    {
        protected readonly DatabaseContext _dbContext;

        public AppSettingsCustomRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<AppSettingsCustomEntity> AddAsync(AppSettingsCustomEntity entity)
        {
            if (entity.Default)
            {
                await _dbContext.AppSettingsCustomItems.ForEachAsync(x => x.Default = false);
            }
            _dbContext.AppSettingsCustomItems.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(AppSettingsCustomEntity entity)
        {
            if (entity.Default)
            {
                var firstEntity = await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync();
                if (firstEntity != null)
                {
                    firstEntity.Default = true;
                }
            }
            _dbContext.AppSettingsCustomItems.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<AppSettingsCustomEntity>> GetAsync()
        {
            return await _dbContext.AppSettingsCustomItems.ToListAsync();
        }

        public async Task<AppSettingsCustomEntity> GetByIdAsync(int id)
        {
            return await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SetDefaultAsync(int id)
        {
            await _dbContext.AppSettingsCustomItems.ForEachAsync(x => x.Default = false);
            var defSettings = await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync(c => c.Id == id);
            defSettings.Default = true;
            _dbContext.Entry(defSettings).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(AppSettingsCustomEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
