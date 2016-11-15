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

namespace PUTAirlinesMobile.Helper
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IndividualNumber { get; set; }
        public string PassportNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
        public string Nationality { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}