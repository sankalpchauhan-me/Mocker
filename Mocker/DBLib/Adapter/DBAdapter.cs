using DBLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Adapter
{
    public interface DBAdapter
    {
        DbSet<Developer> Developers { get; set; }
        DbSet<DevApp> DevApps { get; set; }
        DbSet<AppEntity> AppEntitiys { get; set; }
        DbSet<EntityField> EntityFields { get; set; }
        int SaveChanges();

    }
}
