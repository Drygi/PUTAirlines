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
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics;

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
        ScrollView scroll;
        bool goodRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterPage);
            controlsInit();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
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
            scroll = FindViewById<ScrollView>(Resource.Id.scrollViewRegister);
        }

        private void Register_Click(object sender, EventArgs e)
        {
            registerBar.Visibility = ViewStates.Visible;
            goodRegister = true;

            bool loginExist = Helper.MySQLHelper.findLogin(this.login.Text, connection);

            if (loginExist)
            {
                goodRegister = false;
                login.Text = "";
                registerBar.Visibility = ViewStates.Invisible;
                scroll.SmoothScrollTo(0, 0);
                login.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));

            }
            else
                login.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (password1.Text != password2.Text)
            {
                goodRegister = false;
                password1.Text = "";
                password2.Text = "";
                scroll.SmoothScrollTo(0, 0);
                registerBar.Visibility = ViewStates.Invisible;
                password1.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                password2.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
            }
            else
            {
                password1.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
                password2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            }
           if(name.Text.Count()==0)
            {
                name.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                name.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (lastName.Text.Count() == 0)
            {
                lastName.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                lastName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (passsportNumber.Text.Count() == 0)
            {
                passsportNumber.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                passsportNumber.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (nationality.Text.Count() == 0)
            {
                nationality.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                nationality.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (city.Text.Count() == 0)
            {
                city.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                city.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (street.Text.Count() == 0)
            {
                street.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                street.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (postCode.Text.Count() == 0)
            {
                postCode.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                postCode.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));


            if (goodRegister)
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