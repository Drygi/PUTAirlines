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
using Newtonsoft.Json;

namespace PUTAirlinesMobile
{
    [Activity(Label = "Moje zamówienia", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class EditOrder : Activity
    {
        ExpandableListView listView;
        TextView _countOfCLient;
        TextView _costSingle;
        TextView _costGlobal;
        public MySqlConnection connection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditOrder);
            init_title();
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd"); 
            MyOrderData order = GlobalMemory.order[GlobalMemory.actual_edited];
            set_title(order.details.client.Count, order.CenaBiletu, order.CenaBiletu*order.details.client.Count());
            listView = FindViewById<ExpandableListView>(Resource.Id.listviewClientOfReservation);
            listView.SetAdapter(new ExpandableDataAdapterForClient(this, order,this));
        }

        public void init_title()
        {
            _countOfCLient = FindViewById<TextView>(Resource.Id.liczba_miejsc);
             _costSingle = FindViewById<TextView>(Resource.Id.kosztOsoba);
            _costGlobal = FindViewById<TextView>(Resource.Id.kosztCalkowity);
        }
        public void set_title(int count , double cost , double cost_global)
        {
            _countOfCLient.Text = "Liczba zarezerwowanych miejsc : " + count.ToString();
            _costSingle.Text = "Koszt lotu dla jednej osoby : " + cost + " z³";
            _costGlobal.Text = "Koszt ca³kowity : " + cost_global + " z³";
        }
        public void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }
        public void remove_operation(int indeks, MyOrderData order)
        {
            new AlertDialog.Builder(this)
                .SetPositiveButton("Tak", (sender, args) =>
                {
                    try
                    {
                        string token = order.details.client[indeks].UserToken;
                        order.details.client.RemoveAt(indeks);
                        ClientShortJSON jsonObj = new ClientShortJSON()
                        {
                            users = order.details.client.ToArray()
                        };
                        MySQLHelper.removeUser
                        (
                            JsonConvert.SerializeObject(jsonObj), 
                            token, order.details.client.Count, 
                            order.ReservationID.ToString(), 
                            order.CenaBiletu, 
                            connection
                            );

                    }
                    catch (Exception e)
                    {

                    }
                })
                .SetNegativeButton("Nie", (sender, args) =>
                {
                    
                })
                .SetMessage("Czy napewno chcesz usun¹æ osobê ? \n[" +  order.details.client[indeks].ToStringWithoutToken()+ "]")
                .SetTitle("PotwierdŸ operacje")
                .Show();
        }
    }
}