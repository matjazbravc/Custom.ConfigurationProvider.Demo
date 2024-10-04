using Custom.Configuration.Provider.Demo.Data.Entities.Common;
using Custom.Configuration.Provider.Demo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Custom.Configuration.Provider.Demo.Data;

public class DatabaseContext : DbContext
{
  // Don't remove any of constructors!
  public DatabaseContext()
  {
  }

  public DatabaseContext(DbContextOptions<DatabaseContext> options)
      : base(options)
  {
  }

  public DbSet<AppSettingsCustomEntity> AppSettingsCustomItems { get; set; }

  public override int SaveChanges()
  {
    TrackEntityChanges();
    return base.SaveChanges();
  }

  public async Task<int> SaveChangesAsync()
  {
    TrackEntityChanges();
    return await base.SaveChangesAsync();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    TrackEntityChanges();
    return await base.SaveChangesAsync(cancellationToken);
  }

  private void TrackEntityChanges()
  {
    foreach (var entry in ChangeTracker.Entries().Where(e =>
        e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted))
    {
      EntityChangeObserver.Instance.OnChanged(new EntityChangeEventArgs(entry));
    }
  }
}
