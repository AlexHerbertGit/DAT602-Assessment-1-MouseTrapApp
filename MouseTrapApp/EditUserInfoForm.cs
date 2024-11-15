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
    public partial class EditUserInfoForm : Form
    {
        private int userId;
        private string username;
        private int score;
        private int health;
        private bool isAdmin;
        private int inventoryId;
        public EditUserInfoForm(int userId, string username, int score, int health, bool isAdmin, int inventoryId)
        {
            InitializeComponent();

            // Assign values to form fields
            this.userId = userId;
            this.username = username;
            this.score = score;
            this.health = health;
            this.isAdmin = isAdmin;
            this.inventoryId = inventoryId;

            // Populate form fields with user data
            txtUsername.Text = username;
            txtScore.Text = score.ToString();
            numHealth.Value = health;
            chkIsAdmin.Checked = isAdmin;
            txtInventoryId.Text = inventoryId.ToString();
        }

        public event EventHandler UserInfoUpdated;

        private void OnUserInfoUpdated()
        {
            UserInfoUpdated?.Invoke(this, EventArgs.Empty);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = DOA.mySqlConnection)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (MySqlCommand cmd = new MySqlCommand("UpdateUserInfo", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_user_id", this.userId);
                        cmd.Parameters.AddWithValue("p_username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("p_score", int.Parse(txtScore.Text));
                        cmd.Parameters.AddWithValue("p_health", (int)numHealth.Value);
                        cmd.Parameters.AddWithValue("p_is_admin", chkIsAdmin.Checked);
                        cmd.Parameters.AddWithValue("p_inventory_id", txtInventoryId.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("User information updated successfully!");

                        // Trigger the UserInfoUpdated event
                        OnUserInfoUpdated();

                        // Close the form after saving
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user info: " + ex.Message);
            }
        }
    }
}
