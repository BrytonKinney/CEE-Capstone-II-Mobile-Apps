using System;
using Xamarin.Auth;

namespace CapstoneApp.Shared.Services.Interfaces
{
    public interface IGoogleAuthenticator
    {
        OAuth2Authenticator GetAuthenticator();
        void Authenticate();
    }
}
