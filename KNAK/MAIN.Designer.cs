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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAIN));
            pictureBox1 = new PictureBox();
            btnplay = new Button();
            btnEndless = new Button();
            btnExit = new Button();
            btnShop = new Button();
            pictureBox2 = new PictureBox();
            panelOverlay = new Panel();
            panelTutorial = new Panel();
            btnBack = new Button();
            btnNext = new Button();
            pictureBoxTutorial = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelTutorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTutorial).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(168, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(415, 187);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // btnplay
            // 
            btnplay.BackColor = Color.Transparent;
            btnplay.BackgroundImage = Properties.Resources.story_mode_buttom_removebg_preview;
            btnplay.BackgroundImageLayout = ImageLayout.Zoom;
            btnplay.Cursor = Cursors.Hand;
            btnplay.FlatAppearance.BorderSize = 0;
            btnplay.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnplay.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnplay.FlatStyle = FlatStyle.Flat;
            btnplay.Location = new Point(267, 210);
            btnplay.Name = "btnplay";
            btnplay.Size = new Size(220, 50);
            btnplay.TabIndex = 2;
            btnplay.UseVisualStyleBackColor = false;
            btnplay.Click += btnplay_Click;
            // 
            // btnEndless
            // 
            btnEndless.BackColor = Color.Transparent;
            btnEndless.BackgroundImage = Properties.Resources.endless_button_locked_removebg_preview;
            btnEndless.BackgroundImageLayout = ImageLayout.Zoom;
            btnEndless.Cursor = Cursors.Hand;
            btnEndless.FlatAppearance.BorderSize = 0;
            btnEndless.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnEndless.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnEndless.FlatStyle = FlatStyle.Flat;
            btnEndless.Location = new Point(262, 260);
            btnEndless.Name = "btnEndless";
            btnEndless.Size = new Size(229, 67);
            btnEndless.TabIndex = 11;
            btnEndless.UseVisualStyleBackColor = false;
            btnEndless.Click += btnEndless_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.BackgroundImage = Properties.Resources.exit_button_removebg_preview;
            btnExit.BackgroundImageLayout = ImageLayout.Zoom;
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(267, 330);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(220, 50);
            btnExit.TabIndex = 12;
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnexit_Click;
            // 
            // btnShop
            // 
            btnShop.BackColor = Color.Transparent;
            btnShop.BackgroundImage = Properties.Resources.cart_button_removebg_preview;
            btnShop.BackgroundImageLayout = ImageLayout.Zoom;
            btnShop.Cursor = Cursors.Hand;
            btnShop.FlatAppearance.BorderSize = 0;
            btnShop.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnShop.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnShop.FlatStyle = FlatStyle.Flat;
            btnShop.Location = new Point(352, 395);
            btnShop.Name = "btnShop";
            btnShop.Size = new Size(50, 50);
            btnShop.TabIndex = 15;
            btnShop.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resources.tuts_button_removebg_preview;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Location = new Point(686, 421);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(47, 58);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // panelOverlay
            // 
            panelOverlay.BackColor = Color.Transparent;
            panelOverlay.Dock = DockStyle.Fill;
            panelOverlay.Location = new Point(0, 0);
            panelOverlay.Name = "panelOverlay";
            panelOverlay.Size = new Size(755, 491);
            panelOverlay.TabIndex = 13;
            panelOverlay.Visible = false;
            panelOverlay.Click += panelOverlay_Click;
            // 
            // panelTutorial
            // 
            panelTutorial.BackColor = Color.Transparent;
            panelTutorial.Controls.Add(btnBack);
            panelTutorial.Controls.Add(btnNext);
            panelTutorial.Controls.Add(pictureBoxTutorial);
            panelTutorial.Location = new Point(60, 30);
            panelTutorial.Name = "panelTutorial";
            panelTutorial.Size = new Size(635, 430);
            panelTutorial.TabIndex = 14;
            panelTutorial.Visible = false;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = Properties.Resources.back;
            btnBack.BackgroundImageLayout = ImageLayout.Zoom;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBack.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(5, 180);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(55, 60);
            btnBack.TabIndex = 2;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.Transparent;
            btnNext.BackgroundImage = Properties.Resources.next;
            btnNext.BackgroundImageLayout = ImageLayout.Zoom;
            btnNext.Cursor = Cursors.Hand;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Location = new Point(575, 180);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(55, 60);
            btnNext.TabIndex = 1;
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // pictureBoxTutorial
            // 
            pictureBoxTutorial.BackColor = Color.Transparent;
            pictureBoxTutorial.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxTutorial.Location = new Point(65, 0);
            pictureBoxTutorial.Name = "pictureBoxTutorial";
            pictureBoxTutorial.Size = new Size(505, 430);
            pictureBoxTutorial.TabIndex = 0;
            pictureBoxTutorial.TabStop = false;
            // 
            // MAIN
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(755, 491);
            Controls.Add(panelTutorial);
            Controls.Add(panelOverlay);
            Controls.Add(btnShop);
            Controls.Add(pictureBox2);
            Controls.Add(btnExit);
            Controls.Add(btnEndless);
            Controls.Add(pictureBox1);
            Controls.Add(btnplay);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MAIN";
            StartPosition = FormStartPosition.CenterScreen;
            Load += KNAK_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelTutorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTutorial).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Button btnplay;
        private PictureBox pictureBox2;
        private Button btnEndless;
        private Button btnExit;
        private Button btnShop;
        private Panel panelOverlay;
        private Panel panelTutorial;
        private PictureBox pictureBoxTutorial;
        private Button btnNext;
        private Button btnBack;
    }
}