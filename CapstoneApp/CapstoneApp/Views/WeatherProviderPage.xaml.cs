using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherProviderPage : ContentPage
	{
		WeatherServicesModel viewModel;
		public WeatherProviderPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel = new WeatherServicesModel();
		}
		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as WeatherModel;
			if (item == null)
				return;

			// Manually deselect item.
			ItemsListView.SelectedItem = null;
			await Navigation.PushModalAsync(new NavigationPage(new WeatherModelDetailsPage(item)));
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new NewWeatherLocationPage()));
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Services.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}