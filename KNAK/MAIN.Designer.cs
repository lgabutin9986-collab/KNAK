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
            btnEndless = new Button();
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
            btnplay.BackgroundImage = (Image)resources.GetObject("btnplay.BackgroundImage");
            btnplay.BackgroundImageLayout = ImageLayout.Zoom;
            btnplay.Cursor = Cursors.Hand;
            btnplay.FlatAppearance.BorderSize = 0;
            btnplay.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnplay.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnplay.FlatStyle = FlatStyle.Flat;
            btnplay.Location = new Point(67, 160);
            btnplay.Name = "btnplay";
            btnplay.Size = new Size(616, 283);
            btnplay.TabIndex = 2;
            btnplay.UseVisualStyleBackColor = false;
            btnplay.Click += btnplay_Click;
            // 
            // btnEndless
            // 
            btnEndless.BackColor = Color.Transparent;
            btnEndless.BackgroundImage = Properties.Resources.endless_button_locked__1_;
            btnEndless.BackgroundImageLayout = ImageLayout.Zoom;
            btnEndless.Cursor = Cursors.Hand;
            btnEndless.FlatAppearance.BorderSize = 0;
            btnEndless.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnEndless.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnEndless.FlatStyle = FlatStyle.Flat;
            btnEndless.Location = new Point(282, 344);
            btnEndless.Name = "btnEndless";
            btnEndless.Size = new Size(192, 46);
            btnEndless.TabIndex = 7;
            btnEndless.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.kainan_logo__1__removebg_preview;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(57, -28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(647, 278);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
          
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(895, 560);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(296, 10);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
           
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
            btnexit.Location = new Point(67, 286);
            btnexit.Name = "btnexit";
            btnexit.Size = new Size(616, 283);
            btnexit.TabIndex = 6;
            btnexit.Text = "--";
            btnexit.UseVisualStyleBackColor = false;
            btnexit.Click += btnexit_Click;
            // 
            // MAIN
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(755, 491);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnplay);
            Controls.Add(btnEndless);
            Controls.Add(btnexit);
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
        private Button btnEndless;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnexit;
    }
}
