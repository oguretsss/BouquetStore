using System.Collections.Generic;
using BouquetStore.Domain.Entities;

namespace BouquetStore.WebUI.Models
{
    public class ProductsListViewModel<T>
    {
        public IEnumerable<T> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}