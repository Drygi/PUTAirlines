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
        private int ID = 0;
        private string name = "";
        private string surName = "";
        private string individualNumber = "";
        private string passportNumber = "";
        private string city = "";
        private string street = "";
        private string postcode = "";
        private string nationality = "";
        private string login = "";
        private string password = "";

        public int getID()
        {
            return this.ID;
        }
        public void setID(int id)
        {
           this.ID = id;
        }

        public string getName()
        {
            return this.name;
        }
        public void setName(string Name)
        {
            this.name = Name;
        }

        public string getLastName()
        {
            return this.surName;
        }
        public void setLastName(string lastName)
        {
            this.surName = lastName;
        }

        public string getIndividualNumber()
        {
            return this.individualNumber;
        }
        public void setIndividualNumber(string IndividualNumber)
        {
            this.individualNumber = IndividualNumber;
        }
        public string getPassportNumber()
        {
            return this.passportNumber;
        }
        public void setPassportNumber(string PassportNumber)
        {
            this.passportNumber = PassportNumber;
        }

        public string getCity()
        {
            return this.city;
        }
        public void setCity(string City)
        {
            this.city = City;
        }

        public string getStreet()
        {
            return this.street;
        }
        public void setStreet(string Street)
        {
            this.street = Street;
        }

        public string getPostcode()
        {
            return this.postcode;
        }
        public void setPostCode(string PostCode)
        {
            this.postcode = PostCode;
        }

        public string getNationality()
        {
            return this.nationality;
        }
        public void setNationality(string Nationality)
        {
            this.nationality = Nationality;
        }

        public string getLogin()
        {
            return this.login;
        }
        public void setLogin(string Login)
        {
            this.login = Login;
        }

        public string getPassword()
        {
            return this.password;
        }
        public void setPassword(string Password)
        {
            this.password = Password;
        }
    }
}