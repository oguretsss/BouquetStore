using BouquetStore.Domain.Entities;
using System.Collections.Generic;

namespace BouquetStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);

        IEnumerable<SeasonPromoProduct> PromoProducts { get; }
        void SavePromoProduct(SeasonPromoProduct product);
        SeasonPromoProduct DeletePromoProduct(int productID);
    }
}
