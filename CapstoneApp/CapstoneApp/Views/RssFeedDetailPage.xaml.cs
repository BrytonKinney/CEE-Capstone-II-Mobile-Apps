using CapstoneApp.Models;
using CapstoneApp.ViewModels;
using LightInject;
using Shared.Entities.RssFeed;
using Shared.Services.Interfaces;
using System;
using CapstoneApp.Shared.Entities.RssFeed;
using CapstoneApp.Shared.ViewModels;
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
            MessagingCenter.Send(this, "SaveRssFeed", viewModel.Item);
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteRssFeed", viewModel.Item);
        }
    }
}