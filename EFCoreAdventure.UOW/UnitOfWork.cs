using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAdventure.UOW
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        public TContext _context { get; set; }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        public void ChangeDatabase(string databaseName)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State.HasFlag(ConnectionState.Open))
            {
                connection.ChangeDatabase(databaseName);
            }
            else
            {
                var connectionString = Regex.Replace(connection.ConnectionString.Replace(" ", ""), @"", 
                    databaseName, RegexOptions.Singleline);
                connection.ConnectionString = connectionString;
            }

            // Following code only working for mysql.
            var items = _context.Model.GetEntityTypes();
            foreach (var item in items)
            {
                if (item.Relational() is RelationalEntityTypeAnnotations extensions)
                {
                    extensions.Schema = databaseName;
                }
            }
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
