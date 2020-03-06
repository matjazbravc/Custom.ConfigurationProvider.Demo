using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Custom.Configuration.Provider.Demo.Data.Entities.Common
{
    public class EntityChangeEventArgs : EventArgs
    {
        public EntityEntry Entry { get; set; }

        public EntityChangeEventArgs(EntityEntry entry)
        {
            Entry = entry;
        }
    }
}
