using System;
using CapstoneApp.Shared.Constants;
using CapstoneApp.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Views;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class WeatherLocationDetailsViewModel : BaseViewModel
    {
        private bool _zipVisible;
        private bool _latLongVisible;
        private bool _cityCountryVisible;
        private WeatherModel _item;

        public WeatherModel Item
        {
            get { return _item;}
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }
        
        public bool ZipVisible
        {
            get { return _zipVisible; }
            set 
            {
                if (_zipVisible != value)
                {
                    _zipVisible = value; 
                    OnPropertyChanged(nameof(ZipVisible));
                }
            }
        }
        
        public bool LatLongVisible
        {
            get { return _latLongVisible;}
            set 
            {
                if (_latLongVisible != value)
                {
                    _latLongVisible = value;
                    OnPropertyChanged(nameof(LatLongVisible));
                }
            }
        }

        public bool CityCountryVisible
        {
            get { return _cityCountryVisible;}
            set
            {
                if (_cityCountryVisible != value)
                {
                    _cityCountryVisible = value; 
                    OnPropertyChanged(nameof(CityCountryVisible));
                }
            }
        }

        public WeatherLocationDetailsViewModel(WeatherModel item)
        {
            Item = new WeatherModel();
            MessagingCenter.Subscribe<NewWeatherLocationPage, WeatherModel>(this, "SaveWeatherChanges", async (obj, itemRec) => await SaveEntity(new WeatherLocations(itemRec), obj));
            MessagingCenter.Subscribe<NewWeatherLocationPage, int>(this, "NewWeatherLocationIndexChange", ChangeViewByIndex);
            ZipVisible = false;
            CityCountryVisible = false;
            LatLongVisible = false;
        }
        async Task<Plugin.Geolocator.Abstractions.Position> GetLocation()
        {
            var locator = CrossGeolocator.Current;
            return await locator.GetLastKnownLocationAsync();
        }
        private void ChangeViewByIndex(ContentPage page, int index)
        {
            var weatherPage = (NewWeatherLocationPage) page;
            Item.LocationProvider = (WeatherSettings.Location) index;
            if(index == (int)WeatherSettings.Location.ZIP)
            {
                ZipVisible = true;
                LatLongVisible = false;
                Item.Latitude = "";
                Item.Longitude = "";
                CityCountryVisible = false;
                Item.City = "";
                Item.CountryCode = "";
                //Item.LocationProvider = WeatherSettings.Location.ZIP;
                OnPropertyChanged(nameof(Item));
            }
            else if(index == (int)WeatherSettings.Location.Coordinates)
            {   
                LatLongVisible = true; 
                ZipVisible = false;
                Item.ZipCode = "";
                CityCountryVisible = false;
                Item.City = "";
                Item.CountryCode = "";
                GetLocation().ContinueWith(t => {
                    if(t.IsCompleted)
                    {
                        Item.Latitude = t.Result.Latitude.ToString();
                        Item.Longitude = t.Result.Longitude.ToString();
                    }
                });
                //Item.LocationProvider = WeatherSettings.Location.Coordinates;
                OnPropertyChanged(nameof(Item));
            }
            else if(index == (int)WeatherSettings.Location.CityCountry)
            {
                CityCountryVisible = true;
                LatLongVisible = false;
                Item.Latitude = "";
                Item.Longitude = "";
                ZipVisible = false;
                Item.ZipCode = "";
                //Item.LocationProvider = WeatherSettings.Location.CityCountry;
                OnPropertyChanged(nameof(Item));
            }
        }
        public List<string> LocationDropdowns => WeatherSettings.LocationDropdownOptions.ToList();
        public ObservableCollection<Country> CountryNames => new ObservableCollection<Country>(Country.List.ToList());

    }
}
