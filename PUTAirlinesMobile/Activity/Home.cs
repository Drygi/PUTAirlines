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
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PUTAirlinesMobile
{
    [Activity(Label = "PUTAirlines",MainLauncher =true, Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Home : Activity
    {
        MySqlConnection connection;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
            await Task.Delay(3000);

            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            string login, haslo;
            if (Helper.MySQLHelper.check_saved_account(out login, out haslo, connection))
            {
                StartActivity(typeof(AccountPage));
                this.Finish();
            }
            else
            {
                this.StartActivity(typeof(LoginPage));
                this.Finish();
            }


        }
    }
}