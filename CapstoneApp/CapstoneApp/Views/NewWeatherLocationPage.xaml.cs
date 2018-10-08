using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.ViewModels;
using Shared.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewWeatherLocationPage : ContentPage
	{
		public WeatherModel Item { get; set; }
        public WeatherLocationDetailsModel viewModel;
		public NewWeatherLocationPage ()
		{
			InitializeComponent ();
			Item = new WeatherModel
			{
				Name = ""
			};
            BindingContext = viewModel = new WeatherLocationDetailsModel();
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
            }
            else if(p.SelectedIndex == (int)WeatherSettings.Location.CityCountry)
            {
                CityCountryEntries.IsVisible = true;
                CoordinatesEntries.IsVisible = false; 
                ZIPCodeEntries.IsVisible = false;
            }
		}
	}
}