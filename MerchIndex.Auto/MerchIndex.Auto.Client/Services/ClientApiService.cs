using MerchIndex.Auto.Client.Pages.Demos.Auth;
using System.Text;
using System.Text.Json;

namespace MerchIndex.Auto.Client.Services
{
    public class ClientApiService([FromKeyedServices("LocalAPIClientFromWASM")] HttpClient localAPIClient,
    [FromKeyedServices("RemoteAPIClientFromWASM")] HttpClient remoteAPIClient) : IApiService
    {
        private readonly HttpClient _localAPIClient = localAPIClient;
        private readonly HttpClient _remoteAPIClient = remoteAPIClient;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task<IEnumerable<Band>> CallLocalApiAsync()
        {
            var response = await _localAPIClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Get,
                    "localapi/bands"));
            response.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<List<Band>>(
                await response.Content.ReadAsStreamAsync(),
                _jsonSerializerOptions,
                CancellationToken.None) ?? [];
        }

        public async Task<IEnumerable<Band>> CallRemoteApiAsync()
        {
            var response = await _remoteAPIClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Get,
                    "remoteapi/bands"));
            response.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<List<Band>>(
               await response.Content.ReadAsStreamAsync(),
               _jsonSerializerOptions,
               CancellationToken.None) ?? [];
        }

        public async Task<string> SaveImageLocalApiAsync(string imageData)
        {
            // Create the request message
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/Image/save");

            // Set the content of the request
            var jsonContent = "{\"ImageData\":\"" + imageData + "\"}";
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _localAPIClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return "";

            //return await JsonSerializer.DeserializeAsync<string>(
            //    await response.Content.ReadAsStreamAsync(),
            //    _jsonSerializerOptions,
            //    CancellationToken.None) ?? [];
        }
    }
}
