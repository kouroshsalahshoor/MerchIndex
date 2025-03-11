using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models;

public class Company
{
    public int Id { get; set; }
    [DisplayName("Företagsnamn")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Name { get; set; } = string.Empty;
    [DisplayName("Verksamhetsområde")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string BusinessArea { get; set; } = string.Empty;
    [DisplayName("Email")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Email { get; set; } = string.Empty;
    [DisplayName("Telefonnummer")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Tel { get; set; } = string.Empty;
    [DisplayName("Adress")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string Address { get; set; } = string.Empty;
    [DisplayName("Område")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string District { get; set; } = string.Empty;
    [DisplayName("Stad")]
    [Required(ErrorMessage = "Fyll i {0}")]
    public string City { get; set; } = string.Empty;
    public string AdminId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HasBeenActivated { get; set; }
    public DateTime ActiveOn { get; set; } = new ();
    public List<Product>? Products { get; set; }
    //public List<Service>? Services { get; set; }
    [DisplayName("Websida")]
    public string? Url { get; set; }

}
