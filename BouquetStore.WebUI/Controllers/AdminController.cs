using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using BouquetStore.WebUI.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BouquetStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ActionResult Index(string category)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.Where(
                    p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID),
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.
                FirstOrDefault(x => x.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (image != null)
            {
                product.ImageMimeType = image.ContentType;
                product.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(product.ImageData, 0, image.ContentLength);
            }
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Продукт \"{0}\" успешно сохранен", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Продукт \"{0}\" успешно удален",
                deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}