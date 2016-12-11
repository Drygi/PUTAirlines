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

    public class ClientShort
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public ClientShort(string imie , string nawisko)
        {
            this.Imie = imie;
            this.Nazwisko = nawisko;
        }
        public override string ToString()
        {
            return Imie + " " + Nazwisko;
        }
    }

    public class MyOrderDataDetails
    {
        public string DataPrzylotu { get; set; }
        public string DataRezerwacji { get; set; }
        public string MiejscowoscOdlotu { get; set; }
        public string KrajOdlotu { get; set; }
        public string MiejscowoscPrzylotu { get; set; }
        public string KrajPrzylotu { get; set; }
        public List<ClientShort> client { get; set; }
    }
    public class MyOrderData
    {
        public string NazwaLotniskaOdlotu { get; set; }
        public string NazwaLotniskaPrzylotu { get; set; }
        public string DataWylotu { get; set; }
        public MyOrderDataDetails details { get; set; }
        public int ReservationID { get; set; }
        public override string ToString()
        {
            return NazwaLotniskaOdlotu + "-" + NazwaLotniskaPrzylotu + " [" + DataWylotu + "]";
        }

        public string ToShortString()
        {
            return NazwaLotniskaOdlotu + "-" + NazwaLotniskaPrzylotu;
        }

        public string MiejsceWylotuToString()
        {
            return "\"" + NazwaLotniskaOdlotu + "\" " + details.MiejscowoscOdlotu + " (" + details.KrajOdlotu+")";
        }

        public string MiejscePrzylotuToString()
        {
            return "\"" + NazwaLotniskaPrzylotu + "\" " + details.MiejscowoscPrzylotu + " (" + details.KrajPrzylotu+")";
        }
    }
}