using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Custom.Configuration.Provider.Demo.Data.Entities.Common;

public class EntityChangeEventArgs(EntityEntry entry) : EventArgs
{
  public EntityEntry Entry { get; set; } = entry;
}
