using EFCoreAdventure.Models;
using EFCoreAdventure.UOW;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFCoreAdventure.Mapping
{
    public class ProductRepository : ReadRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {

        }
        public async Task<Product> Contains(string productName)
        {
            return await _context.Set<Product>().SingleOrDefaultAsync(p => EF.Functions.Like(p.Name, $"{productName}%"));
        }
    }
}
