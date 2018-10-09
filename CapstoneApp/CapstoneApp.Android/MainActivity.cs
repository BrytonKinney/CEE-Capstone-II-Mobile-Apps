
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;

namespace CapstoneApp.Droid
{
    [Activity(Label = "Capstone App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string[] PERMS = { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            if(ApplicationContext.CheckSelfPermission(PERMS[0]) != Permission.Granted)
                RequestPermissions(PERMS, 0);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
	        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}