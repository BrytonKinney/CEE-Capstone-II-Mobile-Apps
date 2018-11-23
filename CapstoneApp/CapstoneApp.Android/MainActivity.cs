
using System.Linq;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace CapstoneApp.Droid
{
    [Activity(Label = "Capstone App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string[] PERMS =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.Bluetooth,
            Manifest.Permission.ChangeWifiMulticastState
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
            foreach (var perm in PERMS.Select((item, index) => new { Index = index, Permission = item }))
            {
                if(ApplicationContext.CheckSelfPermission(perm.Permission) != Permission.Granted)
                    RequestPermissions(PERMS, perm.Index);
            }
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}