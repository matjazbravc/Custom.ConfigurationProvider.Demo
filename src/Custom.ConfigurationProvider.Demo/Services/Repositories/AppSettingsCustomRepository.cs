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
            var isDefault = entity.Default;
            if (isDefault)
            {
                await ClearDefaultAsync(entity.Id);
            }
            entity.Default = isDefault;
            _dbContext.AppSettingsCustomItems.Add(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// Clear default for all entities
        /// </summary>
        /// <returns></returns>
        public async Task ClearDefaultAsync()
        {
            await _dbContext.AppSettingsCustomItems.ForEachAsync(x => x.Default = false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Clear default for all entities, except excluded Id
        /// </summary>
        /// <param name="excludeId">Id to exclude</param>
        /// <returns></returns>
        public async Task ClearDefaultAsync(int excludeId)
        {
            var defSettings = await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync(x => x.Id != excludeId && x.Default == true);
            if (defSettings != null)
            {
                defSettings.Default = false;
                _dbContext.Entry(defSettings).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(AppSettingsCustomEntity entity)
        {
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

        /// <summary>
        /// Set first entity as default if there is no one
        /// </summary>
        /// <returns></returns>
        public async Task SetDefaultAsync()
        {
            var defOption = await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync(c => c.Default);
            if (defOption == null)
            {
                var defSettings = await _dbContext.AppSettingsCustomItems.FirstOrDefaultAsync();
                defSettings.Default = true;
                _dbContext.Entry(defSettings).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(AppSettingsCustomEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            var isDefault = entity.Default;
            if (isDefault)
            {
                await ClearDefaultAsync(entity.Id).ConfigureAwait(false);
            }
        }
    }
}
