using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MerchIndex.Auto.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using ServiceCollectionExtensions;
using Persilsoft.Nominatim.Geolocation.Blazor;
using Persilsoft.Nominatim.Geolocation.Blazor.Geocoding;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// register api service for calls originating from WASM
builder.Services.AddScoped<IApiService, ClientApiService>();

builder.Services.AddKeyedScoped<HttpClient>("LocalAPIClientFromWASM",
    (sp, key) =>
       new HttpClient
       {
           BaseAddress = new Uri(builder.Configuration["LocalAPIBaseAddress"] ??
                throw new Exception("LocalAPIBaseAddress is missing."))
       });

builder.Services.AddKeyedScoped<HttpClient>("RemoteAPIClientFromWASM",
    (sp, key) =>
       new HttpClient
       {
           BaseAddress = new Uri(builder.Configuration["RemoteAPIBaseAddress"] ??
                throw new Exception("RemoteAPIBaseAddress is missing."))
       });

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7001/api/")
    });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7001/api/") });

builder.Services.AddSingleton<LocalStorageService>();

builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddGeolocationService(); // must be added in server side too
builder.Services.AddNominatimGeocoderService(); // must be added in server side too
builder.Services.AddSingleton<GeolocationService>();

await builder.Build().RunAsync();
