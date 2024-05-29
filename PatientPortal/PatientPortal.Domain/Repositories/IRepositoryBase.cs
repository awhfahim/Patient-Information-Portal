using System.Linq.Expressions;

namespace PatientPortal.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Edit(TEntity entityToUpdate);
        Task EditAsync(TEntity entityToUpdate);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        Task RemoveAsync(TEntity entityToDelete, CancellationToken cancellationToken);
        Task RemoveAsync(TKey id, CancellationToken cancellationToken);

    }
}