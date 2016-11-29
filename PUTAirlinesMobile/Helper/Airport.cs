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
    public class Airport
    {

        public Airport() { }
        public Airport(int airportID, string airportName, string city, string country )
        {
            this.AirportID = airportID;
            this.City = city;
            this.Country = country;
            this.AirportName = airportName;
        }

        public int AirportID { get; set; }
        public string City { get; set; }
        public string  Country { get; set; }
        public string AirportName { get; set; }

        public override string ToString()
        {
            return City+" " + AirportName;
        }
    }
}