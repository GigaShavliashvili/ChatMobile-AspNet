using System.Linq.Expressions;

namespace chatmobile.repositories
{
    public interface IRepositories<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All { get; }
        TEntity Get<Tkey>(Tkey id) where Tkey : struct;
        Task<TEntity> GetAsync<Tkey>(Tkey id, CancellationToken cancellationToken) where Tkey : struct;

        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<int> CompleteAsync(CancellationToken cancellationToken);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}