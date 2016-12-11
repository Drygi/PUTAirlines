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
using Java.Lang;

namespace PUTAirlinesMobile
{
    public class buttons
    {
        public buttons() { delete = false;edit = false; }
        public bool delete;
        public bool edit;
    }


    public class ExpandableDataAdapter : BaseExpandableListAdapter
    {
        Activity context;
        List<MyOrderData> DataList;
        List<buttons> click_is_declared;
        MyOrder obj;
        public ExpandableDataAdapter(Activity newContext, List<MyOrderData> newList, MyOrder a) : base()
        {
            context = newContext;
            DataList = newList;
            obj = a;
            click_is_declared = reset_list(DataList.Count());
        }


        private List<buttons> reset_list(int cout)
        {
            List<buttons> returned = new List<buttons>();
            for (int i = 0; i < cout; i++) returned.Add(new buttons());
            return returned;        
        }


        public override int GroupCount
        {
            get
            {
                return this.DataList.Count();
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return true;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);
            }
            //  string newId = "", newValue = "";
            //  GetChildViewHelper(groupPosition, childPosition, out newId, out newValue);
            row.FindViewById<TextView>(Resource.Id.t_mwylotu).Text = DataList[groupPosition].MiejsceWylotuToString();
            row.FindViewById<TextView>(Resource.Id.t_mprzylotu).Text = DataList[groupPosition].MiejscePrzylotuToString();
            row.FindViewById<TextView>(Resource.Id.t_drezerwacji).Text = DataList[groupPosition].details.DataRezerwacji;
            row.FindViewById<TextView>(Resource.Id.t_dprzylotu).Text = DataList[groupPosition].details.DataPrzylotu;
            row.FindViewById<TextView>(Resource.Id.t_dodlutu).Text = DataList[groupPosition].DataWylotu;

            string temp = "";
            foreach(var i in DataList[groupPosition].details.client)
            {
                temp += i.ToString() + "\n";
            }

            row.FindViewById<TextView>(Resource.Id.t_o).Text = temp;

            if(!click_is_declared[groupPosition].edit)
            {
                row.FindViewById<Button>(Resource.Id.Edytuj).Click += (o, e) => 
                {
                    obj.setAlert(groupPosition.ToString());
                };

                click_is_declared[groupPosition].edit = true;
            }
            

            if (!click_is_declared[groupPosition].delete)
            {
                row.FindViewById<Button>(Resource.Id.Usun).Click += (o, e) =>
                {
                    obj.remove_operation(groupPosition, DataList[groupPosition].ToShortString());
                };

                click_is_declared[groupPosition].delete = true;
            };

            return row;
            //throw new NotImplementedException ();
        }
        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            View header = convertView;
            if (header == null)
            {
                header = context.LayoutInflater.Inflate(Resource.Layout.ListGroup, null);
            }
            header.FindViewById<TextView>(Resource.Id.DataHeader).Text = DataList[groupPosition].ToString();

            return header;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}