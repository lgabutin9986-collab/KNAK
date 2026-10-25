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
            picCustomer = new PictureBox();
            Walktimer = new System.Windows.Forms.Timer(components);
            btnBack = new Button();
            btnRestart = new Button();
            lblStage = new Label();
            pnlBubbleChat = new Panel();
            lblQuantity = new Label();
            picFood = new PictureBox();
            pboxFryer = new PictureBox();
            pboxPares = new PictureBox();
            pboxEggsBasket = new PictureBox();
            pboxBatterBowl = new PictureBox();
            pboxMeatBowl = new PictureBox();
            pboxTray = new PictureBox();
            ((ISupportInitialize)picCustomer).BeginInit();
            pnlBubbleChat.SuspendLayout();
            ((ISupportInitialize)picFood).BeginInit();
            ((ISupportInitialize)pboxFryer).BeginInit();
            ((ISupportInitialize)pboxPares).BeginInit();
            ((ISupportInitialize)pboxEggsBasket).BeginInit();
            ((ISupportInitialize)pboxBatterBowl).BeginInit();
            ((ISupportInitialize)pboxMeatBowl).BeginInit();
            ((ISupportInitialize)pboxTray).BeginInit();
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
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(200, 30, 35, 48);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(16, 16);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(110, 38);
            btnBack.TabIndex = 1;
            btnBack.Text = "← Main Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnRestart
            // 
            btnRestart.BackColor = Color.FromArgb(200, 30, 35, 48);
            btnRestart.Cursor = Cursors.Hand;
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRestart.ForeColor = Color.White;
            btnRestart.Location = new Point(136, 16);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(100, 38);
            btnRestart.TabIndex = 2;
            btnRestart.Text = "↻ Restart";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
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
            // pnlBubbleChat
            // 
            pnlBubbleChat.BackColor = Color.Transparent;
            pnlBubbleChat.BackgroundImage = Properties.Resources.speech_bubble_;
            pnlBubbleChat.BackgroundImageLayout = ImageLayout.Stretch;
            pnlBubbleChat.Controls.Add(lblQuantity);
            pnlBubbleChat.Controls.Add(picFood);
            pnlBubbleChat.Location = new Point(455, 16);
            pnlBubbleChat.Name = "pnlBubbleChat";
            pnlBubbleChat.Size = new Size(321, 145);
            pnlBubbleChat.TabIndex = 4;
            pnlBubbleChat.Visible = false;
            pnlBubbleChat.Paint += pnlDialogue_Paint;
            // 
            // lblQuantity
            // 
            lblQuantity.BackColor = Color.Transparent;
            lblQuantity.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblQuantity.ForeColor = Color.FromArgb(180, 83, 9);
            lblQuantity.Location = new Point(162, 48);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(48, 30);
            lblQuantity.TabIndex = 5;
            lblQuantity.Text = "× 1";
            lblQuantity.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picFood
            // 
            picFood.BackColor = Color.Transparent;
            picFood.BackgroundImage = Properties.Resources.ready_to_serve_kwekkwek;
            picFood.BackgroundImageLayout = ImageLayout.Zoom;
            picFood.Cursor = Cursors.Hand;
            picFood.Location = new Point(106, 38);
            picFood.Name = "picFood";
            picFood.Size = new Size(54, 52);
            picFood.SizeMode = PictureBoxSizeMode.Zoom;
            picFood.TabIndex = 0;
            picFood.TabStop = false;
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
            // GAME
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1024, 576);
            Controls.Add(lblStage);
            Controls.Add(btnRestart);
            Controls.Add(btnBack);
            Controls.Add(picCustomer);
            Controls.Add(pnlBubbleChat);
            Controls.Add(pboxFryer);
            Controls.Add(pboxEggsBasket);
            Controls.Add(pboxBatterBowl);
            Controls.Add(pboxMeatBowl);
            Controls.Add(pboxTray);
            Controls.Add(pboxPares);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GAME";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KNAK - Stage 1";
            Load += GAME_Load;
            ((ISupportInitialize)picCustomer).EndInit();
            pnlBubbleChat.ResumeLayout(false);
            ((ISupportInitialize)picFood).EndInit();
            ((ISupportInitialize)pboxFryer).EndInit();
            ((ISupportInitialize)pboxPares).EndInit();
            ((ISupportInitialize)pboxEggsBasket).EndInit();
            ((ISupportInitialize)pboxBatterBowl).EndInit();
            ((ISupportInitialize)pboxMeatBowl).EndInit();
            ((ISupportInitialize)pboxTray).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picCustomer;
        private System.Windows.Forms.Timer Walktimer;
        private Button btnBack;
        private Button btnRestart;
        private Label lblStage;
        private Panel pnlBubbleChat;
        private PictureBox picFood;
        private Label lblQuantity;
        private PictureBox pboxFryer;
        private PictureBox pboxPares;
        private PictureBox pboxEggsBasket;
        private PictureBox pboxBatterBowl;
        private PictureBox pboxMeatBowl;
        private PictureBox pboxTray;
    }
}