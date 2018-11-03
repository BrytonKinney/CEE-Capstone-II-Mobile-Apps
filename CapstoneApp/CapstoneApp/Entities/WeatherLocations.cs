using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;

namespace CapstoneApp.Shared.Entities
{
    [SQLite.Table(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_TABLE)]
    public class WeatherLocations : BaseEntity
    {
        public WeatherLocations() { }
        public WeatherLocations(WeatherModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            ZipCode = model.ZipCode;
            City = model.City;
            Country = model.CountryCode;
            LocationCode = model.LocationProvider;
            Enabled = model.Enabled ? 1 : 0;
        }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_NAME)]
        public string Name { get; set; }
        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_LATITUDE)]
        public string Latitude { get; set; }
        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_LONGITUDE)]
        public string Longitude { get; set; }
        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_ZIP)]
        public string ZipCode { get; set; }
        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_CITY)]
        public string City { get; set; }
        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_COUNTRY)]
        public string Country { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_CODE)]
        public WeatherSettings.Location LocationCode { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_ENABLED)]
        public int Enabled { get; set; }
    }
}
