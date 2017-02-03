using BouquetStore.Domain.Abstract;
using System.Collections.Generic;
using BouquetStore.Domain.Entities;

namespace BouquetStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
