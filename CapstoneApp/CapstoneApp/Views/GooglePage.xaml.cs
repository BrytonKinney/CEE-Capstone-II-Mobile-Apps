using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CapstoneApp.Shared.Models;
using Xamarin.Auth;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using CapstoneApp.Shared.ViewModels;
using CapstoneApp.Shared.Constants;
using Xamarin.Auth;
using Xamarin.Forms;

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

        async void Google_Btn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string RedirectUri = null;
                string ClientId = null;

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
                authenticator.Error += OnError;

                AuthenticationState.Authenticator = authenticator;

                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(authenticator);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void OnError(object sender, AuthenticatorErrorEventArgs e)
        {
            Debug.WriteLine(e.Message);
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
               // string UserInfoUrl = "https://www.googleapis.com/auth/userinfo";

                    // If the user is authenticated, request their basic user data from Google
                string UserInfoUrl = "https://www.googleapis.com/auth/calendar.readonly";

                var request = new OAuth2Request("GET", new Uri(UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {

                    string userJson = await response.GetResponseTextAsync();
                    JObject data = JObject.Parse(userJson);
                  //  model.Email = data["emailAddress"].ToString();
                    MessagingCenter.Send(this, "AddGoogleAccount", model);
                }
                else
                {

                }
                await DisplayAlert("Email address", e.Account.Username, "OK");
            }
        }
    }
}
