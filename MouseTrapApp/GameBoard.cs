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
        public GameBoard(int gameId)
        {
            InitializeComponent();
            this.gameId = gameId;
            InitializeGameBoard();
            PopulateGameBoard(gameId);
        }

        private void InitializeGameBoard()
        {
            // Set the column count and configure column width
            dataGridView1.ColumnCount = 13; // 13 columns for the game board
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 60; // Set each column width to 60 pixels
                column.SortMode = DataGridViewColumnSortMode.NotSortable; 
            }

            // Add rows and configure row height
            dataGridView1.RowCount = 12; // 12 rows for the game board
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 50; // Set each row height to 60 pixels
            }

            // Set other properties for the DataGridView
            dataGridView1.RowHeadersVisible = false; 
            dataGridView1.ColumnHeadersVisible = false; 
            dataGridView1.AllowUserToResizeColumns = false; 
            dataGridView1.AllowUserToResizeRows = false; 
            dataGridView1.ScrollBars = ScrollBars.None; 
            dataGridView1.ReadOnly = true; 
            dataGridView1.DefaultCellStyle.Padding = new Padding(1); 
        }
        //Method to populate the game board using the tile data recieved from the database
        public void PopulateGameBoard(int gameId)
        {
            List<Tile> tiles = UserDOA.GetTilesForGame(gameId);

            Console.WriteLine($"Total tiles retrieved: {tiles.Count}"); // Debug output

            foreach (Tile tile in tiles)
            {
                int row = tile.PositionY; // Corrected to PositionY for row
                int col = tile.PositionX; // Corrected to PositionX for column

                // Access the specific cell in the DataGridView
                DataGridViewCell cell = dataGridView1.Rows[row].Cells[col];

                if (cell != null)
                {
                    Console.WriteLine($"Setting tile at ({tile.PositionX}, {tile.PositionY}) with TileTypeId {tile.TileTypeId}"); // Debug output

                    // Check TileTypeId and set cell properties accordingly
                    if (tile.TileTypeId == 1) // Home Tile
                    {
                        cell.Style.BackColor = Color.LightGray;
                    }
                    else if (tile.TileTypeId == 2) // Barrier Tile
                    {
                        // Convert cell to DataGridViewImageCell for images
                        dataGridView1.Rows[row].Cells[col] = new DataGridViewImageCell();
                        cell = dataGridView1.Rows[row].Cells[col];
                        cell.Value = Properties.Resources.BarrierImage; // Set the barrier image
                        cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    else if (tile.TileTypeId == 3) // Open Tile
                    {
                        cell.Style.BackColor = Color.Transparent;
                    }
                    else if (tile.TileTypeId == 4) // Finish Tile
                    {
                        cell.Style.BackColor = Color.Black;
                    }
                    else
                    {
                        Console.WriteLine($"Unknown TileTypeId: {tile.TileTypeId} at ({tile.PositionX}, {tile.PositionY})");
                    }
                }
                else
                {
                    Console.WriteLine($"No cell found at position ({tile.PositionX}, {tile.PositionY})");
                }
            }
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
