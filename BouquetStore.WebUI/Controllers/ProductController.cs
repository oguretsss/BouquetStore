using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using BouquetStore.WebUI.Models;

namespace BouquetStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category)
        {
            ProductsListViewModel<Product> model = new ProductsListViewModel<Product>
            {
                Products = repository.Products.Where(
                    p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID),
                CurrentCategory = category
            };
            return View(model);
        }

        public FileResult GetImage(int productId)
        {
            Product prod = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null && prod.ImageData != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return new FilePathResult(@"~/Content/images/image-not-available.jpg", "image/jpeg");
            }
        }
    }
}