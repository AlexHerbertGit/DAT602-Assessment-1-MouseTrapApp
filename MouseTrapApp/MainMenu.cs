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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
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
            //Retrieve the GameID for the new game
            int gameId = UserDOA.InitializeNewGameAndBoard();

            //Create an instance of the GameBoard
            GameBoard gameBoard = new GameBoard(gameId);

            //Show the GameBoard form window
            gameBoard.ShowDialog();

            this.Hide();
        }
    }
}
