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
using Android.Views.InputMethods;
using PUTAirlinesMobile.Helper;

namespace PUTAirlinesMobile
{
    [Activity(Label = "PasswordChange", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class PasswordChange : Activity
    {
        MySqlConnection connection;
        Button saveButton;
        EditText actualPass, newPass1, newPass2;
        LinearLayout lay;
        Client client;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PasswordChange);
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            initControls();
        }


        private void initControls()
        {
            saveButton = FindViewById<Button>(Resource.Id.saveChange);
            saveButton.Click += SaveButton_Click;
            saveButton.Clickable = false;
            saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            actualPass = FindViewById<EditText>(Resource.Id.actualPassword);
            actualPass.TextChanged += ActualPass_TextChanged;
            newPass1 = FindViewById<EditText>(Resource.Id.newPassword1);
            newPass1.TextChanged += NewPass1_TextChanged;
            newPass2 = FindViewById<EditText>(Resource.Id.newPassword2);
            newPass2.TextChanged += NewPass2_TextChanged;
            lay = FindViewById<LinearLayout>(Resource.Id.layoutID);
            lay.Touch += Lay_Touch;
        }
        private void Lay_Touch(object sender, View.TouchEventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(lay.WindowToken, 0);
        }

        private void NewPass2_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (actualPass.Text.Trim() == String.Empty || newPass1.Text.Trim() == String.Empty || newPass2.Text.Trim() == String.Empty)
            {
                saveButton.Clickable = false;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            }
            else
            {
                saveButton.Clickable = true;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            }
        }

        private void NewPass1_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (actualPass.Text.Trim() == String.Empty || newPass1.Text.Trim() == String.Empty || newPass2.Text.Trim() == String.Empty)
            {
                saveButton.Clickable = false;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            }
            else
            {
                saveButton.Clickable = true;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            }
        }
        private void ActualPass_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (actualPass.Text.Trim() == String.Empty || newPass1.Text.Trim() == String.Empty || newPass2.Text.Trim() == String.Empty)
            {
                saveButton.Clickable = false;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            }
            else
            {
                saveButton.Clickable = true;
                saveButton.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            client = GlobalMemory.m_client;

            if (client.Password == GlobalHelper.getMD5(actualPass.Text.Trim()) && newPass1.Text.Trim() == newPass2.Text.Trim())
            {
                client.Password = GlobalHelper.getMD5(newPass1.Text.Trim());

                if (MySQLHelper.UpdateDataBase(client, connection))
                {
                    setAlert("Nowe has³o ustawiono pomyœlnie");
                    // czyszczenie zapisanego konta
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.Clear();
                    edit.Apply();
                    this.Finish();
                }
                else
                    setAlert("Brak po³¹czenia internetowego");
            }
            else
            {
                actualPass.Text = "";
                newPass1.Text = "";
                newPass2.Text = "";
                actualPass.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                newPass1.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                newPass2.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));

                setAlert("Poda³eœ z³e haslo lub podane nowe has³a nie s¹ identyczne");
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