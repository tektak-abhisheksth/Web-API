using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.DbEntity
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindByAsync(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void AddOrUpdate(T entity);
        T Delete(T entity);
        IQueryable<T> AsQueryable();
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
