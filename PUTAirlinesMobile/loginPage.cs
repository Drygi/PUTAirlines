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
using MySql.Data.MySqlClient;

namespace PUTAirlinesMobile
{
    [Activity(Label = "loginPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen")]
    public class LoginPage : Activity
    {
        Button logButton;
        Button registerButton;
        EditText loginEditText;
        EditText passwordEditText;
        ProgressBar loginBar;
        MySqlConnection connection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginPage);
            init_controls();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
        }

        void init_controls()
        {
            logButton = FindViewById<Button>(Resource.Id.loginButton);
            registerButton = FindViewById<Button>(Resource.Id.registerButton);

            loginBar = FindViewById<ProgressBar>(Resource.Id.loginProgressBar);
            loginBar.Visibility = ViewStates.Invisible;

            loginEditText = FindViewById<EditText>(Resource.Id.loginText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordText);

            logButton.Click += LogButton_Click;
            registerButton.Click += RegisterButton_Click;
        }
        private void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            setAlert("Rejestracja");
        }
        private void LogButton_Click(object sender, EventArgs e)
        {
            loginBar.Visibility = ViewStates.Visible;
            bool result = Helper.MySQLHelper.check_if_account_is_correct(this.loginEditText.Text, this.passwordEditText.Text,connection);

            if(result)
            {
                // uruchomienie Panelu
                // StartActivity(typeof(LoginPage));
                // this.Finish();
                setAlert("Podane dane logowania s¹ poprawne.");
            }else
            {
                setAlert("Podane dane logowania s¹ niepoprawne.");
            }                        

        }
        }
}