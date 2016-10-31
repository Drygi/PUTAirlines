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
using MySql.Data.MySqlClient;

namespace PUTAirlinesMobile
{
    [Activity(Label = "RegisterPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class RegisterPage : Activity
    {
        Client client;
        Button register;
        ProgressBar registerBar;
        EditText login, password1, password2, name, lastName, passsportNumber, nationality, city, street, postCode;
        MySqlConnection connection;
        bool goodRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterPage);
            controlsInit();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            goodRegister = true;
        }

        void controlsInit()
        {
            register = FindViewById<Button>(Resource.Id.registerClick);
            register.Click += Register_Click;
           

            registerBar = FindViewById<ProgressBar>(Resource.Id.registerProgressBar);
            registerBar.Visibility = ViewStates.Invisible;


        }

        void variabelsFromAXAML()
        {
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

            variabelsFromAXAML();
            bool loginExist = Helper.MySQLHelper.findLogin(this.login.Text, connection);

            if (loginExist)
            {
                goodRegister = false;
                FindViewById<EditText>(Resource.Id.registerLogin).Text = "";
                FindViewById<EditText>(Resource.Id.registerLogin).Hint = "Podany login ju¿ istnieje!";
                FindViewById<EditText>(Resource.Id.registerLogin).SetHintTextColor(Android.Graphics.Color.Red);
                registerBar.Visibility = ViewStates.Invisible;
                FindViewById<ScrollView>(Resource.Id.scrollViewRegister).SmoothScrollTo(0, 0);
            }

            if (password1.Text != password2.Text)
            {
                goodRegister = false;
                FindViewById<EditText>(Resource.Id.registerPassword).Text = "";
                FindViewById<EditText>(Resource.Id.registerPassword2).Text = "";
                FindViewById<EditText>(Resource.Id.registerPassword).Hint = "Podane has³a musz¹ byæ identyczne";
                FindViewById<EditText>(Resource.Id.registerPassword).SetHintTextColor(Android.Graphics.Color.Red);
                FindViewById<EditText>(Resource.Id.registerPassword2).Hint = "Podane has³a musz¹ byæ identyczne";
                FindViewById<EditText>(Resource.Id.registerPassword2).SetHintTextColor(Android.Graphics.Color.Red);
                FindViewById<ScrollView>(Resource.Id.scrollViewRegister).SmoothScrollTo(0, 0);
                registerBar.Visibility = ViewStates.Invisible;
            }

            if(goodRegister)
            {
                setAlert("Zarejestrowano pomyœlnie");
            }
        }
        private void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }
    }
}