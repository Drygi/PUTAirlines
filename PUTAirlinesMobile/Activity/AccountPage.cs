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
using PUTAirlinesMobile.Helper;
using MySql.Data.MySqlClient;
using Android.Views.InputMethods;

namespace PUTAirlinesMobile
{
    [Activity(Label = "AccountPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AccountPage : Activity
    {
        ScrollView scrollV;
        Button update, edit, passwordChange;
        ProgressBar pBar;
        TextView login;
        EditText name, lastName, passsportNumber, nationality;
        EditText indivdualNumber, city, street, postCode;
        MySqlConnection connection;
        Client client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AccountPage);
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            controlsInit();
        }

        void controlsInit()
        {
            client = GlobalMemory.m_client;
            bool result = Helper.MySQLHelper.check_if_account_is_correct(client.Login, client.Password, connection, out client);
            pBar = FindViewById<ProgressBar>(Resource.Id.ProgressBar);
            pBar.Visibility = ViewStates.Invisible;

          

            name = FindViewById<EditText>(Resource.Id.yourName);
            name.Text = client.Name;
            lastName = FindViewById<EditText>(Resource.Id.yourSurname);
            lastName.Text = client.Surname;
            passsportNumber = FindViewById<EditText>(Resource.Id.yourPassportNumber);
            passsportNumber.Text = client.PassportNumber;
            nationality = FindViewById<EditText>(Resource.Id.yourNationality);
            nationality.Text = client.Nationality;
            city = FindViewById<EditText>(Resource.Id.yourCity);
            city.Text = client.City;
            street = FindViewById<EditText>(Resource.Id.yourStreet);
            street.Text = client.Street;
            postCode = FindViewById<EditText>(Resource.Id.yourPostCode);
            postCode.Text = client.Postcode;
            indivdualNumber = FindViewById<EditText>(Resource.Id.individualNumber);
            indivdualNumber.Text = client.IndividualNumber;
            edit = FindViewById<Button>(Resource.Id.editButton);
            edit.Click += Edit_Click;
            update = FindViewById<Button>(Resource.Id.updateButton);
            update.Click += Update_Click;
            scrollV = FindViewById<ScrollView>(Resource.Id.scroll);
            //scrollV.ScrollChange += ScrollV_ScrollChange;
            update.Clickable = false;
            passwordChange = FindViewById<Button>(Resource.Id.changePasswordButton);
            passwordChange.Click += PasswordChange_Click;
        }

        private void PasswordChange_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(PasswordChange));
        }
        /*
        private void ScrollV_ScrollChange(object sender, View.ScrollChangeEventArgs e)
        {
            //chowanie klawiatury
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(scrollV.WindowToken, 0);
        }
        */
        private void Update_Click(object sender, EventArgs e)
        {
            pBar.Visibility = ViewStates.Invisible;
            update.Clickable = false;
            update.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            edit.Clickable = true; ;
            edit.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            client = getClientFromText();

            if (MySQLHelper.UpdateDataBase(client, connection))
            {
                GlobalMemory.m_client = client;
                setAlert("Zmieniono dane pomyœlnie!");
            }
            else
                setAlert("Coœ posz³o nie tak");

            edit.Clickable = true; ;
            edit.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            update.Clickable = false; ;
            update.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            passwordChange.Clickable = true;
            passwordChange.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            editableEditTexts("#C0C0C0",false);
            scrollV.SmoothScrollTo(0, 0);
            

        }

        private Client getClientFromText()
        {
            Client c = GlobalMemory.m_client;
            c.Name = name.Text.Trim();
            c.Surname = lastName.Text.Trim();
            c.City = city.Text.Trim();
            c.Nationality = nationality.Text.Trim();
            c.PassportNumber = passsportNumber.Text.Trim();
            c.Street = street.Text.Trim();
            c.Postcode = postCode.Text.Trim();        
            return c;
        }

        private void editableEditTexts(string color,bool value)
        {
            name.FocusableInTouchMode = value;
            name.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            lastName.FocusableInTouchMode = value;
            lastName.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            passsportNumber.FocusableInTouchMode = value;
            passsportNumber.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            nationality.FocusableInTouchMode = value;
            nationality.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            city.FocusableInTouchMode = value;
            city.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            street.FocusableInTouchMode = value;
            street.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            postCode.FocusableInTouchMode = value;
            postCode.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            editableEditTexts("#FFFFFF",true);

            edit.Clickable = false;
            edit.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));   
            update.Clickable = true; ;
            update.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            passwordChange.Clickable = false;
            passwordChange.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            scrollV.SmoothScrollTo(0, 0);


        }
        private void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }

        //private void ReLoadd_Click(object sender, EventArgs e)
        //{
        //    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        //    ISharedPreferencesEditor edit = pref.Edit();
        //    edit.Clear();
        //    edit.Apply();

        //    StartActivity(typeof(LoginPage));
        //    this.Finish();
        //}
    }
}