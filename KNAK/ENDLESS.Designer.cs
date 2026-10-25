namespace KNAK
{
    partial class ENDLESS
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ENDLESS));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            lblTimeElapsed = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox3 = new PictureBox();
            lblHighscore = new Label();
            lblCurrentScore = new Label();
            pbRiceCooker = new PictureBox();
            lblRiceStatus = new Label();
            timerRice = new System.Windows.Forms.Timer(components);
            pbRiceProgress = new ProgressBar();
            pbTray = new PictureBox();
            pbTray2 = new PictureBox();
            pbTray3 = new PictureBox();
            pbTray4 = new PictureBox();
            pbPlate1 = new PictureBox();
            pbPlate2 = new PictureBox();
            pbPlate3 = new PictureBox();
            pbPlate4 = new PictureBox();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRiceCooker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(84, 56);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(128, 64, 0);
            pictureBox2.Location = new Point(93, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(131, 49);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(128, 64, 64);
            label1.Font = new Font("Cooper Black", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(93, 14);
            label1.Name = "label1";
            label1.Size = new Size(131, 17);
            label1.TabIndex = 2;
            label1.Text = "TIME ELAPSED:";
            // 
            // lblTimeElapsed
            // 
            lblTimeElapsed.AutoSize = true;
            lblTimeElapsed.BackColor = Color.FromArgb(128, 64, 0);
            lblTimeElapsed.Font = new Font("Cooper Black", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTimeElapsed.ForeColor = SystemColors.ControlLightLight;
            lblTimeElapsed.Location = new Point(103, 36);
            lblTimeElapsed.Name = "lblTimeElapsed";
            lblTimeElapsed.Size = new Size(0, 17);
            lblTimeElapsed.TabIndex = 3;
            lblTimeElapsed.Click += label2_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(128, 64, 0);
            pictureBox3.Location = new Point(638, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(159, 49);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // lblHighscore
            // 
            lblHighscore.AutoSize = true;
            lblHighscore.BackColor = Color.FromArgb(128, 64, 64);
            lblHighscore.Font = new Font("Cooper Black", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHighscore.ForeColor = SystemColors.ControlLightLight;
            lblHighscore.Location = new Point(638, 14);
            lblHighscore.Name = "lblHighscore";
            lblHighscore.Size = new Size(95, 15);
            lblHighscore.TabIndex = 5;
            lblHighscore.Text = "HIGHSCORE: ";
            // 
            // lblCurrentScore
            // 
            lblCurrentScore.AutoSize = true;
            lblCurrentScore.BackColor = Color.FromArgb(128, 64, 64);
            lblCurrentScore.Font = new Font("Cooper Black", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentScore.ForeColor = SystemColors.ControlLightLight;
            lblCurrentScore.Location = new Point(654, 35);
            lblCurrentScore.Name = "lblCurrentScore";
            lblCurrentScore.Size = new Size(0, 15);
            lblCurrentScore.TabIndex = 6;
            // 
            // pbRiceCooker
            // 
            pbRiceCooker.BackColor = Color.Transparent;
            pbRiceCooker.Image = (Image)resources.GetObject("pbRiceCooker.Image");
            pbRiceCooker.Location = new Point(1, 277);
            pbRiceCooker.Name = "pbRiceCooker";
            pbRiceCooker.Size = new Size(139, 126);
            pbRiceCooker.SizeMode = PictureBoxSizeMode.Zoom;
            pbRiceCooker.TabIndex = 7;
            pbRiceCooker.TabStop = false;
            pbRiceCooker.Click += pbRiceCooker_Click;
            // 
            // lblRiceStatus
            // 
            lblRiceStatus.AutoSize = true;
            lblRiceStatus.BackColor = Color.Transparent;
            lblRiceStatus.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRiceStatus.ForeColor = SystemColors.HighlightText;
            lblRiceStatus.Location = new Point(40, 399);
            lblRiceStatus.Name = "lblRiceStatus";
            lblRiceStatus.Size = new Size(65, 17);
            lblRiceStatus.TabIndex = 8;
            lblRiceStatus.Text = "Magluto";
            // 
            // timerRice
            // 
            timerRice.Interval = 200;
            timerRice.Tick += timerRice_Tick;
            // 
            // pbRiceProgress
            // 
            pbRiceProgress.Location = new Point(12, 418);
            pbRiceProgress.Name = "pbRiceProgress";
            pbRiceProgress.Size = new Size(115, 20);
            pbRiceProgress.TabIndex = 9;
            // 
            // pbTray
            // 
            pbTray.BackColor = Color.Transparent;
            pbTray.Image = (Image)resources.GetObject("pbTray.Image");
            pbTray.Location = new Point(35, 209);
            pbTray.Name = "pbTray";
            pbTray.Size = new Size(176, 80);
            pbTray.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray.TabIndex = 10;
            pbTray.TabStop = false;
            // 
            // pbTray2
            // 
            pbTray2.BackColor = Color.Transparent;
            pbTray2.Image = (Image)resources.GetObject("pbTray2.Image");
            pbTray2.Location = new Point(222, 209);
            pbTray2.Name = "pbTray2";
            pbTray2.Size = new Size(176, 80);
            pbTray2.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray2.TabIndex = 11;
            pbTray2.TabStop = false;
            // 
            // pbTray3
            // 
            pbTray3.BackColor = Color.Transparent;
            pbTray3.Image = (Image)resources.GetObject("pbTray3.Image");
            pbTray3.Location = new Point(412, 209);
            pbTray3.Name = "pbTray3";
            pbTray3.Size = new Size(176, 80);
            pbTray3.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray3.TabIndex = 12;
            pbTray3.TabStop = false;
            // 
            // pbTray4
            // 
            pbTray4.BackColor = Color.Transparent;
            pbTray4.Image = (Image)resources.GetObject("pbTray4.Image");
            pbTray4.Location = new Point(594, 209);
            pbTray4.Name = "pbTray4";
            pbTray4.Size = new Size(176, 80);
            pbTray4.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray4.TabIndex = 13;
            pbTray4.TabStop = false;
            // 
            // pbPlate1
            // 
            pbPlate1.BackColor = Color.Silver;
            pbPlate1.Image = (Image)resources.GetObject("pbPlate1.Image");
            pbPlate1.Location = new Point(59, 221);
            pbPlate1.Name = "pbPlate1";
            pbPlate1.Size = new Size(55, 54);
            pbPlate1.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate1.TabIndex = 14;
            pbPlate1.TabStop = false;
            pbPlate1.Visible = false;
            // 
            // pbPlate2
            // 
            pbPlate2.BackColor = Color.Silver;
            pbPlate2.Image = (Image)resources.GetObject("pbPlate2.Image");
            pbPlate2.Location = new Point(247, 221);
            pbPlate2.Name = "pbPlate2";
            pbPlate2.Size = new Size(55, 54);
            pbPlate2.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate2.TabIndex = 15;
            pbPlate2.TabStop = false;
            pbPlate2.Visible = false;
            // 
            // pbPlate3
            // 
            pbPlate3.BackColor = Color.Silver;
            pbPlate3.Image = (Image)resources.GetObject("pbPlate3.Image");
            pbPlate3.Location = new Point(441, 221);
            pbPlate3.Name = "pbPlate3";
            pbPlate3.Size = new Size(55, 54);
            pbPlate3.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate3.TabIndex = 16;
            pbPlate3.TabStop = false;
            pbPlate3.Visible = false;
            // 
            // pbPlate4
            // 
            pbPlate4.BackColor = Color.Silver;
            pbPlate4.Image = (Image)resources.GetObject("pbPlate4.Image");
            pbPlate4.Location = new Point(627, 221);
            pbPlate4.Name = "pbPlate4";
            pbPlate4.Size = new Size(55, 54);
            pbPlate4.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate4.TabIndex = 17;
            pbPlate4.TabStop = false;
            pbPlate4.Visible = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(133, 285);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(241, 134);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 18;
            pictureBox4.TabStop = false;
            // 
            // ENDLESS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox4);
            Controls.Add(pbPlate4);
            Controls.Add(pbPlate3);
            Controls.Add(pbPlate2);
            Controls.Add(pbPlate1);
            Controls.Add(pbTray4);
            Controls.Add(pbTray3);
            Controls.Add(pbTray2);
            Controls.Add(pbTray);
            Controls.Add(pbRiceProgress);
            Controls.Add(lblRiceStatus);
            Controls.Add(pbRiceCooker);
            Controls.Add(lblCurrentScore);
            Controls.Add(lblHighscore);
            Controls.Add(pictureBox3);
            Controls.Add(lblTimeElapsed);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "ENDLESS";
            Text = "ENDLESS";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbRiceCooker).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Label lblTimeElapsed;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox3;
        private Label lblHighscore;
        private Label lblCurrentScore;
        private PictureBox pbRiceCooker;
        private Label lblRiceStatus;
        private System.Windows.Forms.Timer timerRice;
        private ProgressBar pbRiceProgress;
        private PictureBox pbTray;
        private PictureBox pbTray2;
        private PictureBox pbTray3;
        private PictureBox pbTray4;
        private PictureBox pbPlate1;
        private PictureBox pbPlate2;
        private PictureBox pbPlate3;
        private PictureBox pbPlate4;
        private PictureBox pictureBox4;
    }
}