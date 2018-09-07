using Adventurers.Design.Repository;
using EFCoreAdventure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAdventure.UOW
{
    public class ReadRepository<TEntity> : QueryRepository<TEntity>, IReadRepository<TEntity> where TEntity : Entity
    {
        
        public ReadRepository(DbContext context) : base(context)
        {
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Query().Where(predicate).Count();
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query().Where(predicate).CountAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query().CountAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public TEntity Find(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
        {
            return Query(tracking).SingleOrDefault(predicate);
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query(tracking).Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FindAsync(CancellationToken cancellationToken = default(CancellationToken), params object[] keyValues)
        {
            return await _context.Set<TEntity>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query(tracking).SingleOrDefaultAsync(predicate, cancellationToken);
        }

      

        public async Task<ICollection<TEntity>> GetAllAsync(bool tracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query(tracking).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, bool tracking = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Query(tracking).OrderBy(c => c.Id).LastOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<TEntity> LastOrDefault(Expression<Func<TEntity, bool>> predicate = null, bool tracking = false)
        {
            return Query(tracking).OrderBy(c => c.Id).LastOrDefaultAsync(predicate);
        }

        

        public async Task<PageList<TResult>> GetPageListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int pageNumber = 1, int pageSize = 10, bool tracking = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = Query(tracking);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            PageList<TResult> result;
            if (orderBy != null)
            {
                result = await orderBy(query).Select(selector).ToPageListAsync(pageNumber, pageSize, cancellationToken);
            }

            result = await query.Select(selector).ToPageListAsync(pageNumber, pageSize, cancellationToken);

            return result;
        }

        public async Task<PageList<TEntity>> GetPageListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int pageNumber = 1, int pageSize = 10, bool tracking = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = Query(tracking);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            PageList<TEntity> result;
            if (orderBy != null)
            {
                result = await orderBy(query).ToPageListAsync(pageNumber, pageSize, cancellationToken);
            }

            result = await query.ToPageListAsync(pageNumber, pageSize, cancellationToken);

            return result;
        }
    }
}
