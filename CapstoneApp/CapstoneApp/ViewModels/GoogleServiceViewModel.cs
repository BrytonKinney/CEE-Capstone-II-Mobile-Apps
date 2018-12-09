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
using Xamarin.Auth;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class GoogleServiceViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<GoogleDataModel> Services { get; set; }
        public GoogleServiceViewModel()
        {
            Services = new ObservableCollection<GoogleDataModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Unsubscribe<GooglePage, GoogleDataModel>(this, "AddGoogleAccount");
            MessagingCenter.Subscribe<GooglePage, GoogleDataModel>(this, "AddGoogleAccount", async (obj, item) =>
            {
                var newModel = new GoogleEntity(item);
                await DbProvider.GetConnection().DeleteAllAsync<GoogleEntity>();
                await SaveEntity(newModel, obj);
                if(Services.All(x => newModel.Email != x.Email))
                    Services.Add(new GoogleDataModel(newModel));
                new Command(async () => await ExecuteLoadItemsCommand()).Execute(null);
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
                var googleEntities = await DbProvider.GetConnection().Table<GoogleEntity>().ToListAsync();
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
