using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using BouquetStore.WebUI.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            ProductsListViewModel<Product> model = new ProductsListViewModel<Product>
            {
                Products = repository.Products.Where(
                    p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID),
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult PromoProducts(string category)
        {
            ProductsListViewModel<SeasonPromoProduct> model = new ProductsListViewModel<SeasonPromoProduct>
            {
                Products = repository.PromoProducts.Where(
                    p => category == null || p.Category == category)
                    .OrderBy(p => p.ImageNumberInGrid),
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

        public ViewResult EditPromo(int productID)
        {
            SeasonPromoProduct product = repository.PromoProducts.
                FirstOrDefault(x => x.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditPromo(SeasonPromoProduct product, HttpPostedFileBase image = null, HttpPostedFileBase imageSecondary = null)
        {
            if (image != null)
            {
                product.ImageMimeType = image.ContentType;
                product.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(product.ImageData, 0, image.ContentLength);
            }
            if(imageSecondary != null)
            {
                product.ImageMimeTypeSecondary = imageSecondary.ContentType;
                product.ImageDataSecondary = new byte[imageSecondary.ContentLength];
                imageSecondary.InputStream.Read(product.ImageDataSecondary, 0, imageSecondary.ContentLength);
            }
            if (ModelState.IsValid)
            {
                repository.SavePromoProduct(product);
                TempData["message"] = string.Format("Промо продукт \"{0}\" успешно сохранен", product.Name);
                return RedirectToAction("PromoProducts");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create(string productType = "Product")
        {
            switch (productType)
            {
                case "SeasonPromo":
                    return View("EditPromo", new SeasonPromoProduct());
                case "Product":
                    return View("Edit", new Product());
                default:
                    return View("Edit", new Product());
            }
            
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

        [HttpPost]
        public ActionResult DeletePromo(int productId)
        {
            SeasonPromoProduct deletedProduct = repository.DeletePromoProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Промо продукт \"{0}\" успешно удален",
                deletedProduct.Name);
            }
            return RedirectToAction("PromoProducts");
        }

        public ViewResult InstaFeed()
        {
            string json = string.Empty;
            string url = @"https://www.instagram.com/fruit_is_goood/media/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            JObject instafeed = JObject.Parse(json);
            JArray images = (JArray)instafeed["items"];
            List<string> imageList = images
                                .Select(img => (string)img["images"]["standard_resolution"]["url"])
                                .Take(9)
                                .ToList();
            return View(imageList);
        }

    }
}