using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace UniDel.Droid
{
    [Activity(Label = "UniDel", Theme = "@style/splashscreen", Icon = "@drawable/unidel_logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            // then call base.OnCreate and the Xamarin.Forms methods
            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //base.OnCreate(savedInstanceState);
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState); // Make Bundle null but coords might be wrong
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;


            MobileBarcodeScanner.Initialize(this.Application);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode,
                permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}