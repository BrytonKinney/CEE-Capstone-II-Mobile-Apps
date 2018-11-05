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
		//public WeatherModel Item = new WeatherModel();
		public WeatherLocationDetailsViewModel viewModel;

	    public NewWeatherLocationPage ()
		{
			InitializeComponent ();
			Title = VC.NEW_WEATHER_LOCATION;
			BindingContext = viewModel = new WeatherLocationDetailsViewModel(new WeatherModel());
		}
		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddWeatherLocation", viewModel.Item);
			await Navigation.PopModalAsync();
		}
		async void Cancel_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

	    private void LocationSelection_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        var pick = (Picker) sender;
	        MessagingCenter.Send(this, "NewWeatherLocationIndexChange", pick.SelectedIndex);
	    }
	}
}