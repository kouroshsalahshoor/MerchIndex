using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        [EmailAddress(ErrorMessage = "Fyll i en giltig e-postadress")]
        public string Email { get; set; } = string.Empty;
    }
}
