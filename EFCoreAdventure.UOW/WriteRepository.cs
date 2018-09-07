using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAdventure.UOW
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        public DbContext _context { get; set; }

        public WriteRepository(DbContext context)
        {
            _context = context;
        }


        public void Delete<TKey>(TKey id)
        {
            var entity = _context.Set<TEntity>().Find(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }



        public void Insert(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Insert(params TEntity[] entities)
        {
            _context.AddRange(entities);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public TEntity Update<TKey>(TEntity tentity, TKey key)
        {
            var entityExist = _context.Set<TEntity>().Find(key);

            if (entityExist != null)
            {
                _context.Entry<TEntity>(entityExist).CurrentValues.SetValues(tentity);
            }

            return entityExist;
        }

        public async Task<TEntity> UpdateAsync<TKey>(TEntity entity, TKey key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entityExist = await _context.Set<TEntity>().FindAsync(key, cancellationToken);

            if (entityExist != null)
            {
                _context.Entry<TEntity>(entityExist).CurrentValues.SetValues(entity);
            }

            return entityExist;
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _context.Set<TEntity>().Where(predicate).ToListAsync();

            if (entities != null)
            {
                Delete(entities);
            }
        }

        public async Task DeleteAsync<TKey>(TKey id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _context.Set<TEntity>().FindAsync(id , cancellationToken);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        public async Task<int> ExecuteCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, parameters, cancellationToken);
        }

    }
}
