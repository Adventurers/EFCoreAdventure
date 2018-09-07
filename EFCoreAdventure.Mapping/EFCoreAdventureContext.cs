using EFCoreAdventure.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAdventure.Mapping
{
    public class EFCoreAdventureContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }

        public EFCoreAdventureContext(DbContextOptions<EFCoreAdventureContext> options) : base(options)
        {

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-20TMPOT;Database=EFCoreAdventure;User Id=sa;Password=1234;MultipleActiveResultSets=true");
        //}

    }
}
