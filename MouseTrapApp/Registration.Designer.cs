namespace MouseTrapApp
{
    partial class Registration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            imgLogo = new PictureBox();
            groupBox1 = new GroupBox();
            btnRegistrationConfirm = new Button();
            registrationLabel = new Label();
            txtPasswordRegistration = new TextBox();
            txtUsernameRegistration = new TextBox();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // imgLogo
            // 
            imgLogo.BackgroundImage = (Image)resources.GetObject("imgLogo.BackgroundImage");
            imgLogo.BackgroundImageLayout = ImageLayout.Stretch;
            imgLogo.Location = new Point(532, 37);
            imgLogo.Name = "imgLogo";
            imgLogo.Size = new Size(182, 173);
            imgLogo.TabIndex = 2;
            imgLogo.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnRegistrationConfirm);
            groupBox1.Controls.Add(registrationLabel);
            groupBox1.Controls.Add(txtPasswordRegistration);
            groupBox1.Controls.Add(txtUsernameRegistration);
            groupBox1.Location = new Point(417, 231);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(405, 360);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // btnRegistrationConfirm
            // 
            btnRegistrationConfirm.Location = new Point(145, 217);
            btnRegistrationConfirm.Name = "btnRegistrationConfirm";
            btnRegistrationConfirm.Size = new Size(118, 27);
            btnRegistrationConfirm.TabIndex = 3;
            btnRegistrationConfirm.Text = "Submit";
            btnRegistrationConfirm.UseVisualStyleBackColor = true;
            btnRegistrationConfirm.Click += btnRegistrationConfirm_Click;
            // 
            // registrationLabel
            // 
            registrationLabel.AutoSize = true;
            registrationLabel.Font = new Font("Gadugi", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            registrationLabel.Location = new Point(135, 56);
            registrationLabel.Name = "registrationLabel";
            registrationLabel.Size = new Size(139, 28);
            registrationLabel.TabIndex = 2;
            registrationLabel.Text = "Registration";
            registrationLabel.Click += loginLabel_Click;
            // 
            // txtPasswordRegistration
            // 
            txtPasswordRegistration.Location = new Point(57, 165);
            txtPasswordRegistration.Name = "txtPasswordRegistration";
            txtPasswordRegistration.PlaceholderText = "Password";
            txtPasswordRegistration.Size = new Size(290, 23);
            txtPasswordRegistration.TabIndex = 1;
            txtPasswordRegistration.TextAlign = HorizontalAlignment.Center;
            txtPasswordRegistration.TextChanged += txtPasswordRegistration_TextChanged;
            // 
            // txtUsernameRegistration
            // 
            txtUsernameRegistration.Location = new Point(57, 105);
            txtUsernameRegistration.Name = "txtUsernameRegistration";
            txtUsernameRegistration.PlaceholderText = "Username";
            txtUsernameRegistration.Size = new Size(290, 23);
            txtUsernameRegistration.TabIndex = 0;
            txtUsernameRegistration.TextAlign = HorizontalAlignment.Center;
            txtUsernameRegistration.TextChanged += txtUsernameRegistration_TextChanged;
            // 
            // Registration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
            Controls.Add(groupBox1);
            Controls.Add(imgLogo);
            Name = "Registration";
            Text = "Registration";
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imgLogo;
        private GroupBox groupBox1;
        private Button btnRegistrationConfirm;
        private Label registrationLabel;
        private TextBox txtPasswordRegistration;
        private TextBox txtUsernameRegistration;
    }
}