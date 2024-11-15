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
            // Hide the Administration form
            this.Hide();

            // Create and show the Registration form
            Registration registrationForm = new Registration();

            // Handle the FormClosed event to reopen Administration and refresh data
            registrationForm.FormClosed += (s, args) =>
            {
                this.Show();
                // Refresh active players and games after registration
                LoadActivePlayers();
                LoadActiveGames();
            };

            registrationForm.ShowDialog();
        }

        private void btnUpdatePlayerInfo_Click(object sender, EventArgs e)
        {
            if (activePlayerView.SelectedRows.Count > 0)
            {
                string selectedUsername = activePlayerView.SelectedRows[0].Cells["Username"].Value.ToString();
                try
                {
                    using (MySqlConnection connection = DOA.mySqlConnection)
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        using (MySqlCommand cmd = new MySqlCommand("GetUserDetails", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_username", selectedUsername);

                            cmd.Parameters.Add(new MySqlParameter("p_user_id", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                            cmd.Parameters.Add(new MySqlParameter("p_score", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                            cmd.Parameters.Add(new MySqlParameter("p_is_admin", MySqlDbType.Bit) { Direction = ParameterDirection.Output });
                            cmd.Parameters.Add(new MySqlParameter("p_health", MySqlDbType.Int32) { Direction = ParameterDirection.Output });
                            cmd.Parameters.Add(new MySqlParameter("p_inventory_id", MySqlDbType.Int32) { Direction = ParameterDirection.Output });

                            cmd.ExecuteNonQuery();

                            // Extract data from output parameters
                            int userId = Convert.ToInt32(cmd.Parameters["p_user_id"].Value);
                            int score = Convert.ToInt32(cmd.Parameters["p_score"].Value);
                            bool isAdmin = Convert.ToBoolean(cmd.Parameters["p_is_admin"].Value);
                            int health = Convert.ToInt32(cmd.Parameters["p_health"].Value);
                            int inventoryId = Convert.ToInt32(cmd.Parameters["p_inventory_id"].Value);

                            // Initialize EditUserInfoForm
                            EditUserInfoForm editForm = new EditUserInfoForm(userId, selectedUsername, score, health, isAdmin, inventoryId);

                            // Close the current Administration form
                            this.Hide();

                            // Show the edit form
                            editForm.FormClosed += (s, args) =>
                            {
                                // Reopen the Administration form after edit is done
                                this.Show();
                                // Refresh the active players and games
                                LoadActivePlayers();
                                LoadActiveGames();
                            };

                            editForm.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching user details: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a username to update.");
            }
        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            if (activePlayerView.SelectedRows.Count > 0)
            {
                string selectedUsername = activePlayerView.SelectedRows[0].Cells["Username"].Value.ToString();

                //Delete player confirmation
                var confirmResult = MessageBox.Show(
                    $"Are you sure you want to remove the player \"{selectedUsername}\"?", "Confirm User Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection connection = DOA.mySqlConnection)
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            using (MySqlCommand cmd = new MySqlCommand("DeletePlayerByUsername", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_username", selectedUsername);

                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string feedbackMessage = reader[0].ToString();
                                        MessageBox.Show(feedbackMessage, "Result");
                                    }
                                }
                            }
                        }

                        LoadActivePlayers();
                        LoadActiveGames();
                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show("Error removing player: " + ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a player from the list to remove.", "No Selection");
            }
        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            if (activeGameView.SelectedRows.Count > 0)
            {
                // Get the selected Game ID from the DataGridView
                int selectedGameId;
                bool isValid = int.TryParse(activeGameView.SelectedRows[0].Cells["game_id"].Value.ToString(), out selectedGameId);

                if (isValid)
                {
                    try
                    {
                        // Open MySQL connection
                        using (MySqlConnection connection = DOA.mySqlConnection)
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                            }

                            // Call the TerminateGame procedure
                            using (MySqlCommand cmd = new MySqlCommand("TerminateGame", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_game_id", selectedGameId);

                                // Execute the procedure
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        // Fetch and display the termination message
                                        string terminationMessage = reader["TerminationMessage"].ToString();
                                        MessageBox.Show(terminationMessage);
                                    }
                                }
                            }
                        }

                        // Refresh the DataGridView to reflect the changes
                        LoadActiveGames();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error terminating game: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Game ID. Please select a valid game.");
                }
            }
            else
            {
                MessageBox.Show("Please select a game to terminate.");
            }
        }

        private void activeGameView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void activePlayerView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
