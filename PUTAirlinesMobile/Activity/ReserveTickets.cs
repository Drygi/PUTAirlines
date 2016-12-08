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
        Spinner startFlySpinner, finishFlySpinner;
        MySqlConnection connection;
        RelativeLayout lay;
        ListView listV;
        TextView dateFly, resTxt;
        Button search;
        ArrayAdapter<Flight> adapter;
        List<Flight> flights;
        List<Airport> airports;
        Airport startAirport = new Airport();
        Airport finishAirport = new Airport();
        // wypisywanie wszystkich lotnisk --> spinner
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReserveTickets);
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            initControls();
        }

        private void initControls()
        {
            dateFly = FindViewById<TextView>(Resource.Id.date);
            dateFly.Click += Date_Click;
            search = FindViewById<Button>(Resource.Id.searchFlys);
            search.Click += Search_Click;
            airports = MySQLHelper.getAirports(connection);
            var adp = new ArrayAdapter(this, Resource.Layout.spinner_layout, airports);
            startFlySpinner = FindViewById<Spinner>(Resource.Id.spinner1);
            startFlySpinner.Adapter = adp;
            startFlySpinner.ItemSelected += StartFlySpinner_ItemSelected;
            finishFlySpinner = FindViewById<Spinner>(Resource.Id.spinner2);
            finishFlySpinner.Adapter = adp;
            finishFlySpinner.ItemSelected += FinishFlySpinner_ItemSelected;
            search.Clickable = false;
            lay = FindViewById<RelativeLayout>(Resource.Id.relLayout);
            lay.Click += Lay_Click;
            listV = FindViewById<ListView>(Resource.Id.listView);
            listV.ItemClick += ListV_ItemClick;
            resTxt = FindViewById<TextView>(Resource.Id.ResText);
        }

        private void FinishFlySpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            finishAirport = airports[Convert.ToInt16(e.Id)];
        }

        private void StartFlySpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            startAirport = airports[Convert.ToInt16(e.Id)];
        }

        private void ListV_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            GlobalMemory.mFlight = flights[Convert.ToInt16(e.Id)];
            StartActivity(typeof(ReserveTickets_2));

        }
        private void Lay_Click(object sender, EventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(lay.WindowToken, 0);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            flights = MySQLHelper.getFlight(timeMemory, startAirport.AirportID, finishAirport.AirportID, connection);
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

                search.Clickable = true;
                search.SetBackgroundColor(Android.Graphics.Color.ParseColor("#003366"));

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