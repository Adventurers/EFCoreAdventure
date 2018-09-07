using Adventurers.Design.Repository;
using EFCoreAdventure.Models;

namespace EFCoreAdventure.UOW
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public IReadRepository<TEntity> Query { get; set ; }
        public IWriteRepository<TEntity> Write { get; set; }

        public Repository(IReadRepository<TEntity> query, IWriteRepository<TEntity> write)
        {
            
            Query = query;
            Write = write;
            
        }
    }
}
