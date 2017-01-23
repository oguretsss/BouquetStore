using BouquetStore.Domain.Entities;
using System.Collections.Generic;

namespace BouquetStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
