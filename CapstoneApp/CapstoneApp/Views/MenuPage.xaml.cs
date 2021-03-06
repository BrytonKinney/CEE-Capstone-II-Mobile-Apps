using CapstoneApp.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Devices, Title = "Smart Mirrors" },
                new HomeMenuItem {Id = MenuItemType.RssFeeds, Title="RSS Feeds" },
                new HomeMenuItem {Id = MenuItemType.Weather, Title="Weather Services" },
                new HomeMenuItem {Id = MenuItemType.Google, Title="Google Authentication"},
                new HomeMenuItem {Id = MenuItemType.QuadrantSettings, Title = "Quadrant Settings" },
                new HomeMenuItem {Id = MenuItemType.Email, Title="Email"},
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}