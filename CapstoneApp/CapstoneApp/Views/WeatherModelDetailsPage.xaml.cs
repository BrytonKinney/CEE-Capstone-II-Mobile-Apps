using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherModelDetailsPage : ContentPage
	{
		WeatherModelDetailsViewModel viewModel;
		WeatherModel Item;
		public WeatherModelDetailsPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel = new WeatherModelDetailsViewModel();
		}
		public WeatherModelDetailsPage(WeatherModel item)
		{
			Item = item;
			InitializeComponent();
			BindingContext = viewModel = new WeatherModelDetailsViewModel(item);
		}
		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "SaveWeatherChanges", Item);
			await Navigation.PopModalAsync();
		}
	}
}