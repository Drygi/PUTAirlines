using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace PUTAirlinesMobile
{
    [Activity(Label = "panelPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PanelPage : Activity
    {
        Button reLoadd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.panelPage);
            reLoadd = FindViewById<Button>(Resource.Id.reLoad);
            reLoadd.Click += ReLoadd_Click;


        }
        private void ReLoadd_Click(object sender, EventArgs e)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.Clear();
            edit.Apply();

            StartActivity(typeof(LoginPage));
             this.Finish();

        
        }
    }
}