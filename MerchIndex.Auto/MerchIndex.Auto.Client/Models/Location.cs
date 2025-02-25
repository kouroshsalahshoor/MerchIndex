namespace MerchIndex.Auto.Client.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = String.Empty;
        public IEnumerable<Person>? People { get; set; }
    }
}
