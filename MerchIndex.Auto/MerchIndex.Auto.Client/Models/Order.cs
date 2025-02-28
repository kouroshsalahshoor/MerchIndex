using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models
{
    public class Order
    {
        //[BindNever]
        public int Id { get; set; }
        //[BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
        [Required(ErrorMessage = "Fyll i {0}")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string? State { get; set; }
        public string? Zip { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string? Country { get; set; }
        public bool GiftWrap { get; set; }
        public bool Shipped { get; set; }
    }
}