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
using PUTAirlinesMobile.Resources;

namespace PUTAirlinesMobile.Helper
{
    public static class MySQLHelper
    {
        public static MySqlConnection getConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public static bool check_if_account_is_correct(string login,string password, MySqlConnection conn)
        {
            MySqlDataReader rdr = null;
            bool returned = true;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE Login=@log AND Password= @pass;", conn);

                cmd.Parameters.AddWithValue("@log", login);
                cmd.Parameters.AddWithValue("@pass", password);

                var result = cmd.ExecuteReader();
                if (result.HasRows) returned = true; else returned = false;

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

        public static bool findLogin(string login, MySqlConnection conn)
        {
            MySqlDataReader rdr = null;
            bool returned = true;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Client WHERE Login=@log;", conn);

                cmd.Parameters.AddWithValue("@log", login);
               
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
                string insert = "INSERT INTO Client VALUES ('@name',@surName','@individualNumber','@passportNumber','@city',";
                insert += "'@street','@postcode','@nationality','@log','@pass'";

                MySqlCommand cmd = new MySqlCommand(insert, conn);

                cmd.Parameters.AddWithValue("@name", client.getName());
                cmd.Parameters.AddWithValue("@surName", client.getLastName());
                cmd.Parameters.AddWithValue("@individualNumber", client.getIndividualNumber());
                cmd.Parameters.AddWithValue("@passportNumber", client.getPassportNumber());
                cmd.Parameters.AddWithValue("@city", client.getCity());
                cmd.Parameters.AddWithValue("@street", client.getStreet());
                cmd.Parameters.AddWithValue("@postcode", client.getPostcode());
                cmd.Parameters.AddWithValue("@nationality", client.getNationality());
                cmd.Parameters.AddWithValue("@log", client.getLogin());
                cmd.Parameters.AddWithValue("@pass", client.getPassword());

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
    
