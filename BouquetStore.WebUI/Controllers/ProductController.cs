using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;

namespace BouquetStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private byte[] image;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Products);
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