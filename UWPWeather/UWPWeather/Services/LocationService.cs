using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UWPWeather.Services
{
    public class LocationService
    {
        public static LocationService Instance { get; } = new LocationService();

        private LocationService()
        {

        }

        public async Task<Geocoordinate> GetCoordinateAsync()
        {
            var status = await Geolocator.RequestAccessAsync();

            if (status == GeolocationAccessStatus.Allowed)
            {
                var geoPosition = await new Geolocator().GetGeopositionAsync();
                var coordinate = geoPosition.Coordinate;
                return coordinate;
            }
            return null;
        }
    }
}
