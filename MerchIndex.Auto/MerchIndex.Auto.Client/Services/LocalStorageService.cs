using Microsoft.JSInterop;
using System.Text.Json;

namespace MerchIndex.Auto.Client.Services
{
    //https://stackoverflow.com/questions/49704857/how-can-i-access-the-browsers-localstorage-in-blazor
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetAsync(string key, object value)
        {
            string jsVal = null;
            if (value != null)
                jsVal = JsonSerializer.Serialize(value);
            await _js.InvokeVoidAsync("localStorage.setItem", new object[] { key, jsVal });
        }
        public async Task<T> GetAsync<T>(string key)
        {
            string val = await _js.InvokeAsync<string>("localStorage.getItem", key);
            if (val == null) return default;
            T result = JsonSerializer.Deserialize<T>(val);
            return result;
        }
        public async Task RemoveAsync(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }
        public async Task ClearAsync()
        {
            await _js.InvokeVoidAsync("localStorage.clear");
        }
    }
}
