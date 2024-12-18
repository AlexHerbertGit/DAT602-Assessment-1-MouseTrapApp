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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUsernameLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPasswordLogin_TextChanged(Object sender, EventArgs e)
        {

        }

        private void btnOpenRegistrationForm_Click(object sender, EventArgs e)
        {
            Registration registrationForm = new Registration();

            registrationForm.Show();
        }

        private void btnLoginConfirm_Click(object sender, EventArgs e)
        {
            string username = txtUsernameLogin.Text;
            string password = txtPasswordLogin.Text;

            var (loginSuccessful, accountLocked, user) = UserDOA.LoginUser(username, password);

            if (accountLocked)
            {
                MessageBox.Show("User Account is locked due to multiple failed login attempts, please contact an admin.");
            }
            else if (loginSuccessful && user != null)
            {
                MessageBox.Show("Login successful!");
                
                // Create and Display Main Menu Window
                MainMenu mainMenu = new MainMenu(user);
                mainMenu.Show();

                MessageBox.Show(
                    $"User Information:\n\n" +
                    $"Username: {user.Username}\n" +
                    $"User ID: {user.Userid}\n" +
                    $"Score: {user.Score}\n" +
                    $"Is Admin: {user.IsAdmin}\n" +
                    $"Health: {user.Health}\n" +
                    $"Inventory ID: {user.InventoryId}",
                    "User Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
