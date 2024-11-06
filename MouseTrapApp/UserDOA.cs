using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using MouseTrapApp;

namespace MouseTrapApp
{
    public class UserDOA
    {
        private static string connectionString
        {
            get { return "Server=localhost;Port=3306;Database=MousetrapDB;Uid=root;password=Passw0rd123";  }
        }

        private static MySqlConnection _mySqlConnection = null;
        public static MySqlConnection mySqlConnection
        {
            get
            {
                if (_mySqlConnection == null)
                {
                    _mySqlConnection = new MySqlConnection(connectionString);
                }

                return _mySqlConnection;
            }
        }

        public static (bool loginSuccessful, bool accountLocked) LoginUser(string username, string password)
        {
            bool loginSuccessful = false;
            bool accountLocked = false;

            try
            {
                mySqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand("PlayerLogin", mySqlConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Set up input parameters for username and password
                    cmd.Parameters.AddWithValue("p_username", username);
                    cmd.Parameters.AddWithValue("p_password", password);

                    //Define output parameters for return values
                    cmd.Parameters.Add(new MySqlParameter("p_login_successful", MySqlDbType.Bit));
                    cmd.Parameters["p_login_successful"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new MySqlParameter("p_account_locked", MySqlDbType.Bit));
                    cmd.Parameters["p_account_locked"].Direction = ParameterDirection.Output;

                    //Execute Stored Procedure
                    cmd.ExecuteNonQuery();

                    //Retrieve output values
                    loginSuccessful = Convert.ToBoolean(cmd.Parameters["p_login_successful"].Value);
                    accountLocked = Convert.ToBoolean(cmd.Parameters["p_account_locked"].Value);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }

            return (loginSuccessful, accountLocked);
        }

        public static bool CreateUser(string username, string password)
        {
            bool registrationSuccessful = false;
            try
            {
                mySqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand("RegisterPlayer", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Set up input parameters for username and password
                    cmd.Parameters.AddWithValue("p_username", username);
                    cmd.Parameters.AddWithValue("p_password", password);

                    //Define the output parameters for registration success
                    cmd.Parameters.Add(new MySqlParameter("p_registration_successful", MySqlDbType.Bit));
                    cmd.Parameters["p_registration_successful"].Direction = ParameterDirection.Output;

                    //Execute the procedure
                    cmd.ExecuteNonQuery();

                    //Retrieve the output values
                    registrationSuccessful = Convert.ToBoolean(cmd.Parameters["p_registration_successful"].Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == System.Data.ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
            return registrationSuccessful;
        }
    }
}
