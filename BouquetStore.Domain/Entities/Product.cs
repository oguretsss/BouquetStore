using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BouquetStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "У продукта должно быть название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "У продукта должно быть описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Минимальное значение цены: 0.01 рубля")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "К какой категории отнести продукт?")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
