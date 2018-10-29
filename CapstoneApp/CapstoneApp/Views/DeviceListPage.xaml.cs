using CapstoneApp.Shared.Models;
using System.Collections.ObjectModel;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Services.Interfaces;
using CapstoneApp.Shared.ViewModels;
using CapstoneApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LightInject;
using Shared.Services.Interfaces;

namespace CapstoneApp.Shared.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeviceListPage : ContentPage
	{
		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as SmartMirrorModel;
			if (item == null)
				return;
			App.Container.GetInstance<ISmartMirrorService>().SetInstance(item);
			deviceListView.SelectedItem = null;
			SmartMirror selectedMirror = new SmartMirror(item);
			var _db = App.Container.GetInstance<IDatabaseProvider>();
			await _db.AddOrUpdateAsync(selectedMirror);
			await Navigation.PushAsync(new RssFeedsPage());
		}

		public DeviceListPage()
		{
			Title = "Select a Smart Mirror";
			DevicesViewModel vm;
			BindingContext = vm = new DevicesViewModel();
			InitializeComponent();
		}
	}
}
