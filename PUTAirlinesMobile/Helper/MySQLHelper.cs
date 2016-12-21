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
using MySql.Data.MySqlClient;
using PUTAirlinesMobile.Helper;
using System.Data;
using Java.Sql;

namespace PUTAirlinesMobile.Helper
{
    public static class MySQLHelper
    {
        public static MySqlConnection getConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public static bool check_if_account_is_correct(string login, string password, MySqlConnection conn,out Client client)
        {
            bool returned = true;
            Client objClient = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE Login=@log AND Password= @pass;", conn);
                cmd.Parameters.AddWithValue("@log", login);
                cmd.Parameters.AddWithValue("@pass", password);

                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    result.Read();
                    objClient = new Client();
                    objClient.ID = Convert.ToInt16(result[0]);
                    objClient.Name = result[1].ToString();
                    objClient.Surname = result[2].ToString();
                    objClient.IndividualNumber = result[3].ToString();
                    objClient.PassportNumber = result[4].ToString();
                    objClient.City = result[5].ToString();
                    objClient.Street = result[6].ToString();
                    objClient.Postcode = result[7].ToString();
                    objClient.Nationality = result[8].ToString();
                    objClient.Login = result[9].ToString();
                    objClient.Password = result[10].ToString();
                    returned = true;
                }
                 else returned = false;

            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            client = objClient;
            return returned;
        }
        public static bool findLogin(string login, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE Login=@log;", conn);

                cmd.Parameters.AddWithValue("@log", login);

                var result = cmd.ExecuteReader();

                if (result.HasRows)
                {
                    returned = true;
                }
                else
                    returned = false;

            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }
        public static bool findIndividualNumber(string individualNumber, MySqlConnection conn)
        {
            MySqlDataReader rdr = null;
            bool returned = true;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE IndividualNumber=@number;", conn);

                cmd.Parameters.AddWithValue("@number", individualNumber);

                var result = cmd.ExecuteReader();

                if (result.HasRows)
                    returned = true;
                else
                    returned = false;

            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }
        public static bool InsertToDataBase(Client client, MySqlConnection conn)
        {

            bool returned = true;
            try
            {
                conn.Open();
                string insert = "INSERT INTO Client (Name, Surname,IndividualNumber,PassportNumber,City,Street,Postcode,Nationality,Login,Password) ";
                insert += "VALUES (@name,@surName,@individualNumber,@passportNumber,@city,@street,@postcode,@nationality,@log,@pass)";




                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@surName", client.Surname);
                cmd.Parameters.AddWithValue("@individualNumber", client.IndividualNumber);
                cmd.Parameters.AddWithValue("@passportNumber", client.PassportNumber);
                cmd.Parameters.AddWithValue("@city", client.City);
                cmd.Parameters.AddWithValue("@street", client.Street);
                cmd.Parameters.AddWithValue("@postcode", client.Postcode);
                cmd.Parameters.AddWithValue("@nationality", client.Nationality);
                cmd.Parameters.AddWithValue("@log", client.Login);
                cmd.Parameters.AddWithValue("@pass", client.Password);

                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool UpdateDataBase(Client client, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insert = "UPDATE Client SET Name =@name, Surname=@surname,PassportNumber=@passportNumber,City=@city,";
                insert += "Street=@street,Postcode=@postcode,Nationality=@nationality,Password=@pass WHERE ClientID=@id";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@name", client.Name);
                cmd.Parameters.AddWithValue("@surName", client.Surname);
                cmd.Parameters.AddWithValue("@id",client.ID);
                cmd.Parameters.AddWithValue("@passportNumber", client.PassportNumber);
                cmd.Parameters.AddWithValue("@city", client.City);
                cmd.Parameters.AddWithValue("@street", client.Street);
                cmd.Parameters.AddWithValue("@postcode", client.Postcode);
                cmd.Parameters.AddWithValue("@nationality", client.Nationality);
                cmd.Parameters.AddWithValue("@pass", client.Password);

                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }
        public static bool check_saved_account(out string login, out string password, MySqlConnection connection)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("UserName", "");
            string pass = pref.GetString("Password", "");

            if (userName.Trim() == String.Empty || pass.Trim() == String.Empty)
            {
                login = null;
                password = null;
                return false;
            }
            else
            {
                Helper.Client client;
                bool result = Helper.MySQLHelper.check_if_account_is_correct(userName, pass, connection , out client);
                if (result)
                {
                    GlobalMemory.m_client = client;
                    login = userName;
                    password = pass;
                    return true;
                }
                else
                {
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.Clear();
                    edit.Apply();
                    login = null;
                    password = null;
                    return false;
                }
            }
        }

       public static List<Flight> getFlight(DateTime tDate, int startID, int finishID, MySqlConnection conn)
        {
            string stringCMD = "Select A.Name, A.City, AA.Name, AA.City, F.DepartureDate, F.FlightID, F.ConnectionID, F.PriceOfTicket ";
            stringCMD += "FROM Flight F, Airport A, Connection C, Airport AA WHERE C.ConnectionID";
            stringCMD += " = F.ConnectionID AND F.DepartureDate >= @thisDate AND A.AirportId = @stID AND AA.AirportID = @finID";
            stringCMD += " AND A.AirportID = C.DepartureAirportID  AND AA.AirportID = C.ArrivalAirportID";
            List<Flight> flight = new List<Flight>();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(stringCMD, conn);
                cmd.Parameters.AddWithValue("@stID", startID);
                cmd.Parameters.AddWithValue("@finID", finishID);
                cmd.Parameters.AddWithValue("@thisDate", tDate);
                var result = cmd.ExecuteReader();

                
                while (result.Read())
                {
                    flight.Add(new Flight(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetDateTime(4), result.GetInt32(5), result.GetInt32(6),result.GetDouble(7)));
                }

            }
            catch (Exception ex)
            {
                flight = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return flight;
    }

        public static List<Airport> getAirports(MySqlConnection conn)
        {
            List<Airport> airports = new List<Airport>();
            string stringCMD = "SELECT * FROM Airport";

            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(stringCMD, conn);

                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    airports.Add(new Airport(result.GetInt16(0), result.GetString(1), result.GetString(2), result.GetString(3)));
                }

            }
            catch (Exception ex)
            {
                airports = null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return airports;
        }

        public static List<MyOrderData> get_order_data(int ClientID, MySqlConnection conn)
        {
            List<MyOrderData> returned = new List<MyOrderData>();
            string stringCMD = "SELECT reservation.ReservationDate , reservation.JSON, " +
                                        "flight.DepartureDate , flight.ArrivalDate, " +
                                        "airport.Name, airport.City, airport.Country,  " +
                                       "airport2.Name, airport2.City, airport2.Country , reservation.ReservationID , flight.PriceOfTicket " +
                                  /*
                                  0 - data rezerwacji
                                  1 - json osob zapisanych pod bilet
                                  2 - data wylotu
                                  3 - data przylotu
                                  4 - nawa lotniska wylotu
                                  5 - miejscowosc lotniska wylotu
                                  6 - kraj lotniska wylotu
                                  7 - nazwa lotniska przylotu
                                  8 - miejscowosc lotniska przylotu
                                  9 - kraj lotniska przylotu 
                                  */

                               "FROM Reservation reservation , Flight flight, Connection connection , Airport airport, Airport airport2 " +
                               "WHERE reservation.ClientID = @thisClientID " +
                                                                  "AND reservation.FlightID = flight.FlightID " +
                                                                  "AND flight.ConnectionID = connection.ConnectionID " +
                                                                  "AND connection.DepartureAirportID = airport.airportID " +
                                                                  "AND connection.ArrivalAirportID = airport2.airportID ";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(stringCMD, conn);
                cmd.Parameters.AddWithValue("@thisClientID", ClientID);
                var result = cmd.ExecuteReader();

                while (result.Read())
                {
                    MyOrderData order_1 = new MyOrderData();
                    order_1.NazwaLotniskaOdlotu = result.GetString(4);
                    order_1.NazwaLotniskaPrzylotu = result.GetString(7);
                    order_1.DataWylotu = result.GetString(2);
                    order_1.ReservationID = result.GetInt16(10);
                    order_1.CenaBiletu = result.GetFloat(11);
                    MyOrderDataDetails d_order_1 = new MyOrderDataDetails();
                    d_order_1.DataPrzylotu = result.GetString(3);
                    d_order_1.DataRezerwacji = result.GetString(0);
                    d_order_1.KrajOdlotu = result.GetString(6);
                    d_order_1.KrajPrzylotu = result.GetString(9);
                    d_order_1.MiejscowoscOdlotu = result.GetString(5);
                    d_order_1.MiejscowoscPrzylotu = result.GetString(8);


                    List<ClientShort> c_order_1 = GlobalHelper.parsing_JSON(result.GetString(1));

                    d_order_1.client = c_order_1;
                    order_1.details = d_order_1;

                    returned.Add(order_1);
                }

                conn.Close();
                foreach(var i in returned)
                {
                    i.luggages = get_LuggageForReservation(i.ReservationID, conn);
                }

            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static List<Luggage> get_LuggageForReservation(int ReservationID , MySqlConnection conn)
        {
            List<Luggage> retuned = new List<Luggage>();
            string cmdString = "SELECT * FROM Luggage WHERE ReservationID = @thisReservationID";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(cmdString, conn);
            cmd.Parameters.AddWithValue("@thisReservationID", ReservationID);
            var result = cmd.ExecuteReader();

            while (result.Read())
            {
                Luggage luggage = new Luggage();
                luggage.LuggageID = result.GetInt16("LuggageID");
                luggage.ReservationID = result.GetInt16("ReservationID");
                luggage.Lenght = result.GetInt16("Length");
                luggage.Height = result.GetInt16("Height");
                luggage.Width = result.GetInt16("Width");
                luggage.Weight = result.GetInt16("Weight");
                luggage.IsDangerous = result.GetBoolean("isDangerous");
                luggage.UserToken = result.GetString("userToken");

                retuned.Add(luggage);
            }
            conn.Close();
            return retuned;
        }

        public static bool InsertReservation(int clientID, int flightID, string JSON, double allCost,int countOfPeople,MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insertReservation = "INSERT INTO Reservation (ClientID,FlightID,ReservationDate,LastModificationDate,isRealized,JSON,Price,countOfPeople) VALUES (@clientID,@flightID,@actualTime,@actualTime,@zero,@json,@price,@people)";

                MySqlCommand cmd = new MySqlCommand(insertReservation, conn);
                cmd.Parameters.AddWithValue("@clientID", clientID);
                cmd.Parameters.AddWithValue("@flightID", flightID);
                cmd.Parameters.AddWithValue("@actualTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@zero", 1);
                cmd.Parameters.AddWithValue("@price", allCost);
                cmd.Parameters.AddWithValue("@people", countOfPeople);
                cmd.Parameters.AddWithValue("@json", JSON);
                var r = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool updateCountOfClient(int flightID, int countOfClients, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insert = "UPDATE Flight SET CountOfClient = CountOfClient + @clients WHERE FlightID = @Fid ";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@Fid", flightID);
                cmd.Parameters.AddWithValue("@clients", countOfClients);

                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static int getResevationID(int clientID, int flightID, MySqlConnection conn)
        {
            List<int> ids = new List<int>();
            try
            {
                conn.Open();
                string getID = "SELECT ReservationID FROM Reservation WHERE ClientID=@clientID AND FlightID=@flightID ";
                MySqlCommand cmd = new MySqlCommand(getID, conn);

                cmd.Parameters.AddWithValue("@clientID", clientID);
                cmd.Parameters.AddWithValue("@flightID", flightID);

                var result = cmd.ExecuteReader();

              //  result.Read();
                while (result.Read())
                {
                    ids.Add(result.GetInt32(0));
                }
                return ids[ids.Count - 1];


            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static bool UpdateLuggage(Luggage lugagge , MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insertLuggage = "UPDATE Luggage SET  " +
                    "Length=@length, Height=@height, Width=@width, Weight=@weight, isDangerous=@isDanger " +
                    "WHERE LuggageID = @this_LuggageID";

                MySqlCommand cmdLuggage = new MySqlCommand(insertLuggage, conn);

                
                cmdLuggage.Parameters.AddWithValue("@length", lugagge.Lenght);
                cmdLuggage.Parameters.AddWithValue("@height", lugagge.Height);
                cmdLuggage.Parameters.AddWithValue("@width", lugagge.Width);
                cmdLuggage.Parameters.AddWithValue("@weight", lugagge.Weight);
                if (lugagge.IsDangerous)
                    cmdLuggage.Parameters.AddWithValue("@isDanger", 1);
                else
                    cmdLuggage.Parameters.AddWithValue("@isDanger", 0);
                cmdLuggage.Parameters.AddWithValue("@this_LuggageID", lugagge.LuggageID);
                cmdLuggage.ExecuteNonQuery();

            }
            catch (Exception)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool UpdateJSON(string JSON, string ReservationID, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insert = "UPDATE Reservation SET JSON = @thisJSON WHERE ReservationID = @thisReservationID";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@thisJSON", JSON);
                cmd.Parameters.AddWithValue("@thisReservationID", ReservationID);

                var r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool DeleteLuggage(string LugaggeID, string JSON , string ReservationID, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insertLuggage = "DELETE FROM Luggage WHERE LuggageID=@thisLuggageID";
                MySqlCommand cmdLuggage = new MySqlCommand(insertLuggage, conn);
                cmdLuggage.Parameters.AddWithValue("@thisLuggageID", LugaggeID);
                cmdLuggage.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                string updateReservation = "UPDATE Reservation SET  JSON=@thisJSON " +
                    "WHERE ReservationID = @thisReservationID";
                cmdLuggage = new MySqlCommand(updateReservation, conn);
                cmdLuggage.Parameters.AddWithValue("@thisJSON", JSON);
                cmdLuggage.Parameters.AddWithValue("@thisReservationID", ReservationID);
                cmdLuggage.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool DeleteLuggage(string ReservationID, string Token ,MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insertLuggage = "DELETE FROM Luggage WHERE ReservationID=@thisReservationID AND userToken= @thisUserToken";
                MySqlCommand cmdLuggage = new MySqlCommand(insertLuggage, conn);
                cmdLuggage.Parameters.AddWithValue("@thisReservationID", ReservationID);
                cmdLuggage.Parameters.AddWithValue("@thisUserToken", Token);
                cmdLuggage.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }

        public static bool InsertLuggage(Luggage lugagge, int reservationID, MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insertLuggage = "INSERT INTO `Luggage` (`ReservationID`,`Length`, `Height`, `Width`, `Weight`, `isDangerous`, `userToken`) VALUES (@reservationID, @length,@height,@width, @weight, @isDanger,@uToken)";

                MySqlCommand cmdLuggage = new MySqlCommand(insertLuggage, conn);

                cmdLuggage.Parameters.AddWithValue("@reservationID", reservationID);
                cmdLuggage.Parameters.AddWithValue("@length", lugagge.Lenght);
                cmdLuggage.Parameters.AddWithValue("@height", lugagge.Height);
                cmdLuggage.Parameters.AddWithValue("@width", lugagge.Width);
                cmdLuggage.Parameters.AddWithValue("@weight", lugagge.Weight);
                if(lugagge.IsDangerous) 
                    cmdLuggage.Parameters.AddWithValue("@isDanger", 1);
                else
                    cmdLuggage.Parameters.AddWithValue("@isDanger", 0);
                cmdLuggage.Parameters.AddWithValue("@uToken", lugagge.UserToken);

                var r = cmdLuggage.ExecuteNonQuery();

            }
            catch (Exception)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;

        }

        public static bool remove_order(int OrderID, int countOfUser, MySqlConnection conn)
        {
            int FlightID = 0;
            // szukamy loty aby zmnieszyæ CountOfClinet
            string stringCMD = "SELECT FlightID FROM Reservation WHERE ReservationID=@thisOrderID";
                            
             conn.Open();
             MySqlCommand cmd = new MySqlCommand(stringCMD, conn);
             cmd.Parameters.AddWithValue("@thisOrderID", OrderID);
             var result = cmd.ExecuteReader();
             if (!result.HasRows) return false;
             while (result.Read())
             {
                   FlightID = result.GetInt16(0);
             }
            // mamy FlighID - mozemy usuwaæ
             conn.Close();
             conn.Open();
             stringCMD = "DELETE FROM Reservation WHERE ReservationID=@thisOrderID";
             cmd = new MySqlCommand(stringCMD, conn);
             cmd.Parameters.AddWithValue("@thisOrderID", OrderID);
            cmd.ExecuteNonQuery();
            // usuniete
            conn.Close();
            conn.Open();
            stringCMD = "UPDATE Flight SET CountOfClient = CountOfClient - @countOfUser WHERE FlightID = @thisFlightID";
            cmd = new MySqlCommand(stringCMD, conn);
            cmd.Parameters.AddWithValue("@countOfUser", countOfUser);
            cmd.Parameters.AddWithValue("@thisFlightID", FlightID);
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            stringCMD = "DELETE FROM Luggage WHERE ReservationID=@thisOrderID";
            cmd = new MySqlCommand(stringCMD, conn);
            cmd.Parameters.AddWithValue("@thisOrderID", OrderID);
            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        public static bool InsertIntoRes(int clientID,int flightId, string JSON, int countOfPeople, MySqlConnection conn)
        {
            int returned = 1;
            try
            {
                MySqlCommand cmd = new MySqlCommand("InsertIntoReservation", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_ClientID", clientID);
                cmd.Parameters["@_ClientID"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_Flightid", flightId);
                cmd.Parameters["@_Flightid"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_ReservationDate", DateTime.Now);
                cmd.Parameters["@_ReservationDate"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_LastModificationDate", DateTime.Now);
                cmd.Parameters["@_LastModificationDate"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_isRealized", 1);
                cmd.Parameters["@_isRealized"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_JSON", JSON);
                cmd.Parameters["@_JSON"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@_CountOfPeople", countOfPeople);
                cmd.Parameters["@_CountOfPeople"].Direction = ParameterDirection.Input;
                cmd.Connection.Open();
                returned =  cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                returned = 0;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            if (returned == 0)
                return false;
            else
                return true;

        }

        public static bool removeUser(string JSON , string token , int countOfClient, string ReservationID , double price , MySqlConnection conn)
        {
            bool returned = true;
            try
            {
                conn.Open();
                string insert = "UPDATE Reservation SET JSON = @thisJSON, Price = @thisPrice, countOfPeople= @thisCountOfPeople  WHERE ReservationID = @thisReservationID";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@thisJSON", JSON);
                cmd.Parameters.AddWithValue("@thisReservationID", ReservationID);
                cmd.Parameters.AddWithValue("@thisPrice", price*countOfClient);
                cmd.Parameters.AddWithValue("@thisCountOfPeople", countOfClient);
                var r = cmd.ExecuteNonQuery();

                conn.Close();

                if(token!="brak")
                {
                    DeleteLuggage(ReservationID, token, conn);
                }
            }
            catch (Exception ex)
            {
                returned = false;
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return returned;
        }
    }
}




