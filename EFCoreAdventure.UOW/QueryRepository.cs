using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFCoreAdventure.UOW
{
    public class QueryRepository<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public DbContext _context { get; set; }

        
        public QueryRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> FindByQuery(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
        {
            return Query(tracking).Where(predicate);
        }


        public IQueryable<TEntity> GetAllIncluding(bool tracking = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Query(tracking);

            foreach (var property in includeProperties)
            {
                query.Include(property);
            }

            return query;
        }

        public IQueryable<TEntity> Query(bool tracking = false)
        {
            return MakeNoTrackingQuery(tracking);
            
        }

        public IQueryable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return Query().FromSql(sql, parameters);
        }


        private IQueryable<TEntity> MakeNoTrackingQuery(bool tracking)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }
}
