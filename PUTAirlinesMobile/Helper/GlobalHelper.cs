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
using System.Security.Cryptography;
using Android.Util;

namespace PUTAirlinesMobile.Helper
{
    public static class GlobalHelper
    {
         
        public static string generateIdentify()
{
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i<stringChars.Length; i++)
            {
            stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            return finalString;
       
}
        public static string getMD5(string password)
        {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.UTF8.GetBytes(password));
                byte[] result = md5.Hash;
                StringBuilder str = new StringBuilder();

                for (int i = 1; i < result.Length; i++)
                {
                    str.Append(result[i].ToString("x2"));
                }

                return str.ToString();
        }


    }
}