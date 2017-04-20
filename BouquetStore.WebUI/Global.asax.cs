using BouquetStore.Domain.Concrete;
using BouquetStore.Domain.Entities;
using BouquetStore.WebUI.Infrastructure.Binders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BouquetStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Uncomment this to prevent data migration errors
            //Database.SetInitializer<EFDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
    }
    }
}
