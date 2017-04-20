using BouquetStore.Domain.Abstract;
using BouquetStore.Domain.Entities;
using BouquetStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BouquetStore.WebUI.Controllers
{
  public class CartController : Controller
  {
    private IProductRepository repository;

    public CartController(IProductRepository repo)
    {
      repository = repo;
    }
    public ViewResult Index(string returnUrl)
    {
      return View(new CartIndexViewModel
      {
        Cart = GetCart(),
        ReturnUrl = returnUrl
      });
    }
    
    [HttpPost]
    public RedirectToRouteResult AddToCart(int productID, string returnUrl, bool isPromo = false)
    {
      ProductAbstract product;
      if(isPromo)
      {
        product = repository.PromoProducts.FirstOrDefault(x => x.ProductID == productID);
      }
      else
      {
        product = repository.Products.FirstOrDefault(x => x.ProductID == productID);
      }

      if (product != null)
      {
        GetCart().AddItem(product, 1);
      }

      return RedirectToAction("Index", new { returnUrl });
    }

    private Cart GetCart()
    {
      Cart cart = (Cart)Session["Cart"];
      if (cart == null)
      {
        cart = new Cart();
        Session["Cart"] = cart;
      }
      return cart;
    }
  }
}