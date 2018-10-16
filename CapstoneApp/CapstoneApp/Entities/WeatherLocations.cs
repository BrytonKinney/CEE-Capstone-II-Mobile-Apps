using CapstoneApp.Shared.Models;
using Shared.Constants;

namespace CapstoneApp.Shared.Entities
{
    [SQLite.Table(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_TABLE)]
    public class WeatherLocations
    {
        public WeatherLocations() { }
        public WeatherLocations(WeatherModel model)
        {
            Name = model.Name;
            if(model.LocationProvider == WeatherSettings.Location.CityCountry)
                LocationString = $"{model.City},{model.CountryCode}";
            else if(model.LocationProvider == WeatherSettings.Location.Coordinates)
                LocationString = $"{model.Latitude},{model.Longitude}";
            else if(model.LocationProvider == WeatherSettings.Location.ZIP)
                LocationString = model.ZipCode;
            LocationCode = model.LocationProvider;
            Enabled = model.Enabled ? 1 : 0;
        }

        [SQLite.Column(DatabaseConstants.ID)]
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Indexed]
        public int? Id { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_NAME)]
        public string Name { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_STRING)]
        public string LocationString { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_LOCATION_CODE)]
        public WeatherSettings.Location LocationCode { get; set; }

        [SQLite.Column(DatabaseConstants.WeatherLocations.WEATHER_ENABLED)]
        public int Enabled { get; set; }
    }
}
