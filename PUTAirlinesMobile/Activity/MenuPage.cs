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
    [Activity(Label = "MenuPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MenuPage : Activity
    {
        Button account, reservation, logout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuPage);
            initControlos();  
        }

        private void initControlos()
        {
            account = FindViewById<Button>(Resource.Id.accountButton);
            account.Click += Account_Click;
            
            reservation = FindViewById<Button>(Resource.Id.reservationButton);
            reservation.Click += Reservation_Click;

            logout = FindViewById<Button>(Resource.Id.logoutButton);
            logout.Click += Logout_Click;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.Clear();
            edit.Apply();
            StartActivity(typeof(LoginPage));
            GlobalMemory.m_client = null;
            this.Finish();

        }

        private void Reservation_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ReserveTickets));
        }

        private void Account_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AccountPage));
        }
    }
}