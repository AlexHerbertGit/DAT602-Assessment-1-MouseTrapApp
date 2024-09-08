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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void loginLabel_Click(object sender, EventArgs e)
        {

        }

        private void txtUsernameRegistration_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPasswordRegistration_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistrationConfirm_Click(object sender, EventArgs e)
        {

            //Get username and password inputs from textbox inputs
            string username = txtUsernameRegistration.Text;
            string password = txtPasswordRegistration.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both a username and a password");
                return;
            }

            bool userCreated = UserDOA.CreateUser(username, password);

            if (userCreated)
            {
                MessageBox.Show("Registration successful!");

                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Please try again.");
            }
        }
    }
}
