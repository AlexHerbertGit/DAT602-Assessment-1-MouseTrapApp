using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using MouseTrapApp;
using System.Reflection.Metadata.Ecma335;

namespace MouseTrapApp
{
    public class DOA
    {
        private static string connectionString
        {
            get { return "Server=localhost;Port=3306;Database=MousetrapDB;Uid=root;password=Passw0rd123"; }
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


        public static (int gameId, int maxRows, int maxColumns, List<Tile> tiles) InitializeNewGameAndBoard()
        {
            int gameId = -1;
            int maxRows = 0;
            int maxColumns = 0;
            int mapId = -1;
            List<Tile> tiles = new List<Tile>();

            try
            {
                mySqlConnection.Open();

                // Call the CreateNewGame procedure to create a new record in Game table, the retreive the Game ID
                using (MySqlCommand createGameCmd = new MySqlCommand("CreateNewGame", mySqlConnection))
                {
                    createGameCmd.CommandType = CommandType.StoredProcedure;

                    // Define the output parameter for the Game ID
                    MySqlParameter gameIdParam = new MySqlParameter("p_game_id", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    createGameCmd.Parameters.Add(gameIdParam);

                    //Execute the procedure
                    createGameCmd.ExecuteNonQuery();

                    //Retrieve the Game ID from procedure output and assign to variable
                    gameId = gameIdParam.Value != DBNull.Value ? Convert.ToInt32(gameIdParam.Value) : -1;
                }

                // Fetch the map dimensions using the GetMapDimensionForGame procedure

                using (MySqlCommand getMapDimensionsCmd = new MySqlCommand("GetMapDimensionsForGame", mySqlConnection))
                {
                    getMapDimensionsCmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter for the Game ID
                    getMapDimensionsCmd.Parameters.AddWithValue("p_game_id", gameId);

                    //Output parameters for max rows and columns
                    MySqlParameter maxRowsParam = new MySqlParameter("p_max_rows", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    MySqlParameter maxColumnsParam = new MySqlParameter("p_max_columns", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    MySqlParameter mapIdParam = new MySqlParameter("p_map_id", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };

                    getMapDimensionsCmd.Parameters.Add(maxRowsParam);
                    getMapDimensionsCmd.Parameters.Add(maxColumnsParam);
                    getMapDimensionsCmd.Parameters.Add(mapIdParam);

                    //Execute the procedure
                    getMapDimensionsCmd.ExecuteNonQuery();

                    // Fecth max rows and columns from query results
                    maxRows = maxRowsParam.Value != DBNull.Value ? Convert.ToInt32(maxRowsParam.Value) : 0;
                    maxColumns = maxColumnsParam.Value != DBNull.Value ? Convert.ToInt32(maxColumnsParam.Value) : 0;
                    mapId = mapIdParam.Value != DBNull.Value ? Convert.ToInt32(mapIdParam.Value) : -1;
                }

                //Populate board with Items
                PopulateItemsOnBoard(mapId);

                //Retreive Tile data using the GetTilesForGame procedure
                using (MySqlCommand getTilesCmd = new MySqlCommand("GetTilesForGame", mySqlConnection))
                {
                    getTilesCmd.CommandType = CommandType.StoredProcedure;
                    getTilesCmd.Parameters.AddWithValue("p_game_id", gameId);

                    using (MySqlDataReader reader = getTilesCmd.ExecuteReader())
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
                Console.WriteLine("Error Inititating the game and board: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
            return (gameId, maxRows, maxColumns, tiles);
        }

        public static void PopulateItemsOnBoard(int mapId)
        {
            try
            {
                mySqlConnection.Open();

                using (MySqlCommand cmd = new MySqlCommand("PopulateItemsOnBoard", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_map_id", mapId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error populating items on board: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
        }
      
    }
}
