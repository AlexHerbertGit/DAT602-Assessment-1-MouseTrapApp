namespace MouseTrapApp
{
    partial class EditUserInfoForm
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
            txtUsername = new TextBox();
            txtScore = new TextBox();
            numHealth = new NumericUpDown();
            chkIsAdmin = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtInventoryId = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)numHealth).BeginInit();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(232, 77);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(317, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtScore
            // 
            txtScore.Location = new Point(232, 106);
            txtScore.Name = "txtScore";
            txtScore.PlaceholderText = "Score";
            txtScore.Size = new Size(317, 23);
            txtScore.TabIndex = 1;
            // 
            // numHealth
            // 
            numHealth.Location = new Point(232, 135);
            numHealth.Name = "numHealth";
            numHealth.Size = new Size(120, 23);
            numHealth.TabIndex = 2;
            // 
            // chkIsAdmin
            // 
            chkIsAdmin.AutoSize = true;
            chkIsAdmin.Location = new Point(232, 164);
            chkIsAdmin.Name = "chkIsAdmin";
            chkIsAdmin.Size = new Size(78, 19);
            chkIsAdmin.TabIndex = 3;
            chkIsAdmin.Text = "Is Admin?";
            chkIsAdmin.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(232, 231);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 33);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save User Info";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(351, 231);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(104, 33);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(166, 80);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 6;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(190, 109);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 7;
            label2.Text = "Score";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(184, 137);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 8;
            label3.Text = "Health";
            // 
            // txtInventoryId
            // 
            txtInventoryId.Location = new Point(232, 189);
            txtInventoryId.Name = "txtInventoryId";
            txtInventoryId.PlaceholderText = "Inventory ID";
            txtInventoryId.Size = new Size(100, 23);
            txtInventoryId.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(155, 192);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 10;
            label4.Text = "Inventory ID";
            // 
            // EditUserInfoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 276);
            Controls.Add(label4);
            Controls.Add(txtInventoryId);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsAdmin);
            Controls.Add(numHealth);
            Controls.Add(txtScore);
            Controls.Add(txtUsername);
            Name = "EditUserInfoForm";
            Text = "EditUserInfoForm";
            ((System.ComponentModel.ISupportInitialize)numHealth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtScore;
        private NumericUpDown numHealth;
        private CheckBox chkIsAdmin;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtInventoryId;
        private Label label4;
    }
}