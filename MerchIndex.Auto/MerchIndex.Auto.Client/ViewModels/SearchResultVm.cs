using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Client.ViewModels
{
    public class SearchResultVm
    {
        public Product? Product { get; set; }
        public Company? Company { get; set; }
        public string City { get; set; } = default!;
        public string District { get; set; } = default!;
    }
}
