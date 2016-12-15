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
    [Activity(Label = "Moje zamówienia", Theme = "@android:style/Theme.NoTitleBar.Fullscreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class EditOrder : Activity
    {
        ExpandableListView listView;
        TextView _countOfCLient;
        TextView _costSingle;
        TextView _costGlobal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditOrder);
            init_title();
            MyOrderData order = GlobalMemory.order[GlobalMemory.actual_edited];
            set_title(order.details.client.Count, 0, 0);
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
    }
}