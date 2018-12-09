using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Net.Wifi;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Services.Interfaces;
using CapstoneApp.ViewModels;
using Xamarin.Forms;
using LightInject;
using Zeroconf;
using System.Reactive.Linq;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Views;

namespace CapstoneApp.Shared.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<SmartMirrorModel> DiscoveredDevices { get; private set; }
        public ISmartMirrorService _smService;
        public DevicesViewModel()
        {
            DiscoveredDevices = new ObservableCollection<SmartMirrorModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _smService = App.Container.GetInstance<ISmartMirrorService>();
            MessagingCenter.Unsubscribe<DeviceListPage, SmartMirror>(this, "MirrorSelected");
            MessagingCenter.Subscribe<DeviceListPage, SmartMirror>(this, "MirrorSelected", async (page, mirror) =>
            {
                await DbProvider.GetConnection().DeleteAllAsync<SmartMirror>();
                await DbProvider.AddOrUpdateAsync(mirror);
                _smService.SetInstance(new SmartMirrorModel(mirror));
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Smart Mirror Selected", "Smart Mirror has been selected.", "OK");
                });
                await page.Navigation.PopModalAsync();
            });
            LoadItemsCommand.Execute(null);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                DiscoveredDevices.Clear();
                var smartMirrors = await _smService.SearchForSmartMirrors();
                foreach (var mirror in smartMirrors)
                {
                    DiscoveredDevices.Add(mirror);
                }
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
