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
        ImageButton account, reservation, logout,myOrder;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuLayout);
            initControlos();  
        }

        private void initControlos()
        {
            account = FindViewById<ImageButton>(Resource.Id.ButtonMojeKonto);
            account.Click += Account_Click;
            
            reservation = FindViewById<ImageButton>(Resource.Id.ButtonZarezerwuj);
            reservation.Click += Reservation_Click;

            logout = FindViewById<ImageButton>(Resource.Id.WyjdzZAplikacji);
            logout.Click += Logout_Click;

            myOrder= FindViewById<ImageButton>(Resource.Id.ButtonMojeRezerwacje);
            myOrder.Click += MyOrder_Click;
        }

        private void MyOrder_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MyOrder));
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.Clear();
            edit.Apply();
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