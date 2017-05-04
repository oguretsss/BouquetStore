using System.ComponentModel.DataAnnotations;

namespace BouquetStore.Domain.Entities
{
  public class OrderDetails
  {
    [Key]
    public int OrderId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string OrderComment { get; set; }

    public bool IsOriginalOrder { get; set; }
  }
}
