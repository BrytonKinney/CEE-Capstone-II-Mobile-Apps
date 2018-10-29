using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Constants;

namespace CapstoneApp.Shared.Models
{
    public class WeatherModel
    {
        public WeatherModel() {}
        public WeatherModel(WeatherLocations loc)
        {
            Name = loc.Name;
            Enabled = loc.Enabled == 1;
            if(loc.LocationCode == WeatherSettings.Location.CityCountry)
            {
                string[] locSplit = loc.LocationString.Split(',');
                City = locSplit[0];
                CountryCode = locSplit[1];
            }
            else if(loc.LocationCode == WeatherSettings.Location.Coordinates)
            {
                string[] locSplit = loc.LocationString.Split(',');
                Latitude = locSplit[0];
                Longitude = locSplit[1];
            }
            else if(loc.LocationCode == WeatherSettings.Location.ZIP)
                ZipCode = loc.LocationString;
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
