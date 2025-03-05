using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        [EmailAddress(ErrorMessage = "Fyll i en giltig e-postadress")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i Förnamn")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i Efternamn")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i Ämne")]
        public string Subject { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Body { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Tel { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? AnsweredOn { get; set; }
    }
}
