using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using PUTAirlinesMobile.Helper;
using Android.Views.InputMethods;
using Android.Content;

namespace PUTAirlinesMobile
{
    [Activity(Label = "RegisterPage", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class RegisterPage : Activity
    {
        Client client; 
        Button register;
        ProgressBar registerBar;
        EditText login, password1, password2, name, lastName, passsportNumber, nationality, city, street, postCode;
        string individualNumber;
        MySqlConnection connection;
        ScrollView scrollV;
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
            scrollV = FindViewById<ScrollView>(Resource.Id.scrollViewRegister);
            //scrollV.ScrollChange += ScrollV_ScrollChange;
        }

        /*
        private void ScrollV_ScrollChange(object sender, View.ScrollChangeEventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(scrollV.WindowToken, 0);
        }
        */
        private void Register_Click(object sender, EventArgs e)
        {
            registerBar.Visibility = ViewStates.Visible;
            goodRegister = true;

            bool loginExist = Helper.MySQLHelper.findLogin(this.login.Text.Trim(), connection);

            CheckRegister(loginExist);


            if (goodRegister)
            {
                addToDataBase();
            }
        }

        private void CheckRegister(bool findLogin)
        {

            if (findLogin || login.Text.Trim() == String.Empty)
            {
                goodRegister = false;
                login.Text = "";
                registerBar.Visibility = ViewStates.Invisible;
                scrollV.SmoothScrollTo(0, 0);
                login.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));

            }
            else
                login.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (password1.Text.Trim() != password2.Text.Trim() || password1.Text.Trim() == String.Empty)
            {
                goodRegister = false;
                password1.Text = "";
                password2.Text = "";
                scrollV.SmoothScrollTo(0, 0);
                registerBar.Visibility = ViewStates.Invisible;
                password1.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                password2.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
            }
            else
            {
                password1.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
                password2.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));
            }
            if (name.Text.Trim() == String.Empty)
            {
                name.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                name.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (lastName.Text.Trim() == String.Empty)
            {
                lastName.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                lastName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (passsportNumber.Text.Trim() == String.Empty)
            {
                passsportNumber.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                passsportNumber.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (nationality.Text.Trim() == String.Empty)
            {
                nationality.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                nationality.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (city.Text.Trim() == String.Empty)
            {
                city.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                city.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (street.Text.Trim() == String.Empty)
            {
                street.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                street.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

            if (postCode.Text.Trim() == String.Empty)
            {
                postCode.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.editTextBorder));
                goodRegister = false;
                registerBar.Visibility = ViewStates.Invisible;
            }
            else
                postCode.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFFFFF"));

        }

        private void addToDataBase()
        {

            client = new Client();
            client.Name = (name.Text.Trim());
            client.Surname = (lastName.Text.Trim());
            do
            {
              individualNumber = Helper.GlobalHelper.generateIdentify();

            }
            while (Helper.MySQLHelper.findIndividualNumber(individualNumber,connection));

           

            client.IndividualNumber = (individualNumber);
            client.PassportNumber = (passsportNumber.Text.Trim());
            client.City = (city.Text.Trim());
            client.Street = (street.Text.Trim());
            client.Postcode = (postCode.Text.Trim());
            client.Nationality = (nationality.Text.Trim());
            client.Login = (login.Text.Trim());
            client.Password =(Helper.GlobalHelper.getMD5(password1.Text.Trim()));

        var result=  Helper.MySQLHelper.InsertToDataBase(client, connection);
         if(result)
            {
                StartActivity(typeof(LoginPage));
                this.Finish();
             }
            else
            {
                setAlert("cos poszlo nie tak");
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