using System;
using System.Collections.Generic;
using Shared.Constants;
using Xamarin.Auth;
using Xamarin.Forms;

namespace CapstoneApp.Shared.Views
{
    public partial class GooglePage : ContentPage
    {
        public GooglePage()
        {
            InitializeComponent();
        }


        async void Google_Btn_Clicked(object sender, EventArgs e)
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

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);


        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {

            
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
                var request = new OAuth2Request("GET", new Uri(UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                }

               
                await DisplayAlert("Email address", e.Account.Username, "OK");
            }
        }
    }
}
