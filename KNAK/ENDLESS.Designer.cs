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
            pbTray1 = new PictureBox();
            pbTray2 = new PictureBox();
            pbTray3 = new PictureBox();
            pbPlate2 = new PictureBox();
            pbPlate3 = new PictureBox();
            pbPlate4 = new PictureBox();
            pbStove = new PictureBox();
            pbRawEgg = new PictureBox();
            pbEggOnPan = new PictureBox();
            timerEgg = new System.Windows.Forms.Timer(components);
            pbEggProgress = new ProgressBar();
            lblEggStatus = new Label();
            pbRawTapa = new PictureBox();
            pbTapaOnPan = new PictureBox();
            timerTapa = new System.Windows.Forms.Timer(components);
            pbTapaProgress = new ProgressBar();
            lblTapaStatus = new Label();
            pbParesPot = new PictureBox();
            lblParesStatus = new Label();
            pbParesProgress = new ProgressBar();
            timerPares = new System.Windows.Forms.Timer(components);
            pbParesBowl1 = new PictureBox();
            pbParesBowl2 = new PictureBox();
            pbParesBowl3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRiceCooker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbStove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRawEgg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbEggOnPan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRawTapa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTapaOnPan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbParesPot).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl3).BeginInit();
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
            lblRiceStatus.Location = new Point(14, 399);
            lblRiceStatus.Name = "lblRiceStatus";
            lblRiceStatus.Size = new Size(99, 17);
            lblRiceStatus.TabIndex = 8;
            lblRiceStatus.Text = "Rice Cooker";
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
            pbRiceProgress.Size = new Size(98, 20);
            pbRiceProgress.TabIndex = 9;
            // 
            // pbTray1
            // 
            pbTray1.BackColor = Color.Transparent;
            pbTray1.Image = (Image)resources.GetObject("pbTray1.Image");
            pbTray1.Location = new Point(35, 209);
            pbTray1.Name = "pbTray1";
            pbTray1.Size = new Size(176, 80);
            pbTray1.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray1.TabIndex = 10;
            pbTray1.TabStop = false;
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
            // pbPlate2
            // 
            pbPlate2.BackColor = Color.Silver;
            pbPlate2.Image = (Image)resources.GetObject("pbPlate2.Image");
            pbPlate2.Location = new Point(55, 221);
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
            pbPlate3.Location = new Point(243, 221);
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
            pbPlate4.Location = new Point(445, 221);
            pbPlate4.Name = "pbPlate4";
            pbPlate4.Size = new Size(55, 54);
            pbPlate4.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate4.TabIndex = 17;
            pbPlate4.TabStop = false;
            pbPlate4.Visible = false;
            // 
            // pbStove
            // 
            pbStove.BackColor = Color.Transparent;
            pbStove.Image = (Image)resources.GetObject("pbStove.Image");
            pbStove.Location = new Point(133, 285);
            pbStove.Name = "pbStove";
            pbStove.Size = new Size(241, 134);
            pbStove.SizeMode = PictureBoxSizeMode.Zoom;
            pbStove.TabIndex = 18;
            pbStove.TabStop = false;
            // 
            // pbRawEgg
            // 
            pbRawEgg.BackColor = Color.Transparent;
            pbRawEgg.Image = (Image)resources.GetObject("pbRawEgg.Image");
            pbRawEgg.Location = new Point(373, 322);
            pbRawEgg.Name = "pbRawEgg";
            pbRawEgg.Size = new Size(46, 44);
            pbRawEgg.SizeMode = PictureBoxSizeMode.Zoom;
            pbRawEgg.TabIndex = 19;
            pbRawEgg.TabStop = false;
            pbRawEgg.Click += pbRawEgg_Click;
            // 
            // pbEggOnPan
            // 
            pbEggOnPan.BackColor = Color.FromArgb(240, 191, 62);
            pbEggOnPan.Image = (Image)resources.GetObject("pbEggOnPan.Image");
            pbEggOnPan.Location = new Point(176, 309);
            pbEggOnPan.Name = "pbEggOnPan";
            pbEggOnPan.Size = new Size(44, 40);
            pbEggOnPan.SizeMode = PictureBoxSizeMode.Zoom;
            pbEggOnPan.TabIndex = 20;
            pbEggOnPan.TabStop = false;
            pbEggOnPan.Visible = false;
            pbEggOnPan.Click += pbEggOnPan_Click;
            // 
            // timerEgg
            // 
            timerEgg.Interval = 1000;
            timerEgg.Tick += timerEgg_Tick;
            // 
            // pbEggProgress
            // 
            pbEggProgress.Location = new Point(151, 418);
            pbEggProgress.Name = "pbEggProgress";
            pbEggProgress.Size = new Size(96, 20);
            pbEggProgress.TabIndex = 21;
            pbEggProgress.Visible = false;
            // 
            // lblEggStatus
            // 
            lblEggStatus.AutoSize = true;
            lblEggStatus.BackColor = Color.Transparent;
            lblEggStatus.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEggStatus.ForeColor = SystemColors.HighlightText;
            lblEggStatus.Location = new Point(153, 398);
            lblEggStatus.Name = "lblEggStatus";
            lblEggStatus.Size = new Size(78, 17);
            lblEggStatus.TabIndex = 22;
            lblEggStatus.Text = "Fried Egg";
            // 
            // pbRawTapa
            // 
            pbRawTapa.BackColor = Color.Transparent;
            pbRawTapa.Image = (Image)resources.GetObject("pbRawTapa.Image");
            pbRawTapa.Location = new Point(367, 372);
            pbRawTapa.Name = "pbRawTapa";
            pbRawTapa.Size = new Size(59, 43);
            pbRawTapa.SizeMode = PictureBoxSizeMode.Zoom;
            pbRawTapa.TabIndex = 23;
            pbRawTapa.TabStop = false;
            pbRawTapa.Click += pbRawTapa_Click;
            // 
            // pbTapaOnPan
            // 
            pbTapaOnPan.BackColor = Color.FromArgb(240, 191, 62);
            pbTapaOnPan.Image = (Image)resources.GetObject("pbTapaOnPan.Image");
            pbTapaOnPan.Location = new Point(278, 309);
            pbTapaOnPan.Name = "pbTapaOnPan";
            pbTapaOnPan.Size = new Size(58, 40);
            pbTapaOnPan.SizeMode = PictureBoxSizeMode.Zoom;
            pbTapaOnPan.TabIndex = 24;
            pbTapaOnPan.TabStop = false;
            pbTapaOnPan.Visible = false;
            pbTapaOnPan.Click += pbTapaOnPan_Click;
            // 
            // timerTapa
            // 
            timerTapa.Interval = 1000;
            timerTapa.Tick += timerTapa_Tick;
            // 
            // pbTapaProgress
            // 
            pbTapaProgress.Location = new Point(253, 418);
            pbTapaProgress.Name = "pbTapaProgress";
            pbTapaProgress.Size = new Size(96, 20);
            pbTapaProgress.TabIndex = 25;
            pbTapaProgress.Visible = false;
            // 
            // lblTapaStatus
            // 
            lblTapaStatus.AutoSize = true;
            lblTapaStatus.BackColor = Color.Transparent;
            lblTapaStatus.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTapaStatus.ForeColor = SystemColors.HighlightText;
            lblTapaStatus.Location = new Point(258, 398);
            lblTapaStatus.Name = "lblTapaStatus";
            lblTapaStatus.Size = new Size(44, 17);
            lblTapaStatus.TabIndex = 26;
            lblTapaStatus.Text = "Tapa";
            // 
            // pbParesPot
            // 
            pbParesPot.BackColor = Color.Transparent;
            pbParesPot.Image = (Image)resources.GetObject("pbParesPot.Image");
            pbParesPot.Location = new Point(425, 288);
            pbParesPot.Name = "pbParesPot";
            pbParesPot.Size = new Size(118, 131);
            pbParesPot.SizeMode = PictureBoxSizeMode.Zoom;
            pbParesPot.TabIndex = 27;
            pbParesPot.TabStop = false;
            pbParesPot.Click += pbParesPot_Click;
            // 
            // lblParesStatus
            // 
            lblParesStatus.AutoSize = true;
            lblParesStatus.BackColor = Color.Transparent;
            lblParesStatus.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblParesStatus.ForeColor = SystemColors.HighlightText;
            lblParesStatus.Location = new Point(457, 402);
            lblParesStatus.Name = "lblParesStatus";
            lblParesStatus.Size = new Size(51, 17);
            lblParesStatus.TabIndex = 28;
            lblParesStatus.Text = "Pares";
            // 
            // pbParesProgress
            // 
            pbParesProgress.Location = new Point(435, 418);
            pbParesProgress.Name = "pbParesProgress";
            pbParesProgress.Size = new Size(96, 20);
            pbParesProgress.TabIndex = 29;
            pbParesProgress.Visible = false;
            // 
            // timerPares
            // 
            timerPares.Interval = 1000;
            timerPares.Tick += timerPares_Tick;
            // 
            // pbParesBowl1
            // 
            pbParesBowl1.BackColor = Color.DarkGray;
            pbParesBowl1.Image = (Image)resources.GetObject("pbParesBowl1.Image");
            pbParesBowl1.Location = new Point(116, 221);
            pbParesBowl1.Name = "pbParesBowl1";
            pbParesBowl1.Size = new Size(55, 54);
            pbParesBowl1.SizeMode = PictureBoxSizeMode.Zoom;
            pbParesBowl1.TabIndex = 30;
            pbParesBowl1.TabStop = false;
            pbParesBowl1.Visible = false;
            // 
            // pbParesBowl2
            // 
            pbParesBowl2.BackColor = Color.DarkGray;
            pbParesBowl2.Image = (Image)resources.GetObject("pbParesBowl2.Image");
            pbParesBowl2.Location = new Point(304, 221);
            pbParesBowl2.Name = "pbParesBowl2";
            pbParesBowl2.Size = new Size(55, 54);
            pbParesBowl2.SizeMode = PictureBoxSizeMode.Zoom;
            pbParesBowl2.TabIndex = 31;
            pbParesBowl2.TabStop = false;
            pbParesBowl2.Visible = false;
            // 
            // pbParesBowl3
            // 
            pbParesBowl3.BackColor = Color.DarkGray;
            pbParesBowl3.Image = (Image)resources.GetObject("pbParesBowl3.Image");
            pbParesBowl3.Location = new Point(506, 221);
            pbParesBowl3.Name = "pbParesBowl3";
            pbParesBowl3.Size = new Size(55, 54);
            pbParesBowl3.SizeMode = PictureBoxSizeMode.Zoom;
            pbParesBowl3.TabIndex = 32;
            pbParesBowl3.TabStop = false;
            pbParesBowl3.Visible = false;
            // 
            // ENDLESS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(pbParesBowl3);
            Controls.Add(pbParesBowl2);
            Controls.Add(pbParesBowl1);
            Controls.Add(pbParesProgress);
            Controls.Add(lblParesStatus);
            Controls.Add(pbParesPot);
            Controls.Add(lblTapaStatus);
            Controls.Add(pbTapaProgress);
            Controls.Add(pbTapaOnPan);
            Controls.Add(pbRawTapa);
            Controls.Add(lblEggStatus);
            Controls.Add(pbEggProgress);
            Controls.Add(pbEggOnPan);
            Controls.Add(pbRawEgg);
            Controls.Add(lblRiceStatus);
            Controls.Add(pbStove);
            Controls.Add(pbPlate4);
            Controls.Add(pbPlate3);
            Controls.Add(pbPlate2);
            Controls.Add(pbTray3);
            Controls.Add(pbTray2);
            Controls.Add(pbTray1);
            Controls.Add(pbRiceProgress);
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
            ((System.ComponentModel.ISupportInitialize)pbTray1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTray3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbStove).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbRawEgg).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbEggOnPan).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbRawTapa).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbTapaOnPan).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbParesPot).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbParesBowl3).EndInit();
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
        private PictureBox pbTray1;
        private PictureBox pbTray2;
        private PictureBox pbTray3;
        private PictureBox pbPlate2;
        private PictureBox pbPlate3;
        private PictureBox pbPlate4;
        private PictureBox pbStove;
        private PictureBox pbRawEgg;
        private PictureBox pbEggOnPan;
        private System.Windows.Forms.Timer timerEgg;
        private ProgressBar pbEggProgress;
        private Label lblEggStatus;
        private PictureBox pbRawTapa;
        private PictureBox pbTapaOnPan;
        private System.Windows.Forms.Timer timerTapa;
        private ProgressBar pbTapaProgress;
        private Label lblTapaStatus;
        private PictureBox pbParesPot;
        private Label lblParesStatus;
        private ProgressBar pbParesProgress;
        private System.Windows.Forms.Timer timerPares;
        private PictureBox pbParesBowl1;
        private PictureBox pbParesBowl2;
        private PictureBox pbParesBowl3;
    }
}