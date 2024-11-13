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
    public partial class Administration : Form
    {
        public Administration()
        {
            InitializeComponent();
            LoadActiveGames();
            LoadActivePlayers();
        }

        // Load Game data from database to be displayed in Data Grid View
        private void LoadActiveGames()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DOA.connectionString)) // Use a new connection
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("GetAllUserGames", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable gameTable = new DataTable();
                            adapter.Fill(gameTable);

                            // Bind directly to the DataSource without adding columns manually
                            activeGameView.DataSource = gameTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading active games: " + ex.Message);
            }
        }

        // Load active players into activePlayerView using data fetched from the database
        private void LoadActivePlayers()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DOA.connectionString)) // Use a new connection
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("GetAllUsers", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable playerTable = new DataTable();
                            adapter.Fill(playerTable);

                            // Bind directly to the DataSource without adding columns manually
                            activePlayerView.DataSource = playerTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading active players: " + ex.Message);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdatePlayerInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {

        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

        }

        private void activeGameView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void activePlayerView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
