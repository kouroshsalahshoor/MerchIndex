using Persilsoft.Nominatim.Geolocation.Blazor;
using Persilsoft.Nominatim.Geolocation.Blazor.Geocoding;

namespace MerchIndex.Auto.Client.Services
{
    public class LocationService
    {
        private readonly GeolocationService _geolocation;
        private readonly IGeocoder _geocoderService;

        public LocationService(GeolocationService geolocation, IGeocoder geocoderService)
        {
            _geolocation = geolocation;
            _geocoderService = geocoderService;
        }
        public async Task<string?> GetAddress()
        {
            try
            {
                var position = await _geolocation.GetPosition();
                if (!position.Equals(default))
                {
                    var latitude = position.Latitude;
                    var longitude = position.Longitude;

                    var address = await _geocoderService.GetGeocodingAddressAsync(latitude, longitude);
                    return address.DisplayAddress;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
