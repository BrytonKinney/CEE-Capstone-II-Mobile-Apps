using CapstoneApp.Models;
using CapstoneApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RssFeedsPage : ContentPage
    {
        RssFeedViewModel viewModel;

        public RssFeedsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RssFeedViewModel(Shared.Constants.CommandConstants.VIEWS.RssFeeds);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as RssFeedModel;
            if (item == null)
                return;

            await Navigation.PushAsync(new RssFeedDetailPage(new RssFeedDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}