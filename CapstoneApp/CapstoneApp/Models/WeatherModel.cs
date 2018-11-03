using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Constants;

namespace CapstoneApp.Shared.Models
{
    public class WeatherModel
    {
        public int? Id { get; set; }
        public WeatherModel() {}
        public WeatherModel(WeatherLocations loc)
        {
            Id = loc.Id;
            Name = loc.Name;
            Enabled = loc.Enabled == 1;
            LocationProvider = loc.LocationCode;
            if(loc.LocationCode == WeatherSettings.Location.CityCountry)
            {
                City = loc.City;
                CountryCode = loc.Country;
            }
            else if(loc.LocationCode == WeatherSettings.Location.Coordinates)
            {
                Latitude = loc.Latitude;
                Longitude = loc.Longitude;
            }
            else if (loc.LocationCode == WeatherSettings.Location.ZIP)
                ZipCode = loc.ZipCode;

        }
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public string ZipCode { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public WeatherSettings.Location LocationProvider { get; set; }
    }
}
