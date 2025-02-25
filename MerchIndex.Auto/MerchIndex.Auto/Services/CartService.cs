using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Services
{
    public class CartService
    {
        private string? _summaryString;
        public string? SummaryString
        {
            get => _summaryString ?? $"{Lines.Sum(x => x.Quantity)} item(s), Total: {ComputeTotalValue():C2}";
            set
            {
                _summaryString = value;
                NotifyStateChanged();
            }
        }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual async Task AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(p => p.Product?.Id == product.Id).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual async Task RemoveLine(Product product) => Lines.RemoveAll(l => l.Product?.Id == product.Id);
        public decimal ComputeTotalValue() => Lines.Sum(e => e.Product!.Price * e.Quantity);
        public virtual async Task Clear() => Lines.Clear();
        public virtual async Task Load() { }
        public async Task setSummary()
        {
            SummaryString = $"{Lines.Sum(x => x.Quantity)} item(s), Total: {ComputeTotalValue():C2}";
        }


        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
