using DBLib.Adapter;
using DBLib.AppDBContext;
using Mocker.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Mocker.Repository
{
    public class DbRepository<T> where T : class
    {
        private DBAdapter _context;
        private DbSet<T> dbEntity;

        public DbRepository()
        {
            _context = new MockSQLContext(System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONN_STRING].ConnectionString);
            dbEntity = _context.Set<T>();
        }

        public DbRepository(DBAdapter context)
        {
            _context = context;
            dbEntity = _context.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = dbEntity;
            return query.ToList();
        }

        public virtual T GetById(object id)
        {
            return dbEntity.Find(id);
        }

        public virtual T Insert(T model)
        {
            return dbEntity.Add(model);
        }
        public virtual T Delete(object id)
        {
            T entityToDelete = dbEntity.Find(id);
            return Delete(entityToDelete);
        }

        public virtual T Delete(T model)
        {
            return dbEntity.Remove(model);

        }

        public virtual void Update(T entityToUpdate)
        {
            _context.Set<T>().AddOrUpdate(entityToUpdate);
        }



        public virtual IQueryable<T> GetManyQueryable(Func<T, bool> where)
        {
            return dbEntity.Where(where).AsQueryable();
        }

        public T Get(Func<T, Boolean> where)
        {
            return dbEntity.Where(where).FirstOrDefault<T>();
        }


        public void Delete(Func<T, Boolean> where)
        {
            IQueryable<T> objects = dbEntity.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                dbEntity.Remove(obj);
        }


        public virtual IEnumerable<T> GetAll()
        {
            return dbEntity.ToList();
        }

        public IQueryable<T> GetWithInclude(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] include)
        {
            IQueryable<T> query = this.dbEntity;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public IQueryable<T> GetWithInclude()
        {
            IQueryable<T> query = this.dbEntity;
            return query;
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            return dbEntity.Single<T>(predicate);
        }



        public T GetFirst(Func<T, bool> predicate)
        {
            return dbEntity.First<T>(predicate);
        }








    }
}