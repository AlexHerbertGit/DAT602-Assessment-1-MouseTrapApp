namespace MouseTrapApp
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
            textBox1 = new TextBox();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox4 = new TextBox();
            button5 = new Button();
            button6 = new Button();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            groupBox1.SuspendLayout();
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
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Location = new Point(186, 205);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(916, 434);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(343, 50);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Selected Player Username";
            textBox1.Size = new Size(226, 23);
            textBox1.TabIndex = 0;
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
            // textBox2
            // 
            textBox2.Location = new Point(630, 79);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Active Players List";
            textBox2.Size = new Size(263, 269);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(343, 79);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Active Games List";
            textBox3.Size = new Size(281, 269);
            textBox3.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(343, 363);
            button1.Name = "button1";
            button1.Size = new Size(105, 29);
            button1.TabIndex = 4;
            button1.Text = "End Game";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(630, 363);
            button2.Name = "button2";
            button2.Size = new Size(84, 29);
            button2.TabIndex = 5;
            button2.Text = "Add Player";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(720, 363);
            button3.Name = "button3";
            button3.Size = new Size(84, 29);
            button3.TabIndex = 6;
            button3.Text = "Update Info";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(810, 363);
            button4.Name = "button4";
            button4.Size = new Size(84, 29);
            button4.TabIndex = 7;
            button4.Text = "Remove";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(575, 50);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "High Score";
            textBox4.Size = new Size(178, 23);
            textBox4.TabIndex = 8;
            // 
            // button5
            // 
            button5.Location = new Point(84, 79);
            button5.Name = "button5";
            button5.Size = new Size(155, 44);
            button5.TabIndex = 9;
            button5.Text = "Change Username";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(84, 143);
            button6.Name = "button6";
            button6.Size = new Size(152, 44);
            button6.TabIndex = 10;
            button6.Text = "Delete Account";
            button6.UseVisualStyleBackColor = true;
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
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imgLogo;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button6;
        private Button button5;
        private TextBox textBox4;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
    }
}