using BouquetStore.WebUI.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BouquetStore.WebUI.Controllers
{
    public class InstagramController : Controller
    {
        private IInstagramFeed provider;
        public InstagramController(IInstagramFeed feedProvider)
        {
            provider = feedProvider;
        }
        // GET: Instagram
        public PartialViewResult Feed(int quantity = 9)
        {
            List<string> photos = provider.GetLatestPhotosFromFeed(quantity, "fruit_is_goood");
            return PartialView(photos);
        }
    }
}