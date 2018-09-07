using Adventurers.Design.Repository;
using EFCoreAdventure.Models;
using System.Threading.Tasks;

namespace EFCoreAdventure.Mapping
{
    public interface IProductRepository : IReadRepository<Product>
    {
        Task<Product> Contains(string producyName);
    }
}
