using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        void Save();
    }

    public abstract class GenericRepository<C, T> : IGenericRepository<T> where T : class where C : DBContext
    {
        public GenericRepository(C db)
        {
            _db = db;
        }

        protected C _db;
        
        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _db.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _db.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _db.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public virtual void Save()
        {
            _db.SaveChanges();
        }
    }
}
