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

        public static List<Tile> GetTilesForGame(int gameId)
        {
            List<Tile> tiles = new List<Tile>();

            try
            {
                mySqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT position_y, position_x, TileTypeid, Itemid, Userid FROM Tile WHERE Mapid = @mapId", mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@mapId", gameId); // Ensure this matches Mapid if gameId == Mapid

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tile tile = new Tile
                            {
                                PositionY = reader.GetInt32("position_y"),
                                PositionX = reader.GetInt32("position_x"),
                                TileTypeId = reader.GetInt32("TileTypeid"),
                                ItemId = reader.IsDBNull("Itemid") ? (int?)null : reader.GetInt32("Itemid"),
                                UserId = reader.IsDBNull("Userid") ? (int?)null : reader.GetInt32("Userid")
                            };

                            tiles.Add(tile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving tiles: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
            return tiles;
        }

        public static int InitializeNewGameAndBoard()
        {
            int gameId = -1;

            try
            {
                mySqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand("InitializeNewGameAndBoard", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define the output parameter for the game ID
                    MySqlParameter gameIdParam = new MySqlParameter("p_game_id", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(gameIdParam);

                    // Execute the procedure
                    cmd.ExecuteNonQuery();

                    // Retrieve the game ID from the output parameter
                    gameId = gameIdParam.Value != DBNull.Value ? Convert.ToInt32(gameIdParam.Value) : -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing the game and board: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
            return gameId;
        }
    }
}
