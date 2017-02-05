using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BouquetStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "У продукта должно быть название")]
        [DisplayName("Название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "У продукта должно быть описание")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Минимальное значение цены: 0.01 рубля")]
        [DisplayName("Цена в рублях")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "К какой категории отнести продукт?")]
        [DisplayName("Категория")]
        public string Category { get; set; }
        [DisplayName("Главное изображение")]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
