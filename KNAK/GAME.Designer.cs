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
            components = new System.ComponentModel.Container();
            ConstructionWorker = new PictureBox();
            Walktimer = new System.Windows.Forms.Timer(components);
            btnBack = new Button();
            btnRestart = new Button();
            lblStage = new Label();
            pnlDialogue = new Panel();
            lblSpeaker = new Label();
            lblDialogue = new Label();
            btnServe = new Button();
            btnTalk = new Button();
            lblStatus = new Label();
            ((System.ComponentModel.ISupportInitialize)ConstructionWorker).BeginInit();
            pnlDialogue.SuspendLayout();
            SuspendLayout();
            // 
            // ConstructionWorker
            // 
            ConstructionWorker.BackColor = Color.Transparent;
            ConstructionWorker.Image = Properties.Resources.worker_normal_face_removebg_preview;
            ConstructionWorker.Location = new Point(720, 14);
            ConstructionWorker.Name = "ConstructionWorker";
            ConstructionWorker.Size = new Size(170, 255);
            ConstructionWorker.SizeMode = PictureBoxSizeMode.Zoom;
            ConstructionWorker.TabIndex = 0;
            ConstructionWorker.TabStop = false;
            ConstructionWorker.Cursor = Cursors.Hand;
            ConstructionWorker.Click += ConstructionWorker_Click;
            // 
            // Walktimer
            // 
            Walktimer.Interval = 16;
            Walktimer.Tick += Walktimer_Tick;
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
            lblStage.Location = new Point(412, 16);
            lblStage.Name = "lblStage";
            lblStage.Size = new Size(200, 32);
            lblStage.TabIndex = 3;
            lblStage.Text = "STAGE 1 • CANTEEN";
            lblStage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDialogue
            // 
            pnlDialogue.BackColor = Color.FromArgb(240, 24, 28, 38);
            pnlDialogue.Controls.Add(lblStatus);
            pnlDialogue.Controls.Add(btnTalk);
            pnlDialogue.Controls.Add(btnServe);
            pnlDialogue.Controls.Add(lblDialogue);
            pnlDialogue.Controls.Add(lblSpeaker);
            pnlDialogue.Location = new Point(272, 290);
            pnlDialogue.Name = "pnlDialogue";
            pnlDialogue.Size = new Size(480, 140);
            pnlDialogue.TabIndex = 4;
            pnlDialogue.Visible = false;
            // 
            // lblSpeaker
            // 
            lblSpeaker.AutoSize = true;
            lblSpeaker.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSpeaker.ForeColor = Color.FromArgb(255, 193, 7);
            lblSpeaker.Location = new Point(16, 12);
            lblSpeaker.Name = "lblSpeaker";
            lblSpeaker.Size = new Size(185, 23);
            lblSpeaker.TabIndex = 0;
            lblSpeaker.Text = "Mang Berting (Worker)";
            // 
            // lblDialogue
            // 
            lblDialogue.Font = new Font("Segoe UI", 10F);
            lblDialogue.ForeColor = Color.White;
            lblDialogue.Location = new Point(16, 38);
            lblDialogue.Name = "lblDialogue";
            lblDialogue.Size = new Size(448, 48);
            lblDialogue.TabIndex = 1;
            lblDialogue.Text = "Pabili po! Isang malamig na tubig at tinapay para sa meryenda.";
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
            btnServe.Text = "🍱 Serve Merienda";
            btnServe.UseVisualStyleBackColor = false;
            btnServe.Click += btnServe_Click;
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
            // GAME
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 576);
            Controls.Add(pnlDialogue);
            Controls.Add(lblStage);
            Controls.Add(btnRestart);
            Controls.Add(btnBack);
            Controls.Add(ConstructionWorker);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GAME";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "KNAK - Stage 1";
            Load += GAME_Load;
            ((System.ComponentModel.ISupportInitialize)ConstructionWorker).EndInit();
            pnlDialogue.ResumeLayout(false);
            pnlDialogue.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ConstructionWorker;
        private System.Windows.Forms.Timer Walktimer;
        private Button btnBack;
        private Button btnRestart;
        private Label lblStage;
        private Panel pnlDialogue;
        private Label lblSpeaker;
        private Label lblDialogue;
        private Button btnServe;
        private Button btnTalk;
        private Label lblStatus;
    }
}