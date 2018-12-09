using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Views;
using CapstoneApp.ViewModels;
using LightInject;
using Shared.Services.Interfaces;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class WeatherModelDetailsViewModel : BaseViewModel
    {
        public WeatherModel Item { get; set; }
        public WeatherModelDetailsViewModel() {}

        public bool IsZipVisible { get; set; }
        public bool IsCoordsVisible { get; set; }
        public bool IsCityCountryVisible { get; set; }

        private IDatabaseProvider _dbDriver;
        public WeatherModelDetailsViewModel(WeatherModel item)
        {
            _dbDriver = App.Container.GetInstance<IDatabaseProvider>();
            Item = item;
            if(!string.IsNullOrWhiteSpace(item.ZipCode))
                IsZipVisible = true;
            if(!string.IsNullOrWhiteSpace(item.City))
                IsCityCountryVisible = true;
            if(!string.IsNullOrWhiteSpace(item.Latitude))
                IsCoordsVisible = true;
            MessagingCenter.Unsubscribe<WeatherModelDetailsPage, WeatherModel>(this, "SaveWeatherChanges");
            MessagingCenter.Subscribe<WeatherModelDetailsPage, WeatherModel>(this, "SaveWeatherChanges", async (obj, itemRec) =>
            {
                await SaveEntity(new WeatherLocations(itemRec), obj);
                Device.BeginInvokeOnMainThread(async () => { await obj.Navigation.PopAsync(); });
            });
        }
    }
}
