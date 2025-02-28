using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models;

public class Company
{
    public int Id { get; set; }
    //[DisplayName("Company Name")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string BusinessArea { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Tel { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string District { get; set; } = string.Empty;
    [Required(ErrorMessage = "Fyll i {0}")]
    public string City { get; set; } = string.Empty;
    public string AdminId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HasBeenActivated { get; set; }
    public DateTime ActiveOn { get; set; } = new ();
    public List<Product> Products { get; set; } = new ();
    //public List<Service> Services { get; set; } = new ();

}
