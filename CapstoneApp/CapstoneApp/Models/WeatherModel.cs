using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneApp.Shared.Models
{
    public class WeatherModel
    {
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
