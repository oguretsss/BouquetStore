using BouquetStore.Domain.Entities;
using System.Data.Entity;

namespace BouquetStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SeasonPromoProduct> SeasonPromoProducts { get; set; }
    }
}
