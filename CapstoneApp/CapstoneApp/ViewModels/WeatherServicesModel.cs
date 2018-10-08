using CapstoneApp.Shared.Models;
using CapstoneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class WeatherServicesModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<WeatherModel> Services { get; set; }
        public WeatherServicesModel()
        {
            Services = new ObservableCollection<WeatherModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Services.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
