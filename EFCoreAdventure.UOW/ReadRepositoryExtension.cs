using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAdventure.UOW
{
    public static class ReadRepositoryExtension
    {
        public static async Task<PageList<TResult>> GetPageListAsync<TEntity, TResult>(
                                                             this IReadRepository<TEntity> repository,
                                                             Expression<Func<TEntity, TResult>> selector,
                                                             Expression<Func<TEntity, bool>> predicate = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                             int pageNumber = 1,
                                                             int pageSize = 10,
                                                             bool tracking = true,
                                                             CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = repository.Query(tracking);


            if (include != null)
            {
                query = include(query);
            }

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
    }
}
