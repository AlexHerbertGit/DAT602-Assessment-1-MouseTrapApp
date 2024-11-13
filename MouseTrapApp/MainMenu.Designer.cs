namespace MouseTrapApp
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            mainMenuGroupBox = new GroupBox();
            btnCloseApp = new Button();
            btnLogout = new Button();
            label1 = new Label();
            btnStartGame = new Button();
            btnAdminDash = new Button();
            imgLogo = new PictureBox();
            mainMenuGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            SuspendLayout();
            // 
            // mainMenuGroupBox
            // 
            mainMenuGroupBox.Controls.Add(btnAdminDash);
            mainMenuGroupBox.Controls.Add(btnCloseApp);
            mainMenuGroupBox.Controls.Add(btnLogout);
            mainMenuGroupBox.Controls.Add(label1);
            mainMenuGroupBox.Controls.Add(btnStartGame);
            mainMenuGroupBox.Location = new Point(391, 219);
            mainMenuGroupBox.Name = "mainMenuGroupBox";
            mainMenuGroupBox.Size = new Size(518, 399);
            mainMenuGroupBox.TabIndex = 0;
            mainMenuGroupBox.TabStop = false;
            mainMenuGroupBox.Text = "Main Menu";
            mainMenuGroupBox.Enter += groupBox1_Enter;
            // 
            // btnCloseApp
            // 
            btnCloseApp.Location = new Point(129, 310);
            btnCloseApp.Name = "btnCloseApp";
            btnCloseApp.Size = new Size(259, 47);
            btnCloseApp.TabIndex = 3;
            btnCloseApp.Text = "Close Application";
            btnCloseApp.UseVisualStyleBackColor = true;
            btnCloseApp.Click += btnCloseApp_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(130, 254);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(259, 47);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Log Out";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gadugi", 18F);
            label1.Location = new Point(191, 19);
            label1.Name = "label1";
            label1.Size = new Size(136, 28);
            label1.TabIndex = 1;
            label1.Text = "Main Menu";
            label1.Click += label1_Click;
            // 
            // btnStartGame
            // 
            btnStartGame.Location = new Point(130, 85);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(259, 47);
            btnStartGame.TabIndex = 0;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // btnAdminDash
            // 
            btnAdminDash.Location = new Point(130, 148);
            btnAdminDash.Name = "btnAdminDash";
            btnAdminDash.Size = new Size(259, 47);
            btnAdminDash.TabIndex = 4;
            btnAdminDash.Text = "Admin Dashboard";
            btnAdminDash.UseVisualStyleBackColor = true;
            btnAdminDash.Click += btnAdminDash_Click;
            // 
            // imgLogo
            // 
            imgLogo.BackgroundImage = (Image)resources.GetObject("imgLogo.BackgroundImage");
            imgLogo.BackgroundImageLayout = ImageLayout.Stretch;
            imgLogo.Location = new Point(561, 12);
            imgLogo.Name = "imgLogo";
            imgLogo.Size = new Size(182, 173);
            imgLogo.TabIndex = 2;
            imgLogo.TabStop = false;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
            Controls.Add(imgLogo);
            Controls.Add(mainMenuGroupBox);
            Name = "MainMenu";
            Text = "MainMenu";
            mainMenuGroupBox.ResumeLayout(false);
            mainMenuGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox mainMenuGroupBox;
        private Button btnStartGame;
        private Label label1;
        private Button btnLogout;
        private Button btnCloseApp;
        private Button btnAdminDash;
        private PictureBox imgLogo;
    }
}