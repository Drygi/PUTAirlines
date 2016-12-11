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

namespace PUTAirlinesMobile
{
    [Activity(Label = "Moje zamówienia", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MyOrder : Activity
    {
        Helper.Client client;
        ExpandableListView listView;
        MySqlConnection connection;
        List<MyOrderData> contener;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyOrderLayout);
            client = GlobalMemory.m_client;
            listView = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            
            connection = Helper.MySQLHelper.getConnection("Server=mysql8.mydevil.net;Port=3306;Database=m1245_paragon;User=m1245_paragon;Password=KsiVnj8HQz32VxT8eNPd");
            contener = GetData();
            listView.SetAdapter(new ExpandableDataAdapter(this,contener,this));
        }

        public List<MyOrderData> GetData()
        {
           return MySQLHelper.get_order_data(client.ID, connection);
        }

        public void setAlert(string message)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alertDialog = alert.Create();
            alertDialog.SetTitle(message);
            alertDialog.Show();
        }

        public void remove_operation(int indext , string name)
        {
            new AlertDialog.Builder(this)
                .SetPositiveButton("Tak", (sender, args) =>
                {
                    try
                    {
                        bool result;
                        result = MySQLHelper.remove_order(this.contener[indext].ReservationID, 
                                                    this.contener[indext].details.client.Count(), connection);
                        if(!result)
                        {
                            setAlert("Usuniecie by³o niemo¿liwe");
                        }else
                        {
                            this.contener.Remove(this.contener[indext]);
                            listView.SetAdapter(new ExpandableDataAdapter(this, contener, this));
                        }

                    }catch(Exception e)
                    {

                    }
                })
                .SetNegativeButton("Nie", (sender, args) =>
                {
                    // nie robimy nic
                })
                .SetMessage("Czy napewno chcesz usun¹æ rejezeracjê lotu ? \n["+name+"]")
                .SetTitle("PotwierdŸ operacje")
                .Show(); 
        }
    }
}