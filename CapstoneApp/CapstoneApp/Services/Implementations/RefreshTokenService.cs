//using System;
//using System.Timers;
//using System.Threading;
//using System.Threading.Tasks;
//using CapstoneApp.Shared.Services.Interfaces;
//using Xamarin.Auth.OAuth2;
//using Shared.Services.Interfaces;
//using CapstoneApp.Shared.Entities;

//namespace CapstoneApp.Shared.Services.Implementations
//{
//    public class RefreshToken: IRefreshTokenService
//    {
//        IDatabaseProvider _db;

//        public async Task RefreshAccessToken()
//        {
//            SQLite.SQLiteAsyncConnection conn = _db.GetConnection();

//            GoogleAuthHandler handler = new GoogleAuthHandler();

//            //Get most recent

//            if (string.IsNullOrWhiteSpace(refreshToken))
//                return;

//            var queryValues = new Dictionary<string, string>
//            {
//                {"refresh_token", refreshToken},
//                {"client_id", handler.ClientId},
//                {"grant_type", "refresh_token"}
//            };



//            try
//            {
//                var result = await handler.Authenticator.RequestAccessTokenAsync(queryValues);

//                if (result.ContainsKey("access_token"))
//                    _db.AddOrUpdateAsync

//                if (result.ContainsKey("refresh_token"))
//                    account.Properties["refresh_token"] = result["refresh_token"];

//                store.Save(account, ServiceId);

//                statusText.Text = "Refresh succeeded";
//            }
//            catch (Exception ex)
//            {
//                statusText.Text = "Refresh failed " + ex.Message;
//            }

//        }

//        public async void RefreshEventHandler(Object source, System.Timers.ElapsedEventArgs e)
//        {
//            await RefreshAccessToken();
            
//        }

//        public async void StartTimer()
//        {
//            var timer = new System.Timers.Timer();
//            timer.Interval = TimeSpan.FromMinutes(30).TotalMilliseconds;
//            timer.Elapsed += RefreshEventHandler;
//            timer.AutoReset = true;
//            timer.Enabled = true;
//        }
//    }
//}
