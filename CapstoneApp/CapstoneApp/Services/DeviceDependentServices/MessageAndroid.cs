//using CapstoneApp.Shared.Services.Implementations;
//using CapstoneApp.Shared.Services.Interfaces;

//[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
//namespace CapstoneApp.Shared.Services.Implementations
//{
//    public class MessageAndroid : IMessageDisplay
//    {
//            public async void LongAlert(string message)
//            {
//                //Xamarin.Forms..Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
//                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Capstone App", message, "OK");
//            }

//            public async void ShortAlert(string message)
//            {
//                //Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
//                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Capstone App", message, "OK");
//            }
//    }
//}
