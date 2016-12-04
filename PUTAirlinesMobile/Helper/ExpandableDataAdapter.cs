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
    public class ExpandableDataAdapter : BaseExpandableListAdapter
    {
        Activity context;
        List<MyOrderData> DataList;
        public ExpandableDataAdapter(Activity newContext, List<MyOrderData> newList) : base()
        {
            context = newContext;
            DataList = newList;
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
            row.FindViewById<TextView>(Resource.Id.t_mwylotu).Text = DataList[groupPosition].details.MiejscowoscOdlotu;
            row.FindViewById<TextView>(Resource.Id.t_mprzylotu).Text = DataList[groupPosition].details.MiejscowoscPrzylotu;
            row.FindViewById<TextView>(Resource.Id.t_drezerwacji).Text = DataList[groupPosition].details.DataRezerwacji;
            row.FindViewById<TextView>(Resource.Id.t_dprzylotu).Text = DataList[groupPosition].details.DataPrzylotu;
            row.FindViewById<TextView>(Resource.Id.t_dodlutu).Text = DataList[groupPosition].DataWylotu;

            string temp = "";
            foreach(var i in DataList[groupPosition].details.client)
            {
                temp += i.ToString() + "\n";
            }

            row.FindViewById<TextView>(Resource.Id.t_o).Text = temp;
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