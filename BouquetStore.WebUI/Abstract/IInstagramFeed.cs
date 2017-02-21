using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouquetStore.WebUI.Abstract
{
    public interface IInstagramFeed
    {
        List<string> GetLatestPhotosFromFeed(int quantity, string accountName);
    }
}
