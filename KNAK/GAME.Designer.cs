using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KNAK
{
    partial class GAME
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(GAME));
            picCustomer = new PictureBox();
            Walktimer = new System.Windows.Forms.Timer(components);
            lblStage = new Label();
            picFood = new PictureBox();
            pboxPares = new PictureBox();
            pboxTray = new PictureBox();
            pboxMeatBowl = new PictureBox();
            pboxBatterBowl = new PictureBox();
            pboxEggsBasket = new PictureBox();
            pboxFryer = new PictureBox();
            picBubbleChat = new PictureBox();
            pauseicon = new PictureBox();
            pausebox = new PictureBox();
            btnresume = new PictureBox();
            restart = new PictureBox();
            mainmenu = new PictureBox();
            shiftbox = new PictureBox();
            nextday1 = new PictureBox();
            showquota = new PictureBox();
            ((ISupportInitialize)picCustomer).BeginInit();
            ((ISupportInitialize)picFood).BeginInit();
            ((ISupportInitialize)pboxPares).BeginInit();
            ((ISupportInitialize)pboxTray).BeginInit();
            ((ISupportInitialize)pboxMeatBowl).BeginInit();
            ((ISupportInitialize)pboxBatterBowl).BeginInit();
            ((ISupportInitialize)pboxEggsBasket).BeginInit();
            ((ISupportInitialize)pboxFryer).BeginInit();
            ((ISupportInitialize)picBubbleChat).BeginInit();
            ((ISupportInitialize)pauseicon).BeginInit();
            ((ISupportInitialize)pausebox).BeginInit();
            ((ISupportInitialize)btnresume).BeginInit();
            ((ISupportInitialize)restart).BeginInit();
            ((ISupportInitialize)mainmenu).BeginInit();
            ((ISupportInitialize)shiftbox).BeginInit();
            ((ISupportInitialize)nextday1).BeginInit();
            ((ISupportInitialize)showquota).BeginInit();
            SuspendLayout();
            // 
            // picCustomer
            // 
            picCustomer.BackColor = Color.Transparent;
            picCustomer.Cursor = Cursors.Hand;
            picCustomer.Image = Properties.Resources.worker_normal_face_removebg_preview;
            picCustomer.Location = new Point(736, 16);
            picCustomer.Name = "picCustomer";
            picCustomer.Size = new Size(170, 255);
            picCustomer.SizeMode = PictureBoxSizeMode.Zoom;
            picCustomer.TabIndex = 0;
            picCustomer.TabStop = false;
            picCustomer.Click += ConstructionWorker_Click;
            // 
            // Walktimer
            // 
            Walktimer.Interval = 16;
            // 
            // lblStage
            // 
            lblStage.BackColor = Color.FromArgb(210, 20, 24, 33);
            lblStage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStage.ForeColor = Color.FromArgb(255, 215, 0);
            lblStage.Location = new Point(404, 0);
            lblStage.Name = "lblStage";
            lblStage.Size = new Size(200, 32);
            lblStage.TabIndex = 3;
            lblStage.Text = "STAGE 1";
            lblStage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picFood
            // 
            picFood.BackColor = Color.Transparent;
            picFood.BackgroundImageLayout = ImageLayout.None;
            picFood.Cursor = Cursors.Hand;
            picFood.Image = Properties.Resources.tapsilog;
            picFood.Location = new Point(563, 45);
            picFood.Name = "picFood";
            picFood.Size = new Size(41, 42);
            picFood.SizeMode = PictureBoxSizeMode.StretchImage;
            picFood.TabIndex = 0;
            picFood.TabStop = false;
            picFood.Visible = false;
            // 
            // pboxPares
            // 
            pboxPares.BackColor = Color.Transparent;
            pboxPares.Cursor = Cursors.Hand;
            pboxPares.Image = Properties.Resources.pares_pot_trimmed;
            pboxPares.Location = new Point(634, 250);
            pboxPares.Name = "pboxPares";
            pboxPares.Size = new Size(142, 113);
            pboxPares.SizeMode = PictureBoxSizeMode.Zoom;
            pboxPares.TabIndex = 7;
            pboxPares.TabStop = false;
            // 
            // pboxTray
            // 
            pboxTray.BackColor = Color.Transparent;
            pboxTray.Cursor = Cursors.Hand;
            pboxTray.Image = Properties.Resources.tray_trimmed;
            pboxTray.Location = new Point(485, 310);
            pboxTray.Name = "pboxTray";
            pboxTray.Size = new Size(130, 42);
            pboxTray.SizeMode = PictureBoxSizeMode.Zoom;
            pboxTray.TabIndex = 11;
            pboxTray.TabStop = false;
            // 
            // pboxMeatBowl
            // 
            pboxMeatBowl.BackColor = Color.Transparent;
            pboxMeatBowl.Cursor = Cursors.Hand;
            pboxMeatBowl.Image = Properties.Resources.beef_pares_meat;
            pboxMeatBowl.Location = new Point(376, 276);
            pboxMeatBowl.Name = "pboxMeatBowl";
            pboxMeatBowl.Size = new Size(92, 78);
            pboxMeatBowl.SizeMode = PictureBoxSizeMode.Zoom;
            pboxMeatBowl.TabIndex = 10;
            pboxMeatBowl.TabStop = false;
            // 
            // pboxBatterBowl
            // 
            pboxBatterBowl.BackColor = Color.Transparent;
            pboxBatterBowl.Cursor = Cursors.Hand;
            pboxBatterBowl.Image = Properties.Resources.kwekkwek_batter_bowl;
            pboxBatterBowl.Location = new Point(272, 274);
            pboxBatterBowl.Name = "pboxBatterBowl";
            pboxBatterBowl.Size = new Size(92, 80);
            pboxBatterBowl.SizeMode = PictureBoxSizeMode.Zoom;
            pboxBatterBowl.TabIndex = 9;
            pboxBatterBowl.TabStop = false;
            // 
            // pboxEggsBasket
            // 
            pboxEggsBasket.BackColor = Color.Transparent;
            pboxEggsBasket.Cursor = Cursors.Hand;
            pboxEggsBasket.Image = Properties.Resources.raw_eggs_basket;
            pboxEggsBasket.Location = new Point(176, 266);
            pboxEggsBasket.Name = "pboxEggsBasket";
            pboxEggsBasket.Size = new Size(84, 88);
            pboxEggsBasket.SizeMode = PictureBoxSizeMode.Zoom;
            pboxEggsBasket.TabIndex = 8;
            pboxEggsBasket.TabStop = false;
            // 
            // pboxFryer
            // 
            pboxFryer.BackColor = Color.Transparent;
            pboxFryer.Cursor = Cursors.Hand;
            pboxFryer.Image = Properties.Resources.deep_fryer_trimmed;
            pboxFryer.Location = new Point(32, 218);
            pboxFryer.Name = "pboxFryer";
            pboxFryer.Size = new Size(130, 145);
            pboxFryer.SizeMode = PictureBoxSizeMode.Zoom;
            pboxFryer.TabIndex = 6;
            pboxFryer.TabStop = false;
            // 
            // picBubbleChat
            // 
            picBubbleChat.BackColor = Color.Transparent;
            picBubbleChat.Image = (Image)resources.GetObject("picBubbleChat.Image");
            picBubbleChat.Location = new Point(547, 35);
            picBubbleChat.Name = "picBubbleChat";
            picBubbleChat.Size = new Size(125, 102);
            picBubbleChat.SizeMode = PictureBoxSizeMode.Zoom;
            picBubbleChat.TabIndex = 12;
            picBubbleChat.TabStop = false;
            picBubbleChat.Visible = false;
            // 
            // pauseicon
            // 
            pauseicon.BackColor = Color.Transparent;
            pauseicon.Image = (Image)resources.GetObject("pauseicon.Image");
            pauseicon.Location = new Point(916, 29);
            pauseicon.Name = "pauseicon";
            pauseicon.Size = new Size(53, 52);
            pauseicon.SizeMode = PictureBoxSizeMode.Zoom;
            pauseicon.TabIndex = 13;
            pauseicon.TabStop = false;
            // 
            // pausebox
            // 
            pausebox.BackColor = Color.Transparent;
            pausebox.Image = (Image)resources.GetObject("pausebox.Image");
            pausebox.Location = new Point(370, 84);
            pausebox.Name = "pausebox";
            pausebox.Size = new Size(347, 316);
            pausebox.SizeMode = PictureBoxSizeMode.Zoom;
            pausebox.TabIndex = 14;
            pausebox.TabStop = false;
            // 
            // btnresume
            // 
            btnresume.BackColor = Color.Sienna;
            btnresume.Image = (Image)resources.GetObject("btnresume.Image");
            btnresume.Location = new Point(450, 180);
            btnresume.Name = "btnresume";
            btnresume.Size = new Size(197, 62);
            btnresume.SizeMode = PictureBoxSizeMode.Zoom;
            btnresume.TabIndex = 15;
            btnresume.TabStop = false;
            // 
            // restart
            // 
            restart.BackColor = Color.Transparent;
            restart.Image = (Image)resources.GetObject("restart.Image");
            restart.Location = new Point(449, 251);
            restart.Name = "restart";
            restart.Size = new Size(197, 60);
            restart.SizeMode = PictureBoxSizeMode.Zoom;
            restart.TabIndex = 16;
            restart.TabStop = false;
            // 
            // mainmenu
            // 
            mainmenu.BackColor = Color.Transparent;
            mainmenu.Image = (Image)resources.GetObject("mainmenu.Image");
            mainmenu.Location = new Point(450, 320);
            mainmenu.Name = "mainmenu";
            mainmenu.Size = new Size(197, 61);
            mainmenu.SizeMode = PictureBoxSizeMode.Zoom;
            mainmenu.TabIndex = 17;
            mainmenu.TabStop = false;
            // 
            // shiftbox
            // 
            shiftbox.BackColor = Color.Transparent;
            shiftbox.Image = (Image)resources.GetObject("shiftbox.Image");
            shiftbox.Location = new Point(327, 68);
            shiftbox.Name = "shiftbox";
            shiftbox.Size = new Size(413, 351);
            shiftbox.SizeMode = PictureBoxSizeMode.Zoom;
            shiftbox.TabIndex = 18;
            shiftbox.TabStop = false;
            // 
            // nextday1
            // 
            nextday1.BackColor = Color.Transparent;
            nextday1.Image = (Image)resources.GetObject("nextday1.Image");
            nextday1.Location = new Point(417, 315);
            nextday1.Name = "nextday1";
            nextday1.Size = new Size(236, 84);
            nextday1.SizeMode = PictureBoxSizeMode.Zoom;
            nextday1.TabIndex = 19;
            nextday1.TabStop = false;
            // 
            // showquota
            // 
            showquota.BackColor = Color.SaddleBrown;
            showquota.Location = new Point(518, 241);
            showquota.Name = "showquota";
            showquota.Size = new Size(125, 62);
            showquota.TabIndex = 20;
            showquota.TabStop = false;
            // 
            // GAME
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Stage1bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1024, 576);
            Controls.Add(showquota);
            Controls.Add(nextday1);
            Controls.Add(shiftbox);
            Controls.Add(mainmenu);
            Controls.Add(restart);
            Controls.Add(btnresume);
            Controls.Add(pausebox);
            Controls.Add(pauseicon);
            Controls.Add(picFood);
            Controls.Add(lblStage);
            Controls.Add(picCustomer);
            Controls.Add(pboxFryer);
            Controls.Add(pboxEggsBasket);
            Controls.Add(pboxBatterBowl);
            Controls.Add(pboxMeatBowl);
            Controls.Add(pboxTray);
            Controls.Add(pboxPares);
            Controls.Add(picBubbleChat);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GAME";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KNAK - Stage 1";
            Load += GAME_Load;
            ((ISupportInitialize)picCustomer).EndInit();
            ((ISupportInitialize)picFood).EndInit();
            ((ISupportInitialize)pboxPares).EndInit();
            ((ISupportInitialize)pboxTray).EndInit();
            ((ISupportInitialize)pboxMeatBowl).EndInit();
            ((ISupportInitialize)pboxBatterBowl).EndInit();
            ((ISupportInitialize)pboxEggsBasket).EndInit();
            ((ISupportInitialize)pboxFryer).EndInit();
            ((ISupportInitialize)picBubbleChat).EndInit();
            ((ISupportInitialize)pauseicon).EndInit();
            ((ISupportInitialize)pausebox).EndInit();
            ((ISupportInitialize)btnresume).EndInit();
            ((ISupportInitialize)restart).EndInit();
            ((ISupportInitialize)mainmenu).EndInit();
            ((ISupportInitialize)shiftbox).EndInit();
            ((ISupportInitialize)nextday1).EndInit();
            ((ISupportInitialize)showquota).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picCustomer;
        private System.Windows.Forms.Timer Walktimer;
        private Label lblStage;
        private PictureBox picFood;
        private PictureBox pboxPares;
        private PictureBox pboxTray;
        private PictureBox pboxMeatBowl;
        private PictureBox pboxBatterBowl;
        private PictureBox pboxEggsBasket;
        private PictureBox pboxFryer;
        private PictureBox picBubbleChat;
        private PictureBox pauseicon;
        private PictureBox pausebox;
        private PictureBox btnresume;
        private PictureBox restart;
        private PictureBox mainmenu;
        private PictureBox shiftbox;
        private PictureBox nextday1;
        private PictureBox showquota;
    }
}