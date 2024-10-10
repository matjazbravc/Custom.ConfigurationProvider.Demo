using System;
using System.Threading;

namespace Custom.Configuration.Provider.Demo.Data.Entities.Common;

public class EntityChangeObserver
{
  public event EventHandler<EntityChangeEventArgs> Changed;

  public void OnChanged(EntityChangeEventArgs e)
  {
    ThreadPool.QueueUserWorkItem((_) => Changed?.Invoke(this, e));
  }

  private static readonly Lazy<EntityChangeObserver> _lazy = new(() => new EntityChangeObserver());

  private EntityChangeObserver() { }

  public static EntityChangeObserver Instance => _lazy.Value;
}
