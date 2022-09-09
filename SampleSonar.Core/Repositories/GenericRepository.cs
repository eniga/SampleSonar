using MicroOrm.Dapper.Repositories;
using SampleSonar.Core.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace SampleSonar.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DapperRepository<T> repository;

        protected GenericRepository(DapperRepository<T> repo)
        {
            repository = repo;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            return await repository.FindAllAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.FindAllAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<T> FindByIdAsync(object id)
        {
            return await repository.FindByIdAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.FindAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<bool> InsertAsync(T item)
        {
            return await repository.InsertAsync(item).ConfigureAwait(false);
        }

        public virtual async Task<int> BulkInsertAsync(IEnumerable<T> items, IDbTransaction transaction = null)
        {
            return await repository.BulkInsertAsync(items, transaction).ConfigureAwait(false);
        }

        public virtual async Task<bool> UpdateAsync(T item, params Expression<Func<T, object>>[] includes)
        {
            return await repository.UpdateAsync(item, includes).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(T item)
        {
            return await repository.DeleteAsync(item).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, IDbTransaction transaction = null)
        {
            return await repository.DeleteAsync(predicate, transaction).ConfigureAwait(false);
        }
    }
}
