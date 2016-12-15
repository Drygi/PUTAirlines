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
    public class ExpandableDataAdapterForClient : BaseExpandableListAdapter
    {
        private Activity _context;
        private MyOrderData _DataList;
        private EditOrder _obj;
        public ExpandableDataAdapterForClient(Activity newContext, MyOrderData newList, EditOrder a)
        {
            _obj = a;
            _DataList = newList;
            _context = newContext;
        }
        public override int GroupCount
        {
            get
            {
                return this._DataList.details.client.Count;
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
                row = _context.LayoutInflater.Inflate(Resource.Layout.DataListClient, null);
            }

            row.FindViewById<EditText>(Resource.Id.c_imie).Text = _DataList.details.client[groupPosition].Imie;
            row.FindViewById<EditText>(Resource.Id.c_nazwisko).Text = _DataList.details.client[groupPosition].Nazwisko;

            return row;
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
                header = _context.LayoutInflater.Inflate(Resource.Layout.ClientGroup, null);
            }
            header.FindViewById<TextView>(Resource.Id.DataHeaderClient).Text = this._DataList.details.client[groupPosition].ToString();

            return header;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}