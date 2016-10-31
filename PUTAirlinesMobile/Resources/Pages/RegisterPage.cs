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
using PUTAirlinesMobile.Resources;

namespace PUTAirlinesMobile
{
    [Activity(Label = "RegisterPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class RegisterPage : Activity
    {
        Client client;
        Button register;
        ProgressBar registerBar;
        EditText login, password1, password2, name, lastName, passsportNumber, nationality, city, street, postCode;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterPage);
            controlsInit();
            // Create your application here
        }

        void controlsInit()
        {
            register = FindViewById<Button>(Resource.Id.registerClick);
            register.Click += Register_Click;
           

            registerBar = FindViewById<ProgressBar>(Resource.Id.registerProgressBar);
            registerBar.Visibility = ViewStates.Invisible;

            login = FindViewById<EditText>(Resource.Id.registerLogin);
            password1 = FindViewById<EditText>(Resource.Id.registerPassword);
            password2 = FindViewById<EditText>(Resource.Id.registerPassword2);
            name = FindViewById<EditText>(Resource.Id.registerName);
            lastName = FindViewById<EditText>(Resource.Id.registerSurname);
            passsportNumber = FindViewById<EditText>(Resource.Id.registerPassportNumber);
            nationality = FindViewById<EditText>(Resource.Id.registerNationality);
            city = FindViewById<EditText>(Resource.Id.registerCity);
            street = FindViewById<EditText>(Resource.Id.registerStreet);
            postCode = FindViewById<EditText>(Resource.Id.registerPostCode);


        }


        private void Register_Click(object sender, EventArgs e)
        {
            registerBar.Visibility = ViewStates.Visible;


        }
    }
}