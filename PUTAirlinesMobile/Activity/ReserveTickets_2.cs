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
using PUTAirlinesMobile.Helper;
using Android.Views.InputMethods;

namespace PUTAirlinesMobile
{
    [Activity(Label = "ReserveTickets_2", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ReserveTickets_2 : Activity
    {
        Spinner vSpinner;
        CheckBox nextToBox;
        MySqlConnection connection;
        Button addLuggage;
        EditText Lenght, Height, Width, Weight, Name, LastName;
        CheckBox isDanger;
        ScrollView scroll_;
        LinearLayout layout_;
        int LuggageValue;
        int counter;
        TextView txt, txtNames;
        List<Luggage> luggages = new List<Luggage>();
        List<ClientShort> clientsShort = new List<ClientShort>();
        int reservationID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReserveTickets_2);
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            initControls();
        }
        private void initControls()
        {
            vSpinner = FindViewById<Spinner>(Resource.Id.valueSpinner);
            var adapter = new ArrayAdapter(this, Resource.Layout.spinner_layout, GetLuggageValue(15));
            vSpinner.Adapter = adapter;
            vSpinner.ItemSelected += VSpinner_ItemSelected;
            nextToBox = FindViewById<CheckBox>(Resource.Id.nextToCheckBox);
            addLuggage = FindViewById<Button>(Resource.Id.addLaggageButton);
            addLuggage.Click += AddLuggage_Click;
            Name = FindViewById<EditText>(Resource.Id.name);
            LastName = FindViewById<EditText>(Resource.Id.surName);
            txtNames = FindViewById<TextView>(Resource.Id.nameLastTxt);

            Lenght = FindViewById<EditText>(Resource.Id.lengthLuggage);
            Height = FindViewById<EditText>(Resource.Id.heightLuggage);
            Width = FindViewById<EditText>(Resource.Id.widthLuggage);
            Weight = FindViewById<EditText>(Resource.Id.weightLuggage);
            isDanger = FindViewById<CheckBox>(Resource.Id.dangerousBox);
            layout_ = FindViewById<LinearLayout>(Resource.Id.leyouutLin);
            layout_.Click += Layout__Click;
            scroll_ = FindViewById<ScrollView>(Resource.Id.scrollViewLay);
            scroll_.ScrollChange += Scroll__ScrollChange;
            txt = FindViewById<TextView>(Resource.Id.laggTxt);
            counter = 0;
        }

        private void Layout__Click(object sender, EventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(scroll_.WindowToken, 0);
        }

        private void Scroll__ScrollChange(object sender, View.ScrollChangeEventArgs e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(layout_.WindowToken, 0);
        }

        private void AddLuggage_Click(object sender, EventArgs e)
        {

            if (Name.Text.Trim()==String.Empty || Name.Text.Trim()==String.Empty || Lenght.Text.Trim() == String.Empty || Height.Text.Trim() == String.Empty || Width.Text.Trim() == String.Empty || Weight.Text.Trim() == String.Empty)
            {
                setAlert("Nie wypelniono wszystkich pól");
            }
            else
            {
                nextToBox.Clickable = false;
                luggages.Add(new Luggage(Convert.ToInt32(Lenght.Text.Trim()), Convert.ToInt32(Height.Text.Trim()), Convert.ToInt32(Width.Text.Trim()), Convert.ToInt32(Weight.Text.Trim()), isDanger.Selected));
                clientsShort.Add(new ClientShort(Name.Text.Trim(), LastName.Text.Trim()));
                
                //wysylanie danych na serwer
                if (counter == LuggageValue)
                {
                    Flight f = GlobalMemory.mFlight;
                    f.CountOfClient += LuggageValue;
                    GlobalMemory.mFlight = f;
                    MySQLHelper.InsertReservation(GlobalMemory.m_client.ID, f.FlightID, GlobalHelper.ToJSON(clientsShort), connection);
                       
                   MySQLHelper.updateCountOfClient(f.FlightID, counter, connection);
                     

                    reservationID = MySQLHelper.getResevationID(GlobalMemory.m_client.ID, f.FlightID, connection);
                 
                    foreach (var item in luggages)
                    {
                        MySQLHelper.InsertLuggage(item, reservationID, connection);
                      
                    }

                    StartActivity(typeof(MenuPage));
                    this.Finish();


                  
                   
                }

                else if (counter == LuggageValue - 1)
                {
                    addLuggage.Text = "Zarezerwuj";
                    counter++;
                }
                else
                {
                    counter++;
                    setAlert("Dodano baga¿ i osobê nr " + counter.ToString() + " Pozosta³o " + (LuggageValue - counter).ToString());
                    txt.Text = "Baga¿ nr " + (counter + 1).ToString();
                    txtNames.Text = "Imie i Nazwisko osoby nr " + (counter+1).ToString();
                    Name.Text = "";
                    LastName.Text = "";
                    Lenght.Text = "";
                    Height.Text = "";
                    Width.Text = "";
                    Weight.Text = "";
                    isDanger.Selected = false;

                }

            }
        }

        private void VSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Id == 0)
                nextToBox.Clickable = false;
            else
                nextToBox.Clickable = true;

            LuggageValue = Convert.ToInt16(e.Id) + 1;
        }

        private void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }
        private List<string> GetLuggageValue(int length)
        {
            List<string> values = new List<string>();

            for (int i = 1; i <= length; i++)
            {
                values.Add(i.ToString());
            }
            return values;
        }
        
       private void insertCountOFClients(int flightID, int countOfClients)
       {
            MySQLHelper.updateCountOfClient(flightID, countOfClients, connection);     
       }
    }
}