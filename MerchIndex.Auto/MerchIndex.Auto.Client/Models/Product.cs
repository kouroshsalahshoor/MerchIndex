using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchIndex.Auto.Client.Models
{
    public class Product
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fyll i en positiv {0}")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Category { get; set; } = string.Empty;
        public string? Tag { get; set; }
        public string? Manifacturer { get; set; }
        public string? ImageUrl { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int PercentOff { get; set; }
        public bool IsNew { get; set; }
        public bool IsHot { get; set; }

    }
}
