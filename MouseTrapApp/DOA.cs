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
        public static string connectionString
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

        public static (int gameId, int maxRows, int maxColumns, int mapId, List<Tile> tiles) InitializeNewGameAndBoard(int userId)
        {
            int gameId = -1;
            int maxRows = 0;
            int maxColumns = 0;
            int mapId = -1;
            List<Tile> tiles = new List<Tile>();

            try
            {
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }

                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Create a new game
                        using (MySqlCommand createGameCmd = new MySqlCommand("CreateNewGame", mySqlConnection, transaction))
                        {
                            createGameCmd.CommandType = CommandType.StoredProcedure;

                            MySqlParameter gameIdParam = new MySqlParameter("p_game_id", MySqlDbType.Int32)
                            {
                                Direction = ParameterDirection.Output
                            };
                            createGameCmd.Parameters.Add(gameIdParam);

                            createGameCmd.ExecuteNonQuery();
                            gameId = gameIdParam.Value != DBNull.Value ? Convert.ToInt32(gameIdParam.Value) : -1;
                        }

                        // Fetch map dimensions
                        using (MySqlCommand getMapDimensionsCmd = new MySqlCommand("GetMapDimensionsForGame", mySqlConnection, transaction))
                        {
                            getMapDimensionsCmd.CommandType = CommandType.StoredProcedure;
                            getMapDimensionsCmd.Parameters.AddWithValue("p_game_id", gameId);

                            MySqlParameter maxRowsParam = new MySqlParameter("p_max_rows", MySqlDbType.Int32) { Direction = ParameterDirection.Output };
                            MySqlParameter maxColumnsParam = new MySqlParameter("p_max_columns", MySqlDbType.Int32) { Direction = ParameterDirection.Output };
                            MySqlParameter mapIdParam = new MySqlParameter("p_map_id", MySqlDbType.Int32) { Direction = ParameterDirection.Output };

                            getMapDimensionsCmd.Parameters.Add(maxRowsParam);
                            getMapDimensionsCmd.Parameters.Add(maxColumnsParam);
                            getMapDimensionsCmd.Parameters.Add(mapIdParam);

                            getMapDimensionsCmd.ExecuteNonQuery();

                            maxRows = maxRowsParam.Value != DBNull.Value ? Convert.ToInt32(maxRowsParam.Value) : 0;
                            maxColumns = maxColumnsParam.Value != DBNull.Value ? Convert.ToInt32(maxColumnsParam.Value) : 0;
                            mapId = mapIdParam.Value != DBNull.Value ? Convert.ToInt32(mapIdParam.Value) : -1;
                        }

                        // Add user to the newly created game
                        bool userAddedToGame = UserDOA.AddUserToGame(userId, gameId);
                        if (!userAddedToGame)
                        {
                            throw new Exception("Failed to add user to the game.");
                        }
                        //Set user starting position on the home tile
                        using (MySqlCommand assignHomeTileCmd = new MySqlCommand("AssignUserToHomeTile", mySqlConnection, transaction))
                        {
                            assignHomeTileCmd.CommandType = CommandType.StoredProcedure;
                            assignHomeTileCmd.Parameters.AddWithValue("p_user_id", userId);
                            assignHomeTileCmd.Parameters.AddWithValue("p_map_id", mapId);

                            assignHomeTileCmd.ExecuteNonQuery();
                        }

                        // Populate items on board
                        PopulateItemsOnBoard(mapId, transaction);

                        // Retrieve tiles with updated items
                        using (MySqlCommand getTilesCmd = new MySqlCommand("GetTilesForGame", mySqlConnection, transaction))
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
                Console.WriteLine("Error initializing the game and board: " + ex.Message);
            }
            finally
            {
                if (mySqlConnection.State == ConnectionState.Open)
                {
                    mySqlConnection.Close();
                }
            }
            return (gameId, maxRows, maxColumns, mapId, tiles);
        }

        public static void PopulateItemsOnBoard(int mapId, MySqlTransaction transaction)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("PopulateItemsOnBoard", mySqlConnection, transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_map_id", mapId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Items populated on board for map ID: {mapId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error populating items on board: {ex.Message}");
                throw;
            }
        }
    }
}
