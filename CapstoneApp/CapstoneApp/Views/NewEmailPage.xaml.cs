using System;
using System.Collections.Generic;
using CapstoneApp.Models;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.ViewModels;

using Xamarin.Forms;

namespace CapstoneApp.Shared.Views
{
    public partial class NewEmailPage : ContentPage
    {

        public EmailModel Item = new EmailModel();
        public EmailViewModel viewModel;
        public NewEmailPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EmailViewModel();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddEmail", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
