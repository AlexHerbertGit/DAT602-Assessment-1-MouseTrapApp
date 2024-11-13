using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseTrapApp
{
    public partial class MainMenu : Form
    {
        private User _loggedInUser;
        public MainMenu(User loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                var (gameId, maxRows, maxColumns, mapId, tiles) = DOA.InitializeNewGameAndBoard(_loggedInUser.Userid);

                if (!tiles.Any())
                {
                    Console.WriteLine("Warning: No tiles were loaded for this game.");
                }
                else
                {
                    Console.WriteLine($"Loaded {tiles.Count} tiles for game board.");
                }

                GameBoard gameBoard = new GameBoard(gameId, maxRows, maxColumns, mapId, tiles, _loggedInUser);
                gameBoard.ShowDialog();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting the game: " + ex.Message);
            }
        }

        private void btnAdminDash_Click(object sender, EventArgs e)
        {
            if (_loggedInUser?.IsAdmin == true)
            {
                Administration adminForm = new Administration();
                adminForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Access Denied: This feature is restricted to admin users only.", "Admin Only", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
