using PUTAirlinesMobile.Helper;
using System.Collections.Generic;

namespace PUTAirlinesMobile
{
    public static class GlobalMemory
    {
        public static Client m_client { get; set; }
        public static Flight mFlight { get; set; }
        public static List<MyOrderData> order { get; set; }
        public static int actual_edited { get; set; }
        public static MenuPage _menuPage { get; set; }
    }
}