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
using Java.Util;
using Android.Views.InputMethods;
using PUTAirlinesMobile.Helper;
using MySql.Data.MySqlClient;

namespace PUTAirlinesMobile
{
    [Activity(Label = "ReserveTickets", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ReserveTickets : Activity
    {
        DateTime timeMemory;
        MySqlConnection connection;
        RelativeLayout lay;
        ListView listV;
        EditText startFly, finishFly;
        TextView dateFly, resTxt;
        Button search;
        ArrayAdapter<Flight> adapter;
        List<Flight> flights;
        Flight selectedFlight = new Flight();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReserveTickets);
            initControls();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
        }
        
        private void initControls()
        {            
            dateFly = FindViewById<TextView>(Resource.Id.date);
            dateFly.Click += Date_Click;
            search = FindViewById<Button>(Resource.Id.searchFlys);
            search.Click += Search_Click;
            startFly = FindViewById<EditText>(Resource.Id.startPlace);
            startFly.TextChanged += StartFly_TextChanged;
            finishFly = FindViewById<EditText>(Resource.Id.finishPlace);
            finishFly.TextChanged += FinishFly_TextChanged;
            search.Clickable = false;
            lay = FindViewById<RelativeLayout>(Resource.Id.relLayout);
            lay.Click += Lay_Click;
            listV = FindViewById<ListView>(Resource.Id.listView);
            listV.ItemClick += ListV_ItemClick;
            resTxt = FindViewById<TextView>(Resource.Id.ResText);
         
        }


        private void ListV_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            selectedFlight = flights[Convert.ToInt16(e.Id)];
            setAlert(selectedFlight.ToString());
            //trzeba tu zrobic przejsce do kolejnej strony i przekazac selctedFligh   
        }
        private void Lay_Click(object sender, EventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(lay.WindowToken, 0);
        }

        private void FinishFly_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (startFly.Text.Trim() == String.Empty || finishFly.Text.Trim() == String.Empty || dateFly.Text == "Wybierz datê")
            {
                search.Clickable = false;
                search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            }
            else
            {
                search.Clickable = true;
                search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            }
        }

        private void StartFly_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (startFly.Text.Trim() == String.Empty || finishFly.Text.Trim() == String.Empty || dateFly.Text== "Wybierz datê")
            {
                search.Clickable = false;
                search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#9FA2A8"));
            }
            else
            {
                search.Clickable = true;
                search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            flights = MySQLHelper.getFlight(timeMemory, startFly.Text, finishFly.Text, connection);
            adapter = new ArrayAdapter<Flight>(this, Android.Resource.Layout.SimpleListItem1, flights);
                        
            if (flights == null)
                setAlert("Brak mo¿liwoœci rezerwacji dla podanej daty");
            else
            {
                resTxt.Visibility = ViewStates.Visible;
                listV.Adapter = adapter;
            }
        }

        private void Date_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                timeMemory = time;
                dateFly.SetTextColor((Android.Graphics.Color.ParseColor("#000000")));
                dateFly.Text = timeMemory.ToShortDateString();
                if(startFly.Text.Trim() != String.Empty && finishFly.Text.Trim() != String.Empty )
                {
                    search.Clickable = true;
                    search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));
                }
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);

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