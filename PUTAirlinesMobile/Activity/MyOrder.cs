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

namespace PUTAirlinesMobile
{
    [Activity(Label = "MyOrder")]
    public class MyOrder : Activity
    {
        Helper.Client client;
        ExpandableListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MyOrderLayout);
            client = GlobalMemory.m_client;
            listView = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            listView.SetAdapter(new ExpandableDataAdapter(this, MyOrder.SampleData()));


        }

        private static MyOrderData sampleOrder()
        {
            MyOrderData order_1 = new MyOrderData();
            order_1.NazwaLotniskaOdlotu = "£awica";
            order_1.NazwaLotniskaPrzylotu = "LosDupos";
            order_1.DataWylotu = "12.09.2016 13:45";

            MyOrderDataDetails d_order_1 = new MyOrderDataDetails();
            d_order_1.DataPrzylotu = "12.09.2016 13:45";
            d_order_1.DataRezerwacji = "12.09.2016 13:45";
            d_order_1.KrajOdlotu = "Polska";
            d_order_1.KrajPrzylotu = "Hiszpania";
            d_order_1.MiejscowoscOdlotu = "Poznañ";
            d_order_1.MiejscowoscPrzylotu = "Mardryt";

            List<ClientShort> c_order_1 = new List<ClientShort>();
            c_order_1.Add(new ClientShort("Jakub", "Kwaœny"));
            c_order_1.Add(new ClientShort("Piotr", "Œiorba"));

            d_order_1.client = c_order_1;
            order_1.details = d_order_1;
            return order_1;
        }
        private static MyOrderData sampleOrder_2()
        {
            MyOrderData order_1 = new MyOrderData();
            order_1.NazwaLotniskaOdlotu = "test1";
            order_1.NazwaLotniskaPrzylotu = "test2";
            order_1.DataWylotu = "12.09.2016 13:45";

            MyOrderDataDetails d_order_1 = new MyOrderDataDetails();
            d_order_1.DataPrzylotu = "12.09.2016 13:45";
            d_order_1.DataRezerwacji = "12.09.2016 13:45";
            d_order_1.KrajOdlotu = "Polska";
            d_order_1.KrajPrzylotu = "Hiszpania";
            d_order_1.MiejscowoscOdlotu = "Poznañ";
            d_order_1.MiejscowoscPrzylotu = "Mardryt";

            List<ClientShort> c_order_1 = new List<ClientShort>();
            c_order_1.Add(new ClientShort("Jakub", "test"));
            c_order_1.Add(new ClientShort("Piotr", "ugibugi"));

            d_order_1.client = c_order_1;
            order_1.details = d_order_1;
            return order_1;
        }
        public static List<MyOrderData> SampleData()
        {
            var newDataList = new List<MyOrderData>();
            newDataList.Add(sampleOrder());
            newDataList.Add(sampleOrder_2());

            return newDataList;
        }

       
    }
}