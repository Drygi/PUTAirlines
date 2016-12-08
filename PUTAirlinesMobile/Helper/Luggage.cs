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
    public class Luggage
    {
        public Luggage() { }

        public Luggage(int length, int height, int width, int weight, bool isDangerous)
        {
            // this.LuggageID = luggageID;
            this.Lenght = length;
            this.Height = height;
            this.Width = width;
            this.Weight = weight;
            this.IsDangerous = isDangerous;
        }

        public int LuggageID { get; set; }
        public int Lenght { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public bool IsDangerous { get; set; }
    }
}