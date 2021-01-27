using DBModels.Models;
using System.Data.Entity;

namespace DBLib.Adapter
{
    /// <summary>
    /// An adapter class that
    /// </summary>
    public interface DBAdapter
    {
        DbSet<Developer> Developers { get; set; }
        DbSet<DevApp> DevApps { get; set; }
        DbSet<AppEntity> AppEntitiys { get; set; }
        DbSet<EntityField> EntityFields { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        //object Entry<T>(T model) where T : class;
    }
}
