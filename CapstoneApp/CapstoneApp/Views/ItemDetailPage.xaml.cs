using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CapstoneApp.Models;
using CapstoneApp.ViewModels;
using Shared.Services.Interfaces;
using LightInject;
using Shared.Entities.RssFeed;

namespace CapstoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void saveChangesBtn_Clicked(object sender, EventArgs e)
        {
            new Command(async () => {
                var db = App.Container.GetInstance<IDatabaseProvider>();
                Item item = viewModel.Item;
                int id = Convert.ToInt32(item.Id);
                var rssFeed = await db.GetConnection().Table<RssFeed>().Where(r => r.Id == id).FirstOrDefaultAsync();
                if(rssFeed != null)
                {
                    rssFeed.Enabled = item.Enabled ? 1 : 0;
                }
                await db.AddOrUpdateAsync(rssFeed);
                Device.BeginInvokeOnMainThread(async () => 
                {
                    await Application.Current.MainPage.DisplayAlert("Saved changes", "RSS Feed Settings saved.", "OK");
                    await Navigation.PopAsync();
                });
            }).Execute(null);
        }
    }
}