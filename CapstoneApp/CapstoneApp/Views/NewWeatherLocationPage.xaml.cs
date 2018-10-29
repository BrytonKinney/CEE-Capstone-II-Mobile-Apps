using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.ViewModels;
using Plugin.Geolocator;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VC = CapstoneApp.Shared.Constants.ViewConstants;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewWeatherLocationPage : ContentPage
	{
		public WeatherModel Item = new WeatherModel();
		public WeatherLocationDetailsViewModel viewModel;
		public NewWeatherLocationPage ()
		{
			InitializeComponent ();
			Title = VC.NEW_WEATHER_LOCATION;
			BindingContext = viewModel = new WeatherLocationDetailsViewModel();
		}
		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddWeatherLocation", Item);
			await Navigation.PopModalAsync();
		}
		async void Cancel_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
		async Task<Plugin.Geolocator.Abstractions.Position> GetLocation()
		{
			var locator = CrossGeolocator.Current;
			return await locator.GetLastKnownLocationAsync();
		}
		private void LocationSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker p = (Picker)sender;
			if(p.SelectedIndex == (int)WeatherSettings.Location.ZIP)
			{
				ZIPCodeEntries.IsVisible = true;
				CoordinatesEntries.IsVisible = false;
				CityCountryEntries.IsVisible = false;
			}
			else if(p.SelectedIndex == (int)WeatherSettings.Location.Coordinates)
			{   
				CoordinatesEntries.IsVisible = true; 
				ZIPCodeEntries.IsVisible = false;
				CityCountryEntries.IsVisible = false;
				GetLocation().ContinueWith(t => {
					if(t.IsCompleted)
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							LatitudeEntry.Text = t.Result.Latitude.ToString();
							LongitudeEntry.Text = t.Result.Longitude.ToString();
						});
					}
				});
			}
			else if(p.SelectedIndex == (int)WeatherSettings.Location.CityCountry)
			{
				CityCountryEntries.IsVisible = true;
				CoordinatesEntries.IsVisible = false; 
				ZIPCodeEntries.IsVisible = false;
			}
		}

		private void CountryDropdowns_SelectedIndexChanged(object sender, EventArgs e)
		{
			Picker p = (Picker)sender;
			Country selCountry = (Country)p.SelectedItem;
			Item.CountryCode = selCountry.TwoLetterCode;
		}

		private void CityEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var entry = (Entry)sender;
			Item.City = entry?.Text ?? "";
		}

		private void LatitudeEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var entry = (Entry)sender;
			Item.Latitude = entry?.Text ?? "";
		}

		private void LongitudeEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var entry = (Entry)sender;
			Item.Longitude = entry?.Text ?? "";
		}

		private void ZipCodeEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var entry = (Entry)sender;
			Item.ZipCode = entry?.Text ?? "";
		}

		private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var entry = (Entry)sender;
			Item.Name = entry?.Text ?? "";
		}
	}
}