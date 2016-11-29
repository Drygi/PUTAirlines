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
            //string stringCMD = "SELECT A.Name, A.City, AA.Name, AA.City, F.DepartureDate";
            //stringCMD += " FROM Flight F, Airport A, Connection C, Airport AA ";
            //stringCMD += "WHERE C.ConnectionID = F.ConnectionID AND F.DepartureDate >= @thisDate AND ";
            //stringCMD += "A.City=@StCity AND A.AirportID = C.DepartureAirportID  AND AA.AirportID = C.ArrivalAirportID ";
            //stringCMD += "AND AA.City=@FiCity";
            string stringCMD = "Select A.Name, A.City, AA.Name, AA.City, F.DepartureDate ";
            stringCMD += "FROM Flight F, Airport A, Connection C, Airport AA WHERE C.ConnectionID";
            stringCMD += " = F.ConnectionID AND F.DepartureDate >= @thisDate AND A.AirportId = @stID AND AA.AirportID = @finID";

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
                    flight.Add(new Flight(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetDateTime(4)));                       
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

    }
}




