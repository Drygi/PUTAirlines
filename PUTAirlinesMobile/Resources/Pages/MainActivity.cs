using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PUTAirlinesMobile.Resources.Pages;

namespace PUTAirlinesMobile
{
    [Activity(Label = "PUTAirlinesMobile", MainLauncher = true,ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@style/Theme.Splash",  Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MySqlConnection connection;
        string userName;
        string password;

        protected override  void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);

            Task.Delay(3000).Wait();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            sharedPreferences();
        }


        void sharedPreferences()
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("UserName", "");
            password = pref.GetString("Password", "");

            if (userName.Trim() == String.Empty || password.Trim() == String.Empty)
            {
                StartActivity(typeof(LoginPage));
                this.Finish();
            }
            else
            {
                bool result = Helper.MySQLHelper.check_if_account_is_correct(userName, password, connection);
                if (result)
                {
                    StartActivity(typeof(panelPage));
                    this.Finish();
                }
                else
                {
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.Clear();
                    edit.Apply();
                    this.StartActivity(typeof(LoginPage));
                }
            }

        }
    }
}

