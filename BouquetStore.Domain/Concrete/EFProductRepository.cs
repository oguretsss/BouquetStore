using BouquetStore.Domain.Abstract;
using System.Collections.Generic;
using BouquetStore.Domain.Entities;
using System;

namespace BouquetStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public IEnumerable<SeasonPromoProduct> PromoProducts
        {
            get { return context.SeasonPromoProducts; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    if (product.ImageData != null)
                    {
                        dbEntry.ImageData = product.ImageData;
                        dbEntry.ImageMimeType = product.ImageMimeType;
                    }
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if(dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }

        public void SavePromoProduct(SeasonPromoProduct product)
        {
            if (product.ProductID == 0)
            {
                context.SeasonPromoProducts.Add(product);
            }
            else
            {
                SeasonPromoProduct dbEntry = context.SeasonPromoProducts.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    if (product.ImageData != null)
                    {
                        dbEntry.ImageData = product.ImageData;
                        dbEntry.ImageMimeType = product.ImageMimeType;
                    }
                    if (product.ImageDataSecondary != null)
                    {
                        dbEntry.ImageDataSecondary = product.ImageDataSecondary;
                        dbEntry.ImageMimeTypeSecondary = product.ImageMimeTypeSecondary;
                    }
                }
            }
            context.SaveChanges();
        }

        public SeasonPromoProduct DeletePromoProduct(int productID)
        {
            SeasonPromoProduct dbEntry = context.SeasonPromoProducts.Find(productID);
            if (dbEntry != null)
            {
                context.SeasonPromoProducts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
