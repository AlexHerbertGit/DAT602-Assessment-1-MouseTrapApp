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

        public static (bool loginSuccessful, bool accountLocked, User? user) LoginUser(string username, string password)
        {
            bool loginSuccessful = false;
            bool accountLocked = false;
            User user = null;

            try
            {
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }

                using (MySqlTransaction transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Attempt Login and Lockout Checks
                        using (MySqlCommand cmd = new MySqlCommand("PlayerLogin", mySqlConnection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Set input parameters
                            cmd.Parameters.AddWithValue("p_username", username);
                            cmd.Parameters.AddWithValue("p_password", password);

                            // Define output parameters
                            cmd.Parameters.Add(new MySqlParameter("p_login_successful", MySqlDbType.Bit) { Direction = ParameterDirection.Output });
                            cmd.Parameters.Add(new MySqlParameter("p_account_locked", MySqlDbType.Bit) { Direction = ParameterDirection.Output });

                            // Execute Stored Procedure
                            cmd.ExecuteNonQuery();

                            // Retrieve output values
                            loginSuccessful = Convert.ToBoolean(cmd.Parameters["p_login_successful"].Value);
                            accountLocked = Convert.ToBoolean(cmd.Parameters["p_account_locked"].Value);

                            Console.WriteLine($"Login successful: {loginSuccessful}, Account locked: {accountLocked}");
                        }

                        // Step 2: Retrieve User Details if Login Succeeded
                        if (loginSuccessful)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("GetUserDetails", mySqlConnection, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_username", username);

                                cmd.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                                cmd.Parameters.Add(new MySqlParameter("p_score", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                                cmd.Parameters.Add(new MySqlParameter("p_is_admin", MySqlDbType.Bit) { Direction = ParameterDirection.Output });
                                cmd.Parameters.Add(new MySqlParameter("p_health", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                                cmd.Parameters.Add(new MySqlParameter("p_inventory_id", MySqlDbType.Int32) { Direction = ParameterDirection.Output });

                                // Execute stored procedure
                                cmd.ExecuteNonQuery();

                                // Confirm we have all required details
                                if (cmd.Parameters["p_user_id"].Value != DBNull.Value)
                                {
                                    // Populate User object with values
                                    user = new User
                                    {
                                        Userid = Convert.ToInt32(cmd.Parameters["p_user_id"].Value),
                                        Username = username,
                                        Score = Convert.ToInt32(cmd.Parameters["p_score"].Value),
                                        IsAdmin = Convert.ToInt32(cmd.Parameters["p_is_admin"].Value) == 1,
                                        Health = Convert.ToInt32(cmd.Parameters["p_health"].Value),
                                        InventoryId = cmd.Parameters["p_inventory_id"].Value == DBNull.Value ? (int?)null : Convert.ToInt32(cmd.Parameters["p_inventory_id"].Value)
                                    };

                                    Console.WriteLine($"Retrieved User: ID={user.Userid}, Admin={user.IsAdmin}");
                                }
                                else
                                {
                                    throw new Exception("User details not found.");
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }

            return (loginSuccessful, accountLocked, user);
        }

        public static bool CreateUser(string username, string password)
        {
            bool registrationSuccessful = false;
            try
            {
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }
                
                using (MySqlTransaction transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("RegisterPlayer", mySqlConnection, transaction))
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

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
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

        public static bool AddUserToGame(int userId, int gameId)
        {
            bool userAddedToGame = false;

            try
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }

                using (MySqlTransaction transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("AddUserToGame", mySqlConnection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Set the parameters
                            cmd.Parameters.AddWithValue("p_user_id", userId);
                            cmd.Parameters.AddWithValue("p_game_id", gameId);

                            // Execute the stored procedure
                            cmd.ExecuteNonQuery();
                            userAddedToGame = true;
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding user to game: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }

            return userAddedToGame;
        }
    }
}
