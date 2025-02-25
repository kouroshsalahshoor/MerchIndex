using MerchIndex.Auto.Client.Pages.Demos.Auth;

namespace MerchIndex.Auto.Client.Services
{
    public interface IApiService
    {
        Task<IEnumerable<Band>> CallLocalApiAsync();

        Task<IEnumerable<Band>> CallRemoteApiAsync();

        Task<string> SaveImageLocalApiAsync(string imageData);
    }
}
