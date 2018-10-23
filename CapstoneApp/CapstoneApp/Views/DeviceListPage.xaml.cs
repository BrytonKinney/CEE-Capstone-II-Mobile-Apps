using CapstoneApp.Shared.Models;
using System.Collections.ObjectModel;
using CapstoneApp.Shared.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Shared.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceListPage : ContentPage
    {
       

		public DeviceListPage(DevicesViewModel vm)
		{
		    BindingContext = vm = new DevicesViewModel();
			InitializeComponent();
		}
    }
}
