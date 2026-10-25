using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
            components = new Container();
            btnBack = new Button();
            lblTitle = new Label();
            lblWave = new Label();
            lblScore = new Label();
            pnlDialogue = new Panel();
            lblStatus = new Label();
            btnTalk = new Button();
            btnServe = new Button();
            lblDialogue = new Label();
            lblSpeaker = new Label();
            CustomerPic = new PictureBox();
            SpawnTimer = new System.Windows.Forms.Timer(components);
            WalkTimer = new System.Windows.Forms.Timer(components);
            pnlDialogue.SuspendLayout();
            ((ISupportInitialize)CustomerPic).BeginInit();
            SuspendLayout();
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
            btnBack.TabIndex = 0;
            btnBack.Text = "← Main Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(220, 20, 24, 33);
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 165, 0);
            lblTitle.Location = new Point(362, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 36);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "♾ ENDLESS MODE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWave
            // 
            lblWave.BackColor = Color.FromArgb(190, 20, 24, 33);
            lblWave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWave.ForeColor = Color.FromArgb(255, 215, 0);
            lblWave.Location = new Point(16, 62);
            lblWave.Name = "lblWave";
            lblWave.Size = new Size(140, 28);
            lblWave.TabIndex = 2;
            lblWave.Text = "Wave: 1";
            lblWave.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblScore
            // 
            lblScore.BackColor = Color.FromArgb(190, 20, 24, 33);
            lblScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblScore.ForeColor = Color.FromArgb(100, 220, 100);
            lblScore.Location = new Point(870, 62);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(138, 28);
            lblScore.TabIndex = 3;
            lblScore.Text = "Score: 0";
            lblScore.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlDialogue
            // 
            pnlDialogue.BackColor = Color.FromArgb(240, 24, 28, 38);
            pnlDialogue.Controls.Add(lblStatus);
            pnlDialogue.Controls.Add(btnTalk);
            pnlDialogue.Controls.Add(btnServe);
            pnlDialogue.Controls.Add(lblDialogue);
            pnlDialogue.Controls.Add(lblSpeaker);
            pnlDialogue.Location = new Point(272, 400);
            pnlDialogue.Name = "pnlDialogue";
            pnlDialogue.Size = new Size(480, 140);
            pnlDialogue.TabIndex = 4;
            pnlDialogue.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblStatus.ForeColor = Color.FromArgb(255, 215, 0);
            lblStatus.Location = new Point(278, 98);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(190, 25);
            lblStatus.TabIndex = 4;
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnTalk
            // 
            btnTalk.BackColor = Color.FromArgb(23, 162, 184);
            btnTalk.Cursor = Cursors.Hand;
            btnTalk.FlatAppearance.BorderSize = 0;
            btnTalk.FlatStyle = FlatStyle.Flat;
            btnTalk.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTalk.ForeColor = Color.White;
            btnTalk.Location = new Point(168, 92);
            btnTalk.Name = "btnTalk";
            btnTalk.Size = new Size(100, 36);
            btnTalk.TabIndex = 3;
            btnTalk.Text = "💬 Talk";
            btnTalk.UseVisualStyleBackColor = false;
            btnTalk.Click += btnTalk_Click;
            // 
            // btnServe
            // 
            btnServe.BackColor = Color.FromArgb(40, 167, 69);
            btnServe.Cursor = Cursors.Hand;
            btnServe.FlatAppearance.BorderSize = 0;
            btnServe.FlatStyle = FlatStyle.Flat;
            btnServe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnServe.ForeColor = Color.White;
            btnServe.Location = new Point(16, 92);
            btnServe.Name = "btnServe";
            btnServe.Size = new Size(140, 36);
            btnServe.TabIndex = 2;
            btnServe.Text = "🍱 Serve Order";
            btnServe.UseVisualStyleBackColor = false;
            btnServe.Click += btnServe_Click;
            // 
            // lblDialogue
            // 
            lblDialogue.Font = new Font("Segoe UI", 10F);
            lblDialogue.ForeColor = Color.White;
            lblDialogue.Location = new Point(16, 38);
            lblDialogue.Name = "lblDialogue";
            lblDialogue.Size = new Size(448, 48);
            lblDialogue.TabIndex = 1;
            lblDialogue.Text = "Pabili po!";
            // 
            // lblSpeaker
            // 
            lblSpeaker.AutoSize = true;
            lblSpeaker.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSpeaker.ForeColor = Color.FromArgb(255, 193, 7);
            lblSpeaker.Location = new Point(16, 12);
            lblSpeaker.Name = "lblSpeaker";
            lblSpeaker.Size = new Size(87, 23);
            lblSpeaker.TabIndex = 0;
            lblSpeaker.Text = "Customer";
            // 
            // CustomerPic
            // 
            CustomerPic.BackColor = Color.Transparent;
            CustomerPic.Cursor = Cursors.Hand;
            CustomerPic.Image = Properties.Resources.worker_normal_face_removebg_preview;
            CustomerPic.Location = new Point(400, 30);
            CustomerPic.Name = "CustomerPic";
            CustomerPic.Size = new Size(200, 252);
            CustomerPic.SizeMode = PictureBoxSizeMode.Zoom;
            CustomerPic.TabIndex = 5;
            CustomerPic.TabStop = false;
            CustomerPic.Click += CustomerPic_Click;
            // 
            // SpawnTimer
            // 
            SpawnTimer.Interval = 2000;
            SpawnTimer.Tick += SpawnTimer_Tick;
            // 
            // WalkTimer
            // 
            WalkTimer.Interval = 16;
            WalkTimer.Tick += WalkTimer_Tick;
            // 
            // ENDLESS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.endless_mode_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1024, 576);
            Controls.Add(CustomerPic);
            Controls.Add(pnlDialogue);
            Controls.Add(lblScore);
            Controls.Add(lblWave);
            Controls.Add(lblTitle);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ENDLESS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KNAK - Endless Mode";
            Load += ENDLESS_Load;
            pnlDialogue.ResumeLayout(false);
            pnlDialogue.PerformLayout();
            ((ISupportInitialize)CustomerPic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
        private Label lblTitle;
        private Label lblWave;
        private Label lblScore;
        private Panel pnlDialogue;
        private Label lblSpeaker;
        private Label lblDialogue;
        private Button btnServe;
        private Button btnTalk;
        private Label lblStatus;
        private PictureBox CustomerPic;
        private System.Windows.Forms.Timer SpawnTimer;
        private System.Windows.Forms.Timer WalkTimer;
    }
}
