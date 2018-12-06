using System;
using CapstoneApp.Shared.Constants;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Services.Interfaces;
using CapstoneApp.Shared.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class GoogleAuthHandler : IGoogleAuthenticator
    {
        public string ClientId = null;
        public string RedirectUri = null;
        public OAuth2Authenticator Authenticator;
        GooglePage Page; 
        

        public GoogleAuthHandler(GooglePage page){

            Page = page;
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

            Authenticator = new Xamarin.Auth.OAuth2Authenticator(
                ClientId,
                null,
                AuthConstants.Scope,
                new Uri(AuthConstants.AuthorizeUrl),
                new Uri(RedirectUri),
                new Uri(AuthConstants.AccessTokenUrl),
                null,
                true);
        }

        public OAuth2Authenticator GetAuthenticator()
        {
            return Authenticator;
        }

        public void Authenticate()
        {
            Authenticator.Completed += OnAuthCompleted;
            AuthenticationState.Authenticator = Authenticator;
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(Authenticator);
        }

        void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            GoogleDataModel model = new GoogleDataModel();
            if (e.IsAuthenticated)
            {
                model.AccessToken = e.Account.Properties["access_token"];
                model.RefreshToken = e.Account.Properties["refresh_token"];
                MessagingCenter.Send(Page, "AddGoogleAccount", model);
            }
        }
    }
}
