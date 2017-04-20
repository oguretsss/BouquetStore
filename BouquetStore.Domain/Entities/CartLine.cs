using BouquetStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouquetStore.Domain.Entities
{
  public class CartLine
  {
    public ProductAbstract Product { get; set; }
    public int Quantity { get; set; }
  }
}
