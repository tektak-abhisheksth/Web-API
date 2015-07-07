using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entity;

namespace DAL.DbEntity
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected iLoopEntity Context { get; set; }
        readonly DbSet<T> _dbSet;

        protected GenericRepository(iLoopEntity context)
        {
            Context = context;
            _dbSet = context.Set<T>();
        }


        #region IGenericRepository<T> implementation
        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = _dbSet.Where(predicate);
            return query;
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FindAsync(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void AddOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        public T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }
        #endregion
    }
}
