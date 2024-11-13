﻿namespace MouseTrapApp
{
    partial class Administration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administration));
            imgLogo = new PictureBox();
            groupBox1 = new GroupBox();
            btnDeleteAccount = new Button();
            btnChangeUsername = new Button();
            txtUserScore = new TextBox();
            btnRemovePlayer = new Button();
            btnUpdatePlayerInfo = new Button();
            btnAddPlayer = new Button();
            btnEndGame = new Button();
            label1 = new Label();
            txtSelectedUser = new TextBox();
            activeGameView = new DataGridView();
            activePlayerView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)activeGameView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)activePlayerView).BeginInit();
            SuspendLayout();
            // 
            // imgLogo
            // 
            imgLogo.BackgroundImage = (Image)resources.GetObject("imgLogo.BackgroundImage");
            imgLogo.BackgroundImageLayout = ImageLayout.Stretch;
            imgLogo.Location = new Point(540, 12);
            imgLogo.Name = "imgLogo";
            imgLogo.Size = new Size(182, 173);
            imgLogo.TabIndex = 3;
            imgLogo.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(activePlayerView);
            groupBox1.Controls.Add(activeGameView);
            groupBox1.Controls.Add(btnDeleteAccount);
            groupBox1.Controls.Add(btnChangeUsername);
            groupBox1.Controls.Add(txtUserScore);
            groupBox1.Controls.Add(btnRemovePlayer);
            groupBox1.Controls.Add(btnUpdatePlayerInfo);
            groupBox1.Controls.Add(btnAddPlayer);
            groupBox1.Controls.Add(btnEndGame);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtSelectedUser);
            groupBox1.Location = new Point(186, 205);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(916, 434);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // btnDeleteAccount
            // 
            btnDeleteAccount.Location = new Point(84, 143);
            btnDeleteAccount.Name = "btnDeleteAccount";
            btnDeleteAccount.Size = new Size(152, 44);
            btnDeleteAccount.TabIndex = 10;
            btnDeleteAccount.Text = "Delete Account";
            btnDeleteAccount.UseVisualStyleBackColor = true;
            btnDeleteAccount.Click += btnDeleteAccount_Click;
            // 
            // btnChangeUsername
            // 
            btnChangeUsername.Location = new Point(84, 79);
            btnChangeUsername.Name = "btnChangeUsername";
            btnChangeUsername.Size = new Size(155, 44);
            btnChangeUsername.TabIndex = 9;
            btnChangeUsername.Text = "Change Username";
            btnChangeUsername.UseVisualStyleBackColor = true;
            btnChangeUsername.Click += btnChangeUsername_Click;
            // 
            // txtUserScore
            // 
            txtUserScore.Location = new Point(575, 50);
            txtUserScore.Name = "txtUserScore";
            txtUserScore.PlaceholderText = "High Score";
            txtUserScore.Size = new Size(178, 23);
            txtUserScore.TabIndex = 8;
            // 
            // btnRemovePlayer
            // 
            btnRemovePlayer.Location = new Point(810, 363);
            btnRemovePlayer.Name = "btnRemovePlayer";
            btnRemovePlayer.Size = new Size(84, 29);
            btnRemovePlayer.TabIndex = 7;
            btnRemovePlayer.Text = "Remove";
            btnRemovePlayer.UseVisualStyleBackColor = true;
            btnRemovePlayer.Click += btnRemovePlayer_Click;
            // 
            // btnUpdatePlayerInfo
            // 
            btnUpdatePlayerInfo.Location = new Point(720, 363);
            btnUpdatePlayerInfo.Name = "btnUpdatePlayerInfo";
            btnUpdatePlayerInfo.Size = new Size(84, 29);
            btnUpdatePlayerInfo.TabIndex = 6;
            btnUpdatePlayerInfo.Text = "Update Info";
            btnUpdatePlayerInfo.UseVisualStyleBackColor = true;
            btnUpdatePlayerInfo.Click += btnUpdatePlayerInfo_Click;
            // 
            // btnAddPlayer
            // 
            btnAddPlayer.Location = new Point(630, 363);
            btnAddPlayer.Name = "btnAddPlayer";
            btnAddPlayer.Size = new Size(84, 29);
            btnAddPlayer.TabIndex = 5;
            btnAddPlayer.Text = "Add Player";
            btnAddPlayer.UseVisualStyleBackColor = true;
            btnAddPlayer.Click += btnAddPlayer_Click;
            // 
            // btnEndGame
            // 
            btnEndGame.Location = new Point(343, 363);
            btnEndGame.Name = "btnEndGame";
            btnEndGame.Size = new Size(105, 29);
            btnEndGame.TabIndex = 4;
            btnEndGame.Text = "End Game";
            btnEndGame.UseVisualStyleBackColor = true;
            btnEndGame.Click += btnEndGame_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gadugi", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(343, 19);
            label1.Name = "label1";
            label1.Size = new Size(204, 28);
            label1.TabIndex = 1;
            label1.Text = "Admin Dashboard";
            label1.Click += label1_Click;
            // 
            // txtSelectedUser
            // 
            txtSelectedUser.Location = new Point(343, 50);
            txtSelectedUser.Name = "txtSelectedUser";
            txtSelectedUser.PlaceholderText = "Selected Player Username";
            txtSelectedUser.Size = new Size(226, 23);
            txtSelectedUser.TabIndex = 0;
            // 
            // activeGameView
            // 
            activeGameView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            activeGameView.Location = new Point(342, 89);
            activeGameView.Name = "activeGameView";
            activeGameView.Size = new Size(227, 268);
            activeGameView.TabIndex = 11;
            activeGameView.CellContentClick += activeGameView_CellContentClick;
            // 
            // activePlayerView
            // 
            activePlayerView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            activePlayerView.Location = new Point(575, 89);
            activePlayerView.Name = "activePlayerView";
            activePlayerView.Size = new Size(240, 268);
            activePlayerView.TabIndex = 12;
            activePlayerView.CellContentClick += activePlayerView_CellContentClick;
            // 
            // Administration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
            Controls.Add(groupBox1);
            Controls.Add(imgLogo);
            Name = "Administration";
            Text = "Administration";
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)activeGameView).EndInit();
            ((System.ComponentModel.ISupportInitialize)activePlayerView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imgLogo;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtSelectedUser;
        private Button btnDeleteAccount;
        private Button btnChangeUsername;
        private TextBox txtUserScore;
        private Button btnRemovePlayer;
        private Button btnUpdatePlayerInfo;
        private Button btnAddPlayer;
        private Button btnEndGame;
        private DataGridView activePlayerView;
        private DataGridView activeGameView;
    }
}