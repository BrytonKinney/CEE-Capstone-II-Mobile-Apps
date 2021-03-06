using CapstoneApp.Models;
using CapstoneApp.Shared.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneApp.Shared.Services.Interfaces;
using LightInject;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.RssFeeds, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Devices:
                        MenuPages.Add(id, new NavigationPage(new DeviceListPage()));
                        break;
                    case (int)MenuItemType.RssFeeds:
                        MenuPages.Add(id, new NavigationPage(new RssFeedsPage()));
                        break;
                    case (int)MenuItemType.Weather:
                        MenuPages.Add(id, new NavigationPage(new WeatherProviderPage()));
                        break;
                    case (int)MenuItemType.Google:
                        MenuPages.Add(id, new NavigationPage(new GooglePage()));
                        break;
                    case (int)MenuItemType.Email:
                        MenuPages.Add(id, new NavigationPage(new GooglePage()));
                        break;
                    case (int)MenuItemType.QuadrantSettings:
                        MenuPages.Add(id, new NavigationPage(new QuadrantSettingsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}