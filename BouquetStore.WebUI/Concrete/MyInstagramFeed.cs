using BouquetStore.WebUI.Abstract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BouquetStore.WebUI.Concrete
{
    public class MyInstagramFeed : IInstagramFeed
    {
        public List<string> GetLatestPhotosFromFeed(int quantity, string accountName)
        {
            string json = string.Empty;
            string url = string.Format("https://www.instagram.com/{0}/media", accountName);

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
                                .Take(quantity)
                                .ToList();
            return imageList;
        }
    }
}