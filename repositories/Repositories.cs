using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace chatmobile.repositories
{
    public class Repositories<TEntity>(DbContext context) : IRepositories<TEntity> where TEntity : class
    {
        protected readonly DbContext _context = context;

        public IQueryable<TEntity> All => _context.Set<TEntity>();
        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual TEntity Get<Tkey>(Tkey id) where Tkey : struct
        {
            return _context.Set<TEntity>().Find(new object[] { id })!;
        }

        public virtual async Task<TEntity> GetAsync<Tkey>(Tkey id, CancellationToken cancellationToken) where Tkey : struct
        {
            return (await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken))!;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return [.. _context.Set<TEntity>()];
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }


        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task RemoveAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entities = _context.Set<TEntity>().Where(predicate).ToList();
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }


        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}