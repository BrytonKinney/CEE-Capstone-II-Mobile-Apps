using System;
using System.Collections.Generic;
using System.Linq;
using CapstoneApp.Shared.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using CapstoneApp.Shared.ViewModels;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Services.Implementations;

namespace CapstoneApp.Shared.Views
{
    public partial class GooglePage : ContentPage
    {
        GoogleDataModel model = new GoogleDataModel();
        GoogleServiceViewModel vm;

        public GooglePage()
        {
            InitializeComponent();
            BindingContext = vm = new GoogleServiceViewModel();
        }

        void Google_Btn_Clicked(object sender, EventArgs e)
        {
            GoogleAuthHandler handler = new GoogleAuthHandler(this);
            handler.Authenticate();
        }
    }
}