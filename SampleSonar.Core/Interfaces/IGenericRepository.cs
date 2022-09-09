using System.Data;
using System.Linq.Expressions;

namespace SampleSonar.Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindByIdAsync(object id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> InsertAsync(T item);
        Task<int> BulkInsertAsync(IEnumerable<T> items, IDbTransaction transaction = null);
        Task<bool> UpdateAsync(T item, params Expression<Func<T, object>>[] includes);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null);
    }
}
