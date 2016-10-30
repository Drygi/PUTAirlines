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
    public class loginPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

           
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.loginPage);
            Button logButton = FindViewById<Button>(Resource.Id.loginButton);
            Button registerButton = FindViewById<Button>(Resource.Id.registerButton);
            ProgressBar loginBar = FindViewById<ProgressBar>(Resource.Id.loginProgressBar);
            loginBar.Visibility = ViewStates.Invisible;

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

         
            string logText = FindViewById<EditText>(Resource.Id.loginText).ToString();
            string passText = FindViewById<EditText>(Resource.Id.passwordText).ToString();

            bool logPass = false;
            ProgressBar loginBar = FindViewById<ProgressBar>(Resource.Id.loginProgressBar);
            loginBar.Visibility = ViewStates.Visible;
            

            string connsqlstring = "Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd";
            MySqlConnection conn = new MySqlConnection(connsqlstring);
            int count = -1;
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();
                string login = logText, password = passText;
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE Login=@log AND Password= @pass;", conn);

                cmd.Parameters.AddWithValue("@log", login);
                cmd.Parameters.AddWithValue("@pass", password);

                count = int.Parse(cmd.ExecuteScalar()+"");

               
                if (count!= -1) logPass = true;
            }
           catch(Exception ex)
            {

                setAlert(ex.Message.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }

            }

            if (logPass == false)
            {
                setAlert("LOGOWANIE NIE POWIODLO SIE");
                loginBar.Visibility = ViewStates.Invisible;
            }
            else
            {
                setAlert("LOGOWANIE POWIODLO SIE");
                loginBar.Visibility = ViewStates.Invisible;
            }

        }
        }
}