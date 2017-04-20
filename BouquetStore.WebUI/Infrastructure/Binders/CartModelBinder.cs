using BouquetStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BouquetStore.WebUI.Infrastructure.Binders
{
  public class CartModelBinder : IModelBinder
  {
    private const string SESSION_KEY = "Cart";
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      Cart cart = null;
      if (controllerContext.HttpContext.Session != null)
      {
        cart = (Cart)controllerContext.HttpContext.Session[SESSION_KEY];
      }

      if (cart == null)
      {
        cart = new Cart();
        if (controllerContext.HttpContext.Session != null)
        {
          controllerContext.HttpContext.Session[SESSION_KEY] = cart;
        }
      }

      return cart;
    }
  }
}