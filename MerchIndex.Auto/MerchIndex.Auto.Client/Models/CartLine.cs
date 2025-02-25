namespace MerchIndex.Auto.Client.Models
{
    public class CartLine
    {
        public int Id { get; set; }
        public long? ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
