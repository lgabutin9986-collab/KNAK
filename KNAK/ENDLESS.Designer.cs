namespace KNAK
{
    partial class ENDLESS
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ENDLESS));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
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
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            rawegg = new PictureBox();
            coatedegg = new PictureBox();
            basogulaman1 = new PictureBox();
            basogulaman2 = new PictureBox();
            basogulaman3 = new PictureBox();
            kwek1 = new PictureBox();
            kwek2 = new PictureBox();

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
            pbCircle1 = new PictureBox();
            pbCircle2 = new PictureBox();
            pbCircle3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbRiceCooker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbTray3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlate3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rawegg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)coatedegg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kwek1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kwek2).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)pbCircle1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCircle2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCircle3).BeginInit();
            SuspendLayout();

            // pictureBox1
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(84, 56);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;

            // pictureBox2
            pictureBox2.BackColor = Color.FromArgb(128, 64, 0);
            pictureBox2.Location = new Point(93, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(131, 49);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;

            // label1
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(128, 64, 64);
            label1.Font = new Font("Cooper Black", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(93, 14);
            label1.Name = "label1";
            label1.Size = new Size(84, 17);
            label1.TabIndex = 2;
            label1.Text = "TIME ELAPSED:";

            // lblTimeElapsed
            lblTimeElapsed.AutoSize = true;
            lblTimeElapsed.BackColor = Color.FromArgb(128, 64, 0);
            lblTimeElapsed.Font = new Font("Cooper Black", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTimeElapsed.ForeColor = SystemColors.ControlLightLight;
            lblTimeElapsed.Location = new Point(103, 36);
            lblTimeElapsed.Name = "lblTimeElapsed";
            lblTimeElapsed.Size = new Size(0, 17);
            lblTimeElapsed.TabIndex = 3;
            lblTimeElapsed.Click += label2_Click;

            // timer1
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;

            label1.Text = "STRIKES:";
            // 
            // pictureBox3
            pictureBox3.BackColor = Color.FromArgb(128, 64, 0);
            pictureBox3.Location = new Point(638, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(159, 49);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;

            // lblHighscore
            lblHighscore.AutoSize = true;
            lblHighscore.BackColor = Color.FromArgb(128, 64, 64);
            lblHighscore.Font = new Font("Cooper Black", 7.20000029F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblHighscore.ForeColor = SystemColors.ControlLightLight;
            lblHighscore.Location = new Point(638, 14);
            lblHighscore.Name = "lblHighscore";
            lblHighscore.Size = new Size(95, 15);
            lblHighscore.TabIndex = 5;
            lblHighscore.Text = "HIGHSCORE: ";

            // lblCurrentScore
            lblCurrentScore.AutoSize = true;
            lblCurrentScore.BackColor = Color.FromArgb(128, 64, 64);
            lblCurrentScore.Font = new Font("Cooper Black", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentScore.ForeColor = SystemColors.ControlLightLight;
            lblCurrentScore.Location = new Point(654, 35);
            lblCurrentScore.Name = "lblCurrentScore";
            lblCurrentScore.Size = new Size(0, 15);
            lblCurrentScore.TabIndex = 6;

            // pbRiceCooker
            pbRiceCooker.BackColor = Color.Transparent;
            pbRiceCooker.Image = (Image)resources.GetObject("pbRiceCooker.Image");
            pbRiceCooker.Location = new Point(1, 277);
            pbRiceCooker.Name = "pbRiceCooker";
            pbRiceCooker.Size = new Size(139, 126);
            pbRiceCooker.SizeMode = PictureBoxSizeMode.Zoom;
            pbRiceCooker.TabIndex = 7;
            pbRiceCooker.TabStop = false;
            pbRiceCooker.Click += pbRiceCooker_Click;

            // lblRiceStatus
            lblRiceStatus.AutoSize = true;
            lblRiceStatus.BackColor = Color.Transparent;
            lblRiceStatus.Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRiceStatus.ForeColor = SystemColors.HighlightText;
            lblRiceStatus.Location = new Point(14, 399);
            lblRiceStatus.Name = "lblRiceStatus";
            lblRiceStatus.Size = new Size(99, 17);
            lblRiceStatus.TabIndex = 8;
            lblRiceStatus.Text = "Magluto";

            lblRiceStatus.Text = "Rice Cooker";
            // 
            // timerRice
            timerRice.Interval = 200;
            timerRice.Tick += timerRice_Tick;

            // pbRiceProgress
            pbRiceProgress.Location = new Point(12, 418);
            pbRiceProgress.Name = "pbRiceProgress";
            pbRiceProgress.Size = new Size(98, 20);
            pbRiceProgress.TabIndex = 9;

            // pbTray
            pbTray.BackColor = Color.Transparent;
            pbTray.Image = (Image)resources.GetObject("pbTray.Image");
            pbTray.Location = new Point(35, 209);
            pbTray.Name = "pbTray";
            pbTray.Size = new Size(176, 80);
            pbTray.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray.TabIndex = 10;
            pbTray.TabStop = false;

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
            pbTray2.BackColor = Color.Transparent;
            pbTray2.Image = (Image)resources.GetObject("pbTray2.Image");
            pbTray2.Location = new Point(222, 209);
            pbTray2.Name = "pbTray2";
            pbTray2.Size = new Size(176, 80);
            pbTray2.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray2.TabIndex = 11;
            pbTray2.TabStop = false;

            // pbTray3
            pbTray3.BackColor = Color.Transparent;
            pbTray3.Image = (Image)resources.GetObject("pbTray3.Image");
            pbTray3.Location = new Point(412, 209);
            pbTray3.Name = "pbTray3";
            pbTray3.Size = new Size(176, 80);
            pbTray3.SizeMode = PictureBoxSizeMode.Zoom;
            pbTray3.TabIndex = 12;
            pbTray3.TabStop = false;

            // pbPlate1
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
            pbPlate2.BackColor = Color.Silver;
            pbPlate2.Image = (Image)resources.GetObject("pbPlate2.Image");
            pbPlate2.Location = new Point(55, 221);
            pbPlate2.Name = "pbPlate2";
            pbPlate2.Size = new Size(55, 54);
            pbPlate2.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate2.TabIndex = 15;
            pbPlate2.TabStop = false;
            pbPlate2.Visible = false;

            // pbPlate3
            pbPlate3.BackColor = Color.Silver;
            pbPlate3.Image = (Image)resources.GetObject("pbPlate3.Image");
            pbPlate3.Location = new Point(439, 221);
            pbPlate3.Location = new Point(243, 221);
            pbPlate3.Name = "pbPlate3";
            pbPlate3.Size = new Size(55, 54);
            pbPlate3.SizeMode = PictureBoxSizeMode.Zoom;
            pbPlate3.TabIndex = 16;
            pbPlate3.TabStop = false;
            pbPlate3.Visible = false;

            // pictureBox4
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(123, 304);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(230, 134);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 18;
            pictureBox4.TabStop = false;

            // pictureBox5 (Dispenser)
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.Image = Properties.Resources.fullgulaman;
            pictureBox5.Location = new Point(680, 291);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(103, 143);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 19;
            pictureBox5.TabStop = false;
            pictureBox5.Click += pictureBox5_Click;

            // pictureBox6
            pictureBox6.BackColor = Color.Transparent;
            pictureBox6.Image = Properties.Resources.fryer;
            pictureBox6.Location = new Point(493, 304);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(139, 126);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 20;
            pictureBox6.TabStop = false;

            // rawegg
            rawegg.BackColor = Color.Transparent;
            rawegg.Image = Properties.Resources.rawegg;
            rawegg.Location = new Point(616, 318);
            rawegg.Name = "rawegg";
            rawegg.Size = new Size(58, 53);
            rawegg.SizeMode = PictureBoxSizeMode.Zoom;
            rawegg.TabIndex = 21;
            rawegg.TabStop = false;

            // coatedegg
            coatedegg.BackColor = Color.Transparent;
            coatedegg.Image = Properties.Resources.stiregg;
            coatedegg.Location = new Point(616, 372);
            coatedegg.Name = "coatedegg";
            coatedegg.Size = new Size(58, 53);
            coatedegg.SizeMode = PictureBoxSizeMode.Zoom;
            coatedegg.TabIndex = 22;
            coatedegg.TabStop = false;

            // basogulaman1
            basogulaman1.BackColor = Color.Transparent;
            basogulaman1.Image = Properties.Resources.gulaman_removebg_preview;
            basogulaman1.Location = new Point(668, 209);
            basogulaman1.Name = "basogulaman1";
            basogulaman1.Size = new Size(41, 81);
            basogulaman1.SizeMode = PictureBoxSizeMode.Zoom;
            basogulaman1.TabIndex = 25;
            basogulaman1.TabStop = false;
            basogulaman1.Visible = false;

            // basogulaman2
            basogulaman2.BackColor = Color.Transparent;
            basogulaman2.Image = Properties.Resources.gulaman_removebg_preview;
            basogulaman2.Location = new Point(709, 209);
            basogulaman2.Name = "basogulaman2";
            basogulaman2.Size = new Size(41, 81);
            basogulaman2.SizeMode = PictureBoxSizeMode.Zoom;
            basogulaman2.TabIndex = 24;
            basogulaman2.TabStop = false;
            basogulaman2.Visible = false;

            // basogulaman3
            basogulaman3.BackColor = Color.Transparent;
            basogulaman3.Image = Properties.Resources.gulaman_removebg_preview;
            basogulaman3.Location = new Point(747, 208);
            basogulaman3.Name = "basogulaman3";
            basogulaman3.Size = new Size(41, 81);
            basogulaman3.SizeMode = PictureBoxSizeMode.Zoom;
            basogulaman3.TabIndex = 23;
            basogulaman3.TabStop = false;
            basogulaman3.Visible = false;

            // kwek1
            kwek1.BackColor = Color.Transparent;
            kwek1.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
            kwek1.Location = new Point(594, 214);
            kwek1.Name = "kwek1";
            kwek1.Size = new Size(37, 75);
            kwek1.SizeMode = PictureBoxSizeMode.Zoom;
            kwek1.TabIndex = 26;
            kwek1.TabStop = false;

            // kwek2
            kwek2.BackColor = Color.Transparent;
            kwek2.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
            kwek2.Location = new Point(634, 214);
            kwek2.Name = "kwek2";
            kwek2.Size = new Size(37, 75);
            kwek2.SizeMode = PictureBoxSizeMode.Zoom;
            kwek2.TabIndex = 27;
            kwek2.TabStop = false;

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
            // pbCircle1
            // 
            pbCircle1.BackColor = Color.FromArgb(128, 64, 0);
            pbCircle1.Image = (Image)resources.GetObject("pbCircle1.Image");
            pbCircle1.Location = new Point(101, 29);
            pbCircle1.Name = "pbCircle1";
            pbCircle1.Size = new Size(38, 31);
            pbCircle1.SizeMode = PictureBoxSizeMode.Zoom;
            pbCircle1.TabIndex = 33;
            pbCircle1.TabStop = false;
            // 
            // pbCircle2
            // 
            pbCircle2.BackColor = Color.FromArgb(128, 64, 0);
            pbCircle2.Image = (Image)resources.GetObject("pbCircle2.Image");
            pbCircle2.Location = new Point(139, 29);
            pbCircle2.Name = "pbCircle2";
            pbCircle2.Size = new Size(38, 31);
            pbCircle2.SizeMode = PictureBoxSizeMode.Zoom;
            pbCircle2.TabIndex = 34;
            pbCircle2.TabStop = false;
            // 
            // pbCircle3
            // 
            pbCircle3.BackColor = Color.FromArgb(128, 64, 0);
            pbCircle3.Image = (Image)resources.GetObject("pbCircle3.Image");
            pbCircle3.Location = new Point(176, 29);
            pbCircle3.Name = "pbCircle3";
            pbCircle3.Size = new Size(38, 31);
            pbCircle3.SizeMode = PictureBoxSizeMode.Zoom;
            pbCircle3.TabIndex = 35;
            pbCircle3.TabStop = false;
            // 
            // ENDLESS
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(kwek2);
            Controls.Add(kwek1);
            Controls.Add(basogulaman3);
            Controls.Add(basogulaman2);
            Controls.Add(basogulaman1);
            Controls.Add(rawegg);
            Controls.Add(coatedegg);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pbCircle3);
            Controls.Add(pbCircle2);
            Controls.Add(pbCircle1);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)rawegg).EndInit();
            ((System.ComponentModel.ISupportInitialize)coatedegg).EndInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman1).EndInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman2).EndInit();
            ((System.ComponentModel.ISupportInitialize)basogulaman3).EndInit();
            ((System.ComponentModel.ISupportInitialize)kwek1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kwek2).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)pbCircle1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCircle2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCircle3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
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
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox rawegg;
        private PictureBox coatedegg;
        private PictureBox basogulaman1;
        private PictureBox basogulaman2;
        private PictureBox basogulaman3;
        private PictureBox kwek1;
        private PictureBox kwek2;
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
        private PictureBox pbCircle1;
        private PictureBox pbCircle2;
        private PictureBox pbCircle3;
    }
}