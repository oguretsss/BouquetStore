using BouquetStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BouquetStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;
        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult ProductCategories(string caller = "Product", string actionName = "List", string category = null)
        {
            ViewBag.controllerName = caller;
            ViewBag.actionName = actionName;
            ViewBag.selectedCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            switch (caller)
            {
              case "Product":
                return PartialView("ShopCategories", categories);
              case "Admin":
                return PartialView("ProductCategories", categories);
              default:
                return PartialView("ProductCategories", categories);
            }
        }
    }
}