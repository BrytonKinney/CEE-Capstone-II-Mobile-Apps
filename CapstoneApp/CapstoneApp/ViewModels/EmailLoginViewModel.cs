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
    public class EmailLoginViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }
        public ObservableCollection<EmailModel> Services { get; set; }
        public EmailLoginViewModel()
        {
            Services = new ObservableCollection<EmailModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<NewEmailPage, EmailModel>(this, "AddNewEmail", async (obj, item) =>
            {
                var dbDriver = App.Container.GetInstance<IDatabaseProvider>();
                var newModel = new Email(item);
                await dbDriver.AddOrUpdateAsync(newModel);
                Services.Add(new EmailModel(newModel));
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
                var dbDriver = App.Container.GetInstance<IDatabaseProvider>();
                var weatherLocations = await dbDriver.GetConnection().Table<Email>().ToListAsync();
                if (weatherLocations.Count > 0)
                {
                    var newModels = weatherLocations.Select(x => new EmailModel(x));
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
