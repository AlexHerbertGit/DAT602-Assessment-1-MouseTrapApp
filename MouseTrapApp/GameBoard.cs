using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseTrapApp
{
    public partial class GameBoard : Form
    {
        private int gameId;
        private int maxRows;
        private int maxColumns;
        private int mapId;
        private List<Tile> tiles;
        private int currentX = 0;
        private int currentY = 0;
        private User _loggedInUser;

        public GameBoard(int gameId, int maxRows, int maxColumns, int mapId, List<Tile> tiles, User loggedInUser)
        {
            InitializeComponent();

            //Initialize a new game and get game board dimensions and tiles
            this.gameId = gameId;
            this.maxRows = maxRows;
            this.maxColumns = maxColumns;
            this.mapId = mapId;
            this.tiles = tiles ?? new List<Tile>();
            this._loggedInUser = loggedInUser;

            //Set up the data grid view with the correct dimensions
            InitializeGameBoard();

            //Populate the game board with tile data
            PopulateGameBoard();
        }

        private void InitializeGameBoard()
        {
            //Set the column count and configuration
            dataGridView1.ColumnCount = maxColumns;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 50;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //Add rows and configuration
            dataGridView1.RowCount = maxRows;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 50;
            }

            //Set DataGridView properties to hide column and row headers etc.
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Padding = new Padding(1);
        }

        public void PopulateGameBoard()
        {
            Console.WriteLine($"Total tiles retrieved: {tiles.Count}");

            foreach (Tile tile in tiles)
            {
                int row = tile.PositionY;
                int col = tile.PositionX;

                if (row < maxRows && col < maxColumns)
                {
                    DataGridViewCell cell = dataGridView1.Rows[row].Cells[col];

                    if (cell != null)
                    {
                        Console.WriteLine($"Setting tile at ({tile.PositionX}, {tile.PositionY})");

                        // Check if there's an item on the tile first
                        if (tile.ItemId != null)
                        {
                            DataGridViewImageCell itemCell = new DataGridViewImageCell
                            {
                                ImageLayout = DataGridViewImageCellLayout.Zoom,
                                Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                            };

                            // Set the image based on ItemId
                            if (tile.ItemId == 1)
                            {
                                itemCell.Value = Properties.Resources.CheeseImage;
                            }
                            else if (tile.ItemId == 2)
                            {
                                itemCell.Value = Properties.Resources.PaperClipImage;
                            }
                            else if (tile.ItemId == 3)
                            {
                                itemCell.Value = Properties.Resources.PeanutImage;
                            }

                            dataGridView1.Rows[row].Cells[col] = itemCell; // Add item image to cell
                        }
                        else
                        {
                            // Only set the background for TileTypeId if there is no item
                            if (tile.TileTypeId == 1)
                            {
                                DataGridViewImageCell characterCell = new DataGridViewImageCell
                                {
                                    Value = Properties.Resources.PlayerImage,
                                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                                    Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                                };
                                dataGridView1.Rows[row].Cells[col] = characterCell;
                            }
                            else if (tile.TileTypeId == 2)
                            {
                                DataGridViewImageCell barrierCell = new DataGridViewImageCell
                                {
                                    Value = Properties.Resources.BarrierImage,
                                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                                    Style = { Alignment = DataGridViewContentAlignment.MiddleCenter }
                                };
                                dataGridView1.Rows[row].Cells[col] = barrierCell;
                            }
                            else if (tile.TileTypeId == 3)
                            {
                                cell.Style.BackColor = Color.White; // Empty Tile
                            }
                            else if (tile.TileTypeId == 4)
                            {
                                cell.Style.BackColor = Color.Black; // Finish Tile
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No cell found at position ({tile.PositionX}, {tile.PositionY})");
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int targetX = e.ColumnIndex;
            int targetY = e.RowIndex;

            if (targetX >= 0 && targetY >= 0 && targetX < maxColumns && targetY < maxRows)
            {
                bool moveSuccessful = MovePlayerToTileInDatabase(targetX, targetY);

                if (moveSuccessful)
                {
                    RefreshBoardAndTiles(); // Refresh board and tiles to reflect database changes
                    UpdatePlayerPositionOnBoard(targetX, targetY); // Update the player image on the board
                }
                else
                {
                    MessageBox.Show("Invalid move. Please select an adjacent tile or avoid the barriers.");
                }
            }
        }

        private bool MovePlayerToTileInDatabase(int targetX, int targetY)
        {
            bool success = false;
            try
            {
                if (DOA.mySqlConnection.State == ConnectionState.Closed)
                {
                    DOA.mySqlConnection.Open();
                }
                using (MySqlCommand cmd = new MySqlCommand("MovePlayerToTile", DOA.mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_user_id", _loggedInUser.Userid);
                    cmd.Parameters.AddWithValue("p_current_x", currentX);
                    cmd.Parameters.AddWithValue("p_current_y", currentY);
                    cmd.Parameters.AddWithValue("p_target_x", targetX);
                    cmd.Parameters.AddWithValue("p_target_y", targetY);

                    cmd.ExecuteNonQuery();
                    success = true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error moving player in the database: " + ex.Message);
                MessageBox.Show("Failed to move player: " + ex.Message, "Move Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }
            finally
            {
                if (DOA.mySqlConnection.State == ConnectionState.Open)
                {
                    DOA.mySqlConnection.Close();
                }
            }
            return success;
        }

        private void UpdatePlayerPositionOnBoard(int targetX, int targetY)
        {
            // Clear the previous position of the player character
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == Properties.Resources.PlayerImage)
                    {
                        cell.Value = null; // Remove character image from the previous tile
                        break;
                    }
                }
            }

            // Set the character image on the new target tile
            dataGridView1.Rows[targetY].Cells[targetX].Value = Properties.Resources.PlayerImage;
            currentX = targetX; // Update the current X and Y coordinates
            currentY = targetY;
        }

        private void RefreshBoardAndTiles()
        {
            tiles = GetUpdatedTilesFromDatabase(); // Re-fetch updated tile data
            PopulateGameBoard(); // Re-render the board with updated tile states
        }

        private List<Tile> GetUpdatedTilesFromDatabase()
        {
            List<Tile> updatedTiles = new List<Tile>();

            try
            {
                if (DOA.mySqlConnection.State == ConnectionState.Closed)
                {
                    DOA.mySqlConnection.Open();
                }

                using (MySqlCommand getTilesCmd = new MySqlCommand("GetTilesForGame", DOA.mySqlConnection))
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
                            updatedTiles.Add(tile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving updated tiles: " + ex.Message);
            }
            finally
            {
                if (DOA.mySqlConnection.State == ConnectionState.Open)
                {
                    DOA.mySqlConnection.Close();
                }
            }

            return updatedTiles;
        }
    }
}
