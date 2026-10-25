namespace KNAK
{
    partial class MAIN
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAIN));
            btnplay = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnexit = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnplay
            // 
            btnplay.BackColor = Color.Transparent;
            btnplay.BackgroundImage = Properties.Resources.story_mode_buttom__1_;
            btnplay.BackgroundImageLayout = ImageLayout.Zoom;
            btnplay.Cursor = Cursors.Hand;
            btnplay.FlatAppearance.BorderSize = 0;
            btnplay.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnplay.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnplay.FlatStyle = FlatStyle.Flat;
            btnplay.Location = new Point(282, 222);
            btnplay.Name = "btnplay";
            btnplay.Size = new Size(192, 46);
            btnplay.TabIndex = 2;
            btnplay.UseVisualStyleBackColor = false;
            btnplay.Click += btnplay_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.kainan_logo__1__removebg_preview;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(-30, -117);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(833, 488);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(629, 377);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(174, 155);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // btnexit
            // 
            btnexit.BackColor = Color.Transparent;
            btnexit.BackgroundImage = (Image)resources.GetObject("btnexit.BackgroundImage");
            btnexit.BackgroundImageLayout = ImageLayout.Zoom;
            btnexit.Cursor = Cursors.Hand;
            btnexit.FlatAppearance.BorderSize = 0;
            btnexit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnexit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnexit.FlatStyle = FlatStyle.Flat;
            btnexit.Location = new Point(111, 270);
            btnexit.Name = "btnexit";
            btnexit.Size = new Size(535, 262);
            btnexit.TabIndex = 6;
            btnexit.UseVisualStyleBackColor = false;
            btnexit.Click += btnexit_Click_1;
            // 
            // MAIN
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(755, 491);
            Controls.Add(pictureBox2);
            Controls.Add(btnplay);
            Controls.Add(btnexit);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MAIN";
            StartPosition = FormStartPosition.CenterScreen;
            Load += KNAK_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnplay;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnexit;
    }
}
