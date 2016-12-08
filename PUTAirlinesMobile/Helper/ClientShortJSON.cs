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
    public class ClientShortJSON
    {
        public ClientShort[] users { get; set; }

        public void addUsers(List<ClientShort> clients)
        {
            users = new ClientShort[clients.Count];

            for (int i = 0; i < clients.Count; i++)
            {
                users[i] = clients[i];
            }
        }
    }
}