using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content.Res;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Services.Interfaces;
using CapstoneApp.Shared.Views;
using CapstoneApp.Views;
using LightInject;
using Shared.Services.Interfaces;
using Xamarin.Forms;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class EventHandler : IEventHandler
    {
        private IMessagingService _msgService;
        private ISmartMirrorService _smService;
        private IDatabaseProvider _dbService;

        public EventHandler()
        {
            _msgService = App.Container.GetInstance<IMessagingService>();
            _smService = App.Container.GetInstance<ISmartMirrorService>();
            _dbService = App.Container.GetInstance<IDatabaseProvider>();
        }

        private async Task SendOrSaveChanges(ConfigurationEventArgs args)
        {
            if (args == null)
                return;

            if(args.Configuration != null)
                await _msgService.SendConfig(args.Configuration);

            else if (args.Entity != null)
                await _dbService.AddOrUpdateAsync(args.Entity);
        }

        public async void CaptureEvent(object sender, ConfigurationEventArgs args)
        {
            try
            {
                if (_smService.GetInstance() == null)
                {
                    await args.Page.Navigation.PushAsync(new DeviceListPage()).ContinueWith(async (t) =>
                    {
                        if (t.IsCompleted)
                        {
                            MessagingCenter.Subscribe<DeviceListPage, SmartMirror>(this, "MirrorSelected", async (page, mirror) =>
                            {
                                var mirrors = await _dbService.GetConnection().Table<SmartMirror>().ToListAsync();
                                mirrors = mirrors.Where(m => m.HostName != mirror.HostName).ToList();
                                foreach (var m in mirrors)
                                {
                                    m.IsSelected = 0;
                                    await _dbService.AddOrUpdateAsync(m);
                                }
                                mirror.IsSelected = 1;
                                await SendOrSaveChanges(new ConfigurationEventArgs(mirror, page));
                            });
                        }
                    });
                }
                else
                    await SendOrSaveChanges(args);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
