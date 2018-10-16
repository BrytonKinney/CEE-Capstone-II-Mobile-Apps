using CapstoneApp.Models;
using CapstoneApp.ViewModels;
using LightInject;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RssFeedDetailPage : ContentPage
    {
        RssFeedDetailViewModel viewModel;

        public RssFeedDetailPage(RssFeedDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public RssFeedDetailPage()
        {
            InitializeComponent();

            var item = new RssFeedModel
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new RssFeedDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void saveChangesBtn_Clicked(object sender, EventArgs e)
        {
            new Command(async () => {
                var db = App.Container.GetInstance<IDatabaseProvider>();
                RssFeedModel item = viewModel.Item;
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