using System.Collections.Generic;
using BouquetStore.Domain.Entities;

namespace BouquetStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}