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
using Java.Sql;

namespace PUTAirlinesMobile
{
   public class Flight
    {
        public Flight(){      }
        public Flight(string departureName, string departureCity, string arrivalName,string arrivalCity, DateTime flighDate)
        {
            this.DepartureName = departureName;
            this.DepartureCity = departureCity;
            this.ArrivalName = arrivalName;
            this.ArrivalCity = arrivalCity;
            this.FlightDate = flighDate;
        }

        public override string ToString()
        {
            return "Wylot o godzinie: " + FlightDate.ToShortTimeString() +"\nDnia: " + FlightDate.ToShortDateString();
        }
        public DateTime FlightDate {get; set;}
        public string DepartureCity { get; set; }
        public string DepartureName { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalName { get; set; }

    }
}