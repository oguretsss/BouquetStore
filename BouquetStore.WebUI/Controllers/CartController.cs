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
    public ViewResult Index(Cart cart, string returnUrl)
    {
      return View(new CartIndexViewModel
      {
        Cart = cart,
        ReturnUrl = returnUrl
      });
    }
    
    [HttpPost]
    public ActionResult AddToCart(Cart cart, int productID, string returnUrl, bool isPromo = false)
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
        cart.AddItem(product, 1);
      }

      if(Request.IsAjaxRequest())
      {
        return PartialView("Summary", cart);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl, bool isPromo = false)
    {
      ProductAbstract product;
      if (isPromo)
      {
        product = repository.PromoProducts.FirstOrDefault(x => x.ProductID == productID);
      }
      else
      {
        product = repository.Products.FirstOrDefault(x => x.ProductID == productID);
      }

      if (product != null)
      {
        cart.RemoveLine(product);
      }
      return RedirectToAction("Index", new { returnUrl });
    }

    public PartialViewResult Summary(Cart cart)
    {
      return PartialView(cart);
    }

  }
}