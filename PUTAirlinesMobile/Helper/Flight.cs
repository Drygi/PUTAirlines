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
        public Flight() { }
        public Flight(string departureName, string departureCity, string arrivalName, string arrivalCity, DateTime flighDate, int flightID, int connectionID,double price)
        {
            this.DepartureName = departureName;
            this.DepartureCity = departureCity;
            this.ArrivalName = arrivalName;
            this.ArrivalCity = arrivalCity;
            this.FlightDate = flighDate;
            this.ConnectionID = connectionID;
            this.FlightID = flightID;
            this.Price = price;
        }
        public Flight(string departureName, string departureCity, string arrivalName, string arrivalCity, DateTime flighDate, int flightID, int connectionID, int countOfClient,double price)
        {

            this.DepartureName = departureName;
            this.DepartureCity = departureCity;
            this.ArrivalName = arrivalName;
            this.ArrivalCity = arrivalCity;
            this.FlightDate = flighDate;
            this.CountOfClient = countOfClient;
            this.ConnectionID = connectionID;
            this.FlightID = flightID;
            this.Price = price;
        }

        public override string ToString()
        {

            return "Cena biletu: "+ String.Format("{0:N2}",Price.ToString())+" z�"+ "\nWylot o godzinie: " + FlightDate.ToShortTimeString() + "\nDnia: " + FlightDate.ToShortDateString();
        }
        public double Price { get; set; }
        public int FlightID { get; set; }
        public DateTime FlightDate { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureName { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalName { get; set; }
        public int ConnectionID { get; set; }
        public int CountOfClient { get; set; }

    }
}