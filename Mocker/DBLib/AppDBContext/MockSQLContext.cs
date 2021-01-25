using DBLib.Adapter;
using DBLib.Models;
using System.Data.Entity;

namespace DBLib.AppDBContext
{
    /// <summary>
    /// ORM for Entity FrameWork
    /// </summary>
    public class MockSQLContext : DbContext, DBAdapter
    {
        public MockSQLContext(string connString)
        {
            this.Database.Connection.ConnectionString = connString;
        }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DevApp> DevApps { get; set; }
        public DbSet<AppEntity> AppEntitiys { get; set; }
        public DbSet<EntityField> EntityFields { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }


        //API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>().HasMany(d => d.DevApps).WithRequired(e => e.Developer).HasForeignKey(e => e.DevId).WillCascadeOnDelete(true);
            modelBuilder.Entity<DevApp>().HasMany(d => d.AppEntitiys).WithRequired(e => e.DevApp).HasForeignKey(e => e.AppId).WillCascadeOnDelete(true);
            modelBuilder.Entity<AppEntity>().HasMany(d => d.EntityFields).WithRequired(e => e.AppEntitiy).HasForeignKey(e => e.EntityId).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
