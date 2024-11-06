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
        private List<Tile> tiles;

        public GameBoard(int gameId, int maxRows, int maxColumns, List<Tile> tiles)
        {
            InitializeComponent();

            //Initialize a new game and get game board dimensions and tiles
            this.gameId = gameId;
            this.maxRows = maxRows;
            this.maxColumns = maxColumns;
            this.tiles = tiles ?? new List<Tile>();

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
                        Console.WriteLine($"Setting tile at ({tile.PositionX}. {tile.PositionY})");

                        // Check TileTypeId and set all the cell properties according to the value
                        if (tile.TileTypeId == 1)
                        {
                            cell.Style.BackColor = Color.LightGray;
                        }
                        else if (tile.TileTypeId == 2)
                        {
                            // Convert cell to DataGridViewImageCell for images
                            DataGridViewImageCell imageCell = new DataGridViewImageCell
                            {
                                Value = Properties.Resources.BarrierImage,
                                ImageLayout = DataGridViewImageCellLayout.Zoom
                            };

                            dataGridView1.Rows[row].Cells[col] = imageCell;
                            imageCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        else if (tile.TileTypeId == 3)
                        {
                            cell.Style.BackColor = Color.Transparent;
                        }
                        else if (tile.TileTypeId == 4)
                        {
                            cell.Style.BackColor = Color.Black;
                        }
                        else
                        {
                            Console.WriteLine($"No cell found at position ({tile.PositionX}, {tile.PositionY}");
                        }
                    }
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
