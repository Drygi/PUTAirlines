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
using Newtonsoft.Json;

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

        private void Set_unchecked(View row)
        {
            row.FindViewById<CheckBox>(Resource.Id.isBagaz).Checked = false;
            row.FindViewById<EditText>(Resource.Id.b_dlugosc).Enabled = false;
            row.FindViewById<EditText>(Resource.Id.b_wysokosc).Enabled = false;
            row.FindViewById<EditText>(Resource.Id.b_szerokosc).Enabled = false;
            row.FindViewById<EditText>(Resource.Id.b_waga).Enabled = false;
            row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Enabled = false;
        }

        private void Set_checked(View row)
        { 

            row.FindViewById<CheckBox>(Resource.Id.isBagaz).Checked = true;
            row.FindViewById<EditText>(Resource.Id.b_dlugosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_wysokosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_szerokosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_waga).Enabled = true;
            row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Enabled = true;
        }

        private void Set_luggage(View row , Helper.Luggage luggage)
        {
            row.FindViewById<CheckBox>(Resource.Id.isBagaz).Checked = true;

            row.FindViewById<EditText>(Resource.Id.b_dlugosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text = luggage.Lenght.ToString();
            row.FindViewById<EditText>(Resource.Id.b_wysokosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_wysokosc).Text = luggage.Height.ToString();
            row.FindViewById<EditText>(Resource.Id.b_szerokosc).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_szerokosc).Text = luggage.Width.ToString();
            row.FindViewById<EditText>(Resource.Id.b_waga).Enabled = true;
            row.FindViewById<EditText>(Resource.Id.b_waga).Text = luggage.Weight.ToString();
            row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Enabled = true;
            row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Checked = luggage.IsDangerous;
        }


        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = _context.LayoutInflater.Inflate(Resource.Layout.DataListClient, null);
            }

            row.FindViewById<EditText>(Resource.Id.c_imie).Text = _DataList.details.client[groupPosition].Imie;
            row.FindViewById<EditText>(Resource.Id.c_imie).Enabled = false;
            row.FindViewById<EditText>(Resource.Id.c_nazwisko).Text = _DataList.details.client[groupPosition].Nazwisko;
            row.FindViewById<EditText>(Resource.Id.c_nazwisko).Enabled = false;


            CheckBox _isBagaz = row.FindViewById<CheckBox>(Resource.Id.isBagaz);
            _isBagaz.Click += (o, e) => {
                if (_isBagaz.Checked)
                    Set_checked(row);
                else
                    Set_unchecked(row);
            };
            if (_DataList.details.client[groupPosition].UserToken != "brak")
            {
                var result = _DataList.luggages.First(s => s.UserToken == _DataList.details.client[groupPosition].UserToken);
                Set_luggage(row, result);

            } else
            {
                Set_unchecked(row);
            }

            row.FindViewById<Button>(Resource.Id.Edytuj).Click += (o, e) => {
                Valid_and_update(row, groupPosition);
            };

            return row;
        }

        private void Valid_and_update(View row , int groudPosition)
        {
            var result = _DataList.luggages.Find(s => s.UserToken == _DataList.details.client[groudPosition].UserToken);
            if(result!=null)
            {
                if (Valid_luggage_field(row))
                {
                    if (!Valid_luggage_field_if_change(row, result))
                    {
                        _obj.setAlert("Brak zmian. ");
                    }else
                    {
                        Helper.MySQLHelper.UpdateLuggage(Get_New_luggage(row, result), _obj.connection);
                        _obj.setAlert("Zmieniono parametry baga¿u pomyœlnie. ");
                    }
                }
                else
                {
                    _DataList.details.client[groudPosition].UserToken = "brak";
                    string newJSON = JsonConvert.SerializeObject(new ClientShortJSON() { users = _DataList.details.client.ToArray() });
                    Helper.MySQLHelper.DeleteLuggage(result.LuggageID.ToString(), newJSON, _DataList.ReservationID.ToString(), _obj.connection);
                    _obj.setAlert("Baga¿ zosta³ usuniêty dla klienta " + _DataList.details.client[groudPosition].ToStringWithoutToken() + ".");

                }
            }
            else
            {
                if(Valid_luggage_field(row))
                {
                    string Token = Helper.GlobalHelper.generateIdentify();
                    _DataList.details.client[groudPosition].UserToken = Token;
                    string newJSON = JsonConvert.SerializeObject(new ClientShortJSON() { users = _DataList.details.client.ToArray() });
                    Helper.Luggage newLuggage = Create_Luggage(row, Token);
                    Helper.MySQLHelper.InsertLuggage(newLuggage, newLuggage.ReservationID, _obj.connection);
                    Helper.MySQLHelper.UpdateJSON(newJSON, _DataList.ReservationID.ToString(), _obj.connection);
                    _obj.setAlert("Baga¿ zosta³ dodany dla klienta " + _DataList.details.client[groudPosition].ToStringWithoutToken() + ".");

                }
                else
                {
                    _obj.setAlert("Brak zmian. ");
                }
            }
        }

        private bool Valid_luggage_field(View row)
        {
            if (row.FindViewById<CheckBox>(Resource.Id.isBagaz).Checked)
            {
                if (Valid_luggage_field_if_empty(row))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private bool Valid_luggage_field_if_empty(View row)
        {
            if (row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text == "") return false;
            if (row.FindViewById<EditText>(Resource.Id.b_wysokosc).Text == "") return false;
            if (row.FindViewById<EditText>(Resource.Id.b_szerokosc).Text == "") return false;
            if (row.FindViewById<EditText>(Resource.Id.b_waga).Text == "") return false;

            return true;
        }

        private bool Valid_luggage_field_if_change(View row , Helper.Luggage luggage)
        {
            string temp = row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text;
            if (row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text != luggage.Lenght.ToString()) return true;
            if (row.FindViewById<EditText>(Resource.Id.b_wysokosc).Text != luggage.Height.ToString()) return true;
            if (row.FindViewById<EditText>(Resource.Id.b_szerokosc).Text != luggage.Width.ToString()) return true;
            if (row.FindViewById<EditText>(Resource.Id.b_waga).Text != luggage.Weight.ToString()) return true;
            if (row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Checked != luggage.IsDangerous) return true;

            return false;
        }

        private Helper.Luggage Get_New_luggage(View row , Helper.Luggage old)
        {
            return new Helper.Luggage()
            {
                LuggageID = old.LuggageID,
                Lenght = int.Parse(row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text),
                Height = int.Parse(row.FindViewById<EditText>(Resource.Id.b_wysokosc).Text),
                Width = int.Parse(row.FindViewById<EditText>(Resource.Id.b_szerokosc).Text),
                Weight = int.Parse(row.FindViewById<EditText>(Resource.Id.b_waga).Text),
                IsDangerous = row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Checked
            };
        }

        private Helper.Luggage Create_Luggage(View row , string Token)
        {
            return new Helper.Luggage()
            {
                ReservationID = _DataList.ReservationID,
                Lenght = int.Parse(row.FindViewById<EditText>(Resource.Id.b_dlugosc).Text),
                Height = int.Parse(row.FindViewById<EditText>(Resource.Id.b_wysokosc).Text),
                Width = int.Parse(row.FindViewById<EditText>(Resource.Id.b_szerokosc).Text),
                Weight = int.Parse(row.FindViewById<EditText>(Resource.Id.b_waga).Text),
                IsDangerous = row.FindViewById<CheckBox>(Resource.Id.is_dangerous).Checked,
                UserToken = Token        
            };
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
            header.FindViewById<TextView>(Resource.Id.DataHeaderClient).Text = this._DataList.details.client[groupPosition].ToStringWithoutToken();

            return header;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}