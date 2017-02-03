using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.
                FirstOrDefault(x => x.ProductID == productID);
            return View(product);
        }
    }
}