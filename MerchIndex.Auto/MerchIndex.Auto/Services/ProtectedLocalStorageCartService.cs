using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MerchIndex.Auto.Client.Models;

namespace MerchIndex.Auto.Services
{
    public class ProtectedLocalStorageCartService : CartService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public ProtectedLocalStorageCartService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public override async Task Load()
        {
            var result = await _protectedLocalStorage.GetAsync<List<CartLine>>("cart");
            Lines = result.Success ? result.Value! : new List<CartLine>();

            await setSummary();
        }

        public override async Task AddItem(Product product, int quantity)
        {
            await base.AddItem(product, quantity);
            await setStorage();
        }
        public override async Task RemoveLine(Product product)
        {
            await base.RemoveLine(product);
            await setStorage();
        }

        public override async Task Clear()
        {
            await base.Clear();
            await _protectedLocalStorage.DeleteAsync("cart");
            await setSummary();
        }
        private async Task setStorage()
        {
            await _protectedLocalStorage.SetAsync("cart", Lines);
            await setSummary();
        }
    }
}
