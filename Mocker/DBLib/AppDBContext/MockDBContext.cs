using DBLib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.AppDBContext
{
    /// <summary>
    /// ORM for Entity FrameWork
    /// </summary>
    public class MockDBContext : DbContext
    {
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DevApp> DevApps { get; set; }
        public DbSet<AppEntity> AppEntitiys { get; set; }
        public DbSet<EntityField> EntityFields { get; set; }

        //Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>().HasMany(d => d.DevApps).WithRequired(e => e.Developer).HasForeignKey(e => e.DevId);
            modelBuilder.Entity<DevApp>().HasMany(d => d.AppEntitiys).WithRequired(e =>e.DevApp).HasForeignKey(e => e.AppId);
            modelBuilder.Entity<AppEntity>().HasMany(d => d.EntityFields).WithRequired(e =>e.AppEntitiy).HasForeignKey(e => e.EntityId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
