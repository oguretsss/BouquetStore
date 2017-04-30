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
    public class PromoController : Controller
    {
        private IProductRepository repository;
        public PromoController(IProductRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult PromoGrid()
        {
            SeasonPromoProduct[] Products = repository.PromoProducts
                .OrderBy(p => p.ImageNumberInGrid)
                //.Take(4)
                .ToArray();

            return PartialView(Products);
        }

        public FileResult GetImage(int productId, string imageType = "Primary")
        {
            SeasonPromoProduct prod = repository.PromoProducts
            .FirstOrDefault(p => p.ProductID == productId);
            byte[] fileContents;
            string mimeType;
            switch (imageType)
            {
                case "Primary":
                    fileContents = prod.ImageData ?? null;
                    mimeType = prod.ImageMimeType ?? null;
                    break;
                case "Secondary":
                    fileContents = prod.ImageDataSecondary ?? null;
                    mimeType = prod.ImageMimeTypeSecondary ?? null;
                    break;
                default:
                    fileContents = prod.ImageData ?? null;
                    mimeType = prod.ImageMimeType ?? null;
                    break;
            }
            if (prod != null && fileContents != null)
            {
                return File(fileContents, mimeType);
            }
            else
            {
                return new FilePathResult(@"~/Content/images/image-not-available.jpg", "image/jpeg");
            }
        }
    }
}