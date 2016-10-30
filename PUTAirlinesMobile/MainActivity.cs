using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace PUTAirlinesMobile
{
    [Activity(Label = "PUTAirlinesMobile", MainLauncher = true, Theme = "@style/Theme.Splash",  Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {


        protected override  void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            Task.Delay(3000).Wait();
            StartActivity(typeof(loginPage));
            this.Finish();
  
          
        }
    }
}

