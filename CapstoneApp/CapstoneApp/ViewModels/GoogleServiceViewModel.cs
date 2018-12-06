using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Views;
using CapstoneApp.ViewModels;
using LightInject;
using Shared.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class GoogleServiceViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<GoogleDataModel> Services { get; set; }
        private IDatabaseProvider _dbDriver;

        public GoogleServiceViewModel()
        {
            _dbDriver = App.Container.GetInstance<IDatabaseProvider>();

            //Delete table
            //_dbDriver.GetConnection().DropTableAsync<GoogleEntity>();

            Services = new ObservableCollection<GoogleDataModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);
            MessagingCenter.Subscribe<GooglePage, GoogleDataModel>(this, "AddGoogleAccount", async (page, item) =>
            {
                await SaveEntity(new GoogleEntity(item), page).ContinueWith(async (t) =>
                {
                    if (t.IsCompleted)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Account Added", "Google Account Added.", "OK");
                            await page.Navigation.PopAsync();
                            new Command(async () => await ExecuteLoadItemsCommand()).Execute(null);
                        });

                    }

                });
            });

        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Services.Clear(); 
                var googleEntities = await _dbDriver.GetConnection().Table<GoogleEntity>().ToListAsync();
                if (googleEntities.Count > 0)
                {
                    var newModels = googleEntities.Select(x => new GoogleDataModel(x));
                    foreach (var m in newModels)
                        Services.Add(m);
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
