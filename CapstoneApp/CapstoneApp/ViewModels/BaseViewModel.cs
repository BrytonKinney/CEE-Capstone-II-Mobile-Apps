using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CapstoneApp.Shared.AppEvents;
using CapstoneApp.Shared.Entities;
using CapstoneApp.Shared.Entities.RssFeed;
using CapstoneApp.Shared.Services.Interfaces;
using CapstoneApp.Shared.Views;
using LightInject;
using Shared.Services.Interfaces;
using Xamarin.Forms;

namespace CapstoneApp.Shared.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event EventHandler<ConfigurationEventArgs> SettingsChanged;
        private ISmartMirrorService _smSvc;
        private IEventHandler _handler;
        private IDatabaseProvider _dbProvider;
        private object dbLock = new object();
        public IDatabaseProvider DbProvider
        {
            get
            {
                lock (dbLock)
                {
                    return _dbProvider ?? (_dbProvider = App.Container.GetInstance<IDatabaseProvider>());
                }
            }
            set
            {
                lock (dbLock)
                {
                    _dbProvider = value;
                }
            }
        }
        public BaseViewModel()
        {
            _handler = App.Container.GetInstance<IEventHandler>();
            DbProvider = App.Container.GetInstance<IDatabaseProvider>();
            _smSvc = App.Container.GetInstance<ISmartMirrorService>();
            SettingsChanged = _handler.CaptureEvent;
        }
        
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        protected virtual async Task SaveEntity(BaseEntity entity, ContentPage sendingPage)
        {
            if (_smSvc.GetInstance() == null)
            {
                await sendingPage.Navigation.PushModalAsync(new NavigationPage(new DeviceListPage()));
            }
            else
            {
                await DbProvider.AddOrUpdateAsync(entity).ConfigureAwait(false);
                OnSettingsChanged(sendingPage);
            }
        }
        protected virtual async void OnSettingsChanged(ContentPage sendingPage)
        {
            try
            {
                if (SettingsChanged != null)
                {
                    var mirror = await DbProvider.GetConnection().Table<SmartMirror>().FirstOrDefaultAsync();
                    var feeds = DbProvider.GetConnection().Table<RssFeed>().Where(rss => rss.Enabled == 1);
                    var weatherLocations = DbProvider.GetConnection().Table<WeatherLocations>().Where(weather => weather.Enabled == 1);
                    var quadrants = await DbProvider.GetConnection().Table<QuadrantSettings>().ToListAsync();

                    if (quadrants.Count == 0)
                        quadrants = CapstoneApp.Shared.Constants.DefaultQuadrantSettings.Defaults.ToList();

                    MirrorConfig config = new MirrorConfig
                    {
                        Mirror = mirror,
                        RssFeeds = await feeds.ToListAsync(),
                        WeatherLocations = await weatherLocations.ToListAsync(),
                        Configuration = quadrants
                    };
                    var settingsChanged = SettingsChanged;
                    settingsChanged.Invoke(this, new ConfigurationEventArgs(config, sendingPage));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
