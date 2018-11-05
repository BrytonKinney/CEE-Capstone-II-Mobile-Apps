using System;
using System.Collections.Generic;
using System.Linq;
using CapstoneApp.Shared.Models;
using Shared.Constants;
using Google.Apis.Services;
using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using CapstoneApp.Shared.ViewModels;

namespace CapstoneApp.Shared.Views
{
    public partial class GooglePage : ContentPage
    {
        GoogleDataModel model = new GoogleDataModel();
        GoogleServiceViewModel vm;

        string RedirectUri = null;
        string ClientId = null;

        public GooglePage()
        {
            InitializeComponent();
            BindingContext = vm = new GoogleServiceViewModel();
        }

        void Google_Btn_Clicked(object sender, EventArgs e)
        {



            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    ClientId = AuthConstants.iOSClientId;
                    RedirectUri = AuthConstants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    ClientId = AuthConstants.AndroidClientId;
                    RedirectUri = AuthConstants.AndroidRedirectUrl;
                    break;
            }


            var authenticator = new OAuth2Authenticator(
                ClientId,
                null,
                AuthConstants.Scope,
                new Uri(AuthConstants.AuthorizeUrl),
                new Uri(RedirectUri),
                new Uri(AuthConstants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);


        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                model.AccessToken = e.Account.Properties["access_token"];
                model.RefreshToken = e.Account.Properties["refresh_token"];
                model.ClientId = ClientId;

                // If the user is authenticated, request their basic user data from Google
                // for finished profile https://people.googleapis.com/v1/people/me?personFields=emailAddresses%2Cphotos&key=
                string UserInfoUrl = "https://www.googleapis.com/gmail/v1/users/me/profile";
                var request = new OAuth2Request("GET", new Uri(UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    dynamic data = JObject.Parse(userJson);
                    model.Email = data.emailAddress;
                    MessagingCenter.Send(this, "AddGoogleAccount", model);
                }
                else{

                }
            }
        }
    }
}
