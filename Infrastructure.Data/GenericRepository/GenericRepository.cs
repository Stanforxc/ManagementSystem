using Infrastructure.Data.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Data.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        internal DataEntities Context;
        internal DbSet<T> DbSet;

        public GenericRepository(DataEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get()
        {
            IQueryable<T> query = DbSet;
            return query.ToList();
        }

        public virtual T GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if(Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Uppdate(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> GetMany(Func<T,bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        public virtual IQueryable<T> GetManyQueryable(Func<T,bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        public T Get(Func<T,Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<T>();
        }

        public void Delete(Func<T,Boolean> where)
        {
            IQueryable<T> objects = DbSet.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                DbSet.Remove(obj);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public IQueryable<T> GetWithInclude(System.Linq.Expressions.Expression<Func<T,bool> >predicate,params string[] include)
        {
            IQueryable<T> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        public T GetSingle(Func<T,bool> predicate)
        {
            return DbSet.Single<T>(predicate);
        }

        public T GetFirst(Func<T,bool> predicate)
        {
            return DbSet.First<T>(predicate);
        }

        public void InsertBatch(ICollection<T> entities)
        {
           foreach(var entity in entities)
            {
                Insert(entity);
            }
        }
    }
}
