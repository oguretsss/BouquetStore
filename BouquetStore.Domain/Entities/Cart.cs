using BouquetStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouquetStore.Domain.Entities
{
  public class Cart
  {
    private List<CartLine> lineCollection = new List<CartLine>();

    public void AddItem(ProductAbstract product, int quantity)
    {
      CartLine line = lineCollection.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();

      if (line != null)
      {
        line.Quantity += quantity;
      }
      else
      {
        lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
      }
    }

    public void DecrementItem(ProductAbstract product)
    {
      CartLine line = lineCollection.Where(x => x.Product.ProductID == product.ProductID).FirstOrDefault();
      if (line != null)
      {
        line.Quantity--;
        if (line.Quantity < 1)
        {
          lineCollection.RemoveAll(x => x.Product.ProductID == product.ProductID);
        }
      }
    }

    public void RemoveLine(ProductAbstract product)
    {
      lineCollection.RemoveAll(x => x.Product.ProductID == product.ProductID);
    }

    public decimal ComputeTotalValue()
    {
      return lineCollection.Sum(p => p.Product.Price * p.Quantity);
    }

    public void Clear()
    {
      lineCollection.Clear();
    }

    public IEnumerable<CartLine> Lines
    {
      get { return lineCollection; }
    }
  }
}
