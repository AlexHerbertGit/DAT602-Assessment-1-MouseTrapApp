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

        private void btnLoginConfirm_Click(object sender, EventArgs e)
        {
            string username = txtUsernameLogin.Text;
            string password = txtPasswordLogin.Text;

            if (UserDOA.LoginUser(username, password))
            {
                MessageBox.Show("Login successful!");

                //Add logic to open the Main Menu Here
            }
            else
            {
                MessageBox.Show("Invalid username or password. PLease try again.");
            }
        }
    }
}
