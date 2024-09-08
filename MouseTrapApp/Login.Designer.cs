namespace MouseTrapApp
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            groupBox1 = new GroupBox();
            btnLoginConfirm = new Button();
            loginLabel = new Label();
            txtPasswordLogin = new TextBox();
            txtUsernameLogin = new TextBox();
            imgLogo = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLoginConfirm);
            groupBox1.Controls.Add(loginLabel);
            groupBox1.Controls.Add(txtPasswordLogin);
            groupBox1.Controls.Add(txtUsernameLogin);
            groupBox1.Location = new Point(425, 209);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 360);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // btnLoginConfirm
            // 
            btnLoginConfirm.Location = new Point(145, 217);
            btnLoginConfirm.Name = "btnLoginConfirm";
            btnLoginConfirm.Size = new Size(118, 27);
            btnLoginConfirm.TabIndex = 3;
            btnLoginConfirm.Text = "Submit";
            btnLoginConfirm.UseVisualStyleBackColor = true;
            btnLoginConfirm.Click += btnLoginConfirm_Click;
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Gadugi", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loginLabel.Location = new Point(165, 57);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(71, 28);
            loginLabel.TabIndex = 2;
            loginLabel.Text = "Login";
            loginLabel.Click += label1_Click;
            // 
            // txtPasswordLogin
            // 
            txtPasswordLogin.Location = new Point(57, 165);
            txtPasswordLogin.Name = "txtPasswordLogin";
            txtPasswordLogin.PlaceholderText = "Password";
            txtPasswordLogin.Size = new Size(290, 23);
            txtPasswordLogin.TabIndex = 1;
            txtPasswordLogin.TextAlign = HorizontalAlignment.Center;
            txtPasswordLogin.TextChanged += txtPasswordLogin_TextChanged;
            // 
            // txtUsernameLogin
            // 
            txtUsernameLogin.Location = new Point(57, 105);
            txtUsernameLogin.Name = "txtUsernameLogin";
            txtUsernameLogin.PlaceholderText = "Username";
            txtUsernameLogin.Size = new Size(290, 23);
            txtUsernameLogin.TabIndex = 0;
            txtUsernameLogin.TextAlign = HorizontalAlignment.Center;
            txtUsernameLogin.TextChanged += txtUsernameLogin_TextChanged;
            // 
            // imgLogo
            // 
            imgLogo.BackgroundImage = (Image)resources.GetObject("imgLogo.BackgroundImage");
            imgLogo.BackgroundImageLayout = ImageLayout.Stretch;
            imgLogo.Location = new Point(537, 30);
            imgLogo.Name = "imgLogo";
            imgLogo.Size = new Size(182, 173);
            imgLogo.TabIndex = 1;
            imgLogo.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
            Controls.Add(imgLogo);
            Controls.Add(groupBox1);
            Name = "Login";
            Text = "Login";
            Load += Login_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtUsernameLogin;
        private Label loginLabel;
        private TextBox txtPasswordLogin;
        private Button btnLoginConfirm;
        private PictureBox imgLogo;
    }
}
