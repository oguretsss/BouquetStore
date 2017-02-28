using BouquetStore.Domain.Abstract;
using System.ComponentModel;
using System.Web.Mvc;

namespace BouquetStore.Domain.Entities
{
    public class SeasonPromoProduct : ProductAbstract
    {
        [DisplayName("Второе изображение")]
        public byte[] ImageDataSecondary { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeTypeSecondary { get; set; }
        [DisplayName("Порядковый номер для сортировки")]
        public int ImageNumberInGrid { get; set; }
    }
}
