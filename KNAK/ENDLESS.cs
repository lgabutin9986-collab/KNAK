using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        private const int PBM_SETSTATE = 0x1010;
        private const int PBST_NORMAL = 1; // Green
        private const int PBST_ERROR = 2;  // Red

        private int currentScore = 0;
        private int totalSeconds = 0;

        // Rice Cooker at Plate Status Variables
        private int riceServingsLeft = 5;
        private int riceProgressValue = 0;
        private bool isCooking = false;

        // Gulaman (Dispenser) Status Variables - 8 seconds refill
        private int gulamanServingsLeft = 2;
        private double refillProgressValue = 0;
        private bool isRefilling = false;

        // Kwek-Kwek Status Variables - 3s stir, 5s cook + 5s before overcook
        private bool isEggMixed = false;
        private bool isEggMixing = false;
        private double eggMixProgressValue = 0;

        private enum FryerState { Empty, Cooking, Cooked, Overcooked }
        private FryerState currentFryerState = FryerState.Empty;
        private double fryerProgressValue = 0;
        private int fryerPhaseSeconds = 0;

        // Gagawa tayo ng 3 hiwalay na ProgressBar para hindi sila mag-agawan
        private ProgressBar pbRefill = new ProgressBar();
        private ProgressBar pbMix = new ProgressBar();
        private ProgressBar pbFryer = new ProgressBar();

        // Timers
        private System.Windows.Forms.Timer timerRefill = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerEggMix = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerFryer = new System.Windows.Forms.Timer();

        public ENDLESS()
        {
            InitializeComponent();

            rawegg.Click += rawegg_Click;
            coatedegg.Click += coatedegg_Click;
            pictureBox6.Click += pictureBox6_Click;

            // I-setup ang mga sariling Progress Bars at idagdag sa Form
            SetupCustomProgressBar(pbRefill, pictureBox5); // Sa ilalim ng dispenser
            SetupCustomProgressBar(pbMix, coatedegg);         // Sa ilalim ng mangkok
            SetupCustomProgressBar(pbFryer, pictureBox6);     // Sa ilalim ng fryer

            // Timer Intervals
            timerRefill.Interval = 80;   // 8 seconds refill
            timerRefill.Tick += timerRefill_Tick;

            timerEggMix.Interval = 60;   // 3 seconds stir
            timerEggMix.Tick += timerEggMix_Tick;

            timerFryer.Interval = 1000;  // 1 second per tick para sa frying at overcooked warning
            timerFryer.Tick += timerFryer_Tick;
        }

        private void SetupCustomProgressBar(ProgressBar pb, Control targetControl)
        {
            pb.Size = new Size(targetControl.Width, 12);
            pb.Location = new Point(targetControl.Location.X, targetControl.Location.Y + targetControl.Height + 3);
            pb.Visible = false;
            Controls.Add(pb);
            pb.BringToFront();
        }

        private void ENDLESS_Load(object sender, EventArgs e)
        {
            UpdateScoreDisplay();

            if (currentScore > GameData.EndlessHighScore)
            {
                GameData.EndlessHighScore = currentScore;
            }

            lblHighscore.Text = "₱" + GameData.EndlessHighScore.ToString("N0");

            // --- SIMULA NG LARO (DEFAULT STATES) ---
            pbRiceCooker.Image = Properties.Resources.rice_cooker_full;
            lblRiceStatus.Text = "Handa na ang kanin (" + riceServingsLeft + "/5 cups)";
            pbRiceProgress.Visible = false;

            pictureBox5.Image = Properties.Resources.fullgulaman;
            coatedegg.Image = Properties.Resources.noeggbowl;

            pbTray.Visible = false;
            pbTray2.Visible = false;
            pbTray3.Visible = false;

            pbPlate1.Visible = false;
            pbPlate2.Visible = false;
            pbPlate3.Visible = false;

            basogulaman1.Visible = false;
            basogulaman2.Visible = false;
            basogulaman3.Visible = false;

            kwek1.Visible = false;
            kwek2.Visible = false;

            pbRefill.Visible = false;
            pbMix.Visible = false;
            pbFryer.Visible = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            coatedegg.Image = Properties.Resources.noeggbowl;
            kwek1.Visible = false;
            kwek2.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            totalSeconds++;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            lblTimeElapsed.Text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }

        // --- RICE COOKER LOGIC ---
        private void pbRiceCooker_Click(object sender, EventArgs e)
        {
            if (riceServingsLeft > 0 && !isCooking)
            {
                if (!pbPlate1.Visible) { riceServingsLeft--; pbTray.Visible = true; pbPlate1.Image = Properties.Resources.plate_with_rice; pbPlate1.Visible = true; }
                else if (!pbPlate2.Visible) { riceServingsLeft--; pbTray2.Visible = true; pbPlate2.Image = Properties.Resources.plate_with_rice; pbPlate2.Visible = true; }
                else if (!pbPlate3.Visible) { riceServingsLeft--; pbTray3.Visible = true; pbPlate3.Image = Properties.Resources.plate_with_rice; pbPlate3.Visible = true; }
                else { return; }

                if (riceServingsLeft <= 0) pbRiceCooker.Image = Properties.Resources.rice_cooker_empty;
            }
            else if (riceServingsLeft <= 0 && !isCooking)
            {
                riceProgressValue = 0;
                pbRiceProgress.Value = 0;
                SetProgressBarColor(pbRiceProgress, PBST_NORMAL);
                pbRiceProgress.Visible = true;
                isCooking = true;
                timerRice.Start();
            }
        }

        private void timerRice_Tick(object sender, EventArgs e)
        {
            riceProgressValue += 5;
            if (riceProgressValue <= 100) pbRiceProgress.Value = riceProgressValue;

            if (riceProgressValue >= 100)
            {
                timerRice.Stop();
                isCooking = false;
                riceServingsLeft = 5;
                pbRiceCooker.Image = Properties.Resources.rice_cooker_full;
                pbRiceProgress.Visible = false;
            }
        }

        // --- GULAMAN REFILL LOGIC (8 seconds - pbRefill) ---
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (gulamanServingsLeft > 0 && !isRefilling)
            {
                if (!basogulaman1.Visible) { gulamanServingsLeft--; basogulaman1.BackColor = Color.Transparent; basogulaman1.Image = Properties.Resources.gulaman_removebg_preview; basogulaman1.Visible = true; basogulaman1.BringToFront(); }
                else if (!basogulaman2.Visible) { gulamanServingsLeft--; basogulaman2.BackColor = Color.Transparent; basogulaman2.Image = Properties.Resources.gulaman_removebg_preview; basogulaman2.Visible = true; basogulaman2.BringToFront(); }
                else if (!basogulaman3.Visible) { gulamanServingsLeft--; basogulaman3.BackColor = Color.Transparent; basogulaman3.Image = Properties.Resources.gulaman_removebg_preview; basogulaman3.Visible = true; basogulaman3.BringToFront(); }
                else { MessageBox.Show("Puno na ang lahat ng baso sa table!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (gulamanServingsLeft <= 0)
                {
                    pictureBox5.Image = Properties.Resources.emptygulaman;
                }
            }
            else if (gulamanServingsLeft <= 0 && !isRefilling)
            {
                isRefilling = true;
                refillProgressValue = 0;
                pbRefill.Value = 0;
                SetProgressBarColor(pbRefill, PBST_NORMAL); // Green
                pbRefill.Visible = true;
                timerRefill.Start();
            }
        }

        private void timerRefill_Tick(object sender, EventArgs e)
        {
            refillProgressValue += 1.25; // 8 seconds total

            if (refillProgressValue <= 100)
            {
                pbRefill.Value = (int)refillProgressValue;
            }

            if (refillProgressValue >= 100)
            {
                timerRefill.Stop();
                isRefilling = false;
                gulamanServingsLeft = 2;
                pictureBox5.Image = Properties.Resources.fullgulaman;
                pbRefill.Visible = false;
            }
        }

        // --- STIRRING LOGIC (3 seconds - pbMix) ---
        private void rawegg_Click(object sender, EventArgs e)
        {
            if (!isEggMixed && !isEggMixing)
            {
                isEggMixing = true;
                eggMixProgressValue = 0;
                coatedegg.Image = Properties.Resources.stiregg;

                pbMix.Value = 0;
                SetProgressBarColor(pbMix, PBST_NORMAL); // Green
                pbMix.Visible = true;
                timerEggMix.Start();
            }
        }

        private void timerEggMix_Tick(object sender, EventArgs e)
        {
            eggMixProgressValue += 3.33; // 3 seconds total

            if (eggMixProgressValue <= 100)
            {
                pbMix.Value = (int)eggMixProgressValue;
            }

            if (eggMixProgressValue >= 100)
            {
                timerEggMix.Stop();
                isEggMixing = false;
                isEggMixed = true;
                pbMix.Visible = false;
            }
        }

        private void coatedegg_Click(object sender, EventArgs e)
        {
            if (isEggMixed && currentFryerState == FryerState.Empty)
            {
                isEggMixed = false;
                coatedegg.Image = Properties.Resources.noeggbowl;

                currentFryerState = FryerState.Cooking;
                fryerProgressValue = 0;
                fryerPhaseSeconds = 0;
                pictureBox6.Image = Properties.Resources.cooking;

                pbFryer.Value = 0;
                SetProgressBarColor(pbFryer.Value == 0 ? pbFryer : pbFryer, PBST_NORMAL); // Green
                pbFryer.Visible = true;
                timerFryer.Start();
            }
        }

        // --- FRYER LOGIC (5s cooking [Green] -> 5s cooked with last 2s turning Red - pbFryer) ---
        private void timerFryer_Tick(object sender, EventArgs e)
        {
            fryerPhaseSeconds++;

            if (currentFryerState == FryerState.Cooking)
            {
                fryerProgressValue += 20; // 5 seconds para maluto
                if (fryerProgressValue > 100) fryerProgressValue = 100;

                pbFryer.Value = (int)fryerProgressValue;

                if (fryerPhaseSeconds >= 5)
                {
                    currentFryerState = FryerState.Cooked;
                    pictureBox6.Image = Properties.Resources.cookedkwek2;
                    fryerPhaseSeconds = 0;
                    fryerProgressValue = 0;
                    SetProgressBarColor(pbFryer, PBST_NORMAL); // Reset to green for the cooked wait window
                }
            }
            else if (currentFryerState == FryerState.Cooked)
            {
                fryerProgressValue += 20; // 5 seconds bago masunog
                if (fryerProgressValue > 100) fryerProgressValue = 100;

                pbFryer.Value = (int)fryerProgressValue;

                // Sa huling 2 segundo (ika-4 at ika-5 segundo), magiging kulay pula
                if (fryerPhaseSeconds >= 3)
                {
                    SetProgressBarColor(pbFryer, PBST_ERROR); // Red color
                }

                if (fryerPhaseSeconds >= 5)
                {
                    currentFryerState = FryerState.Overcooked;
                    pictureBox6.Image = Properties.Resources.overcookedkwek2;
                    timerFryer.Stop();
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (currentFryerState == FryerState.Cooked)
            {
                timerFryer.Stop();

                if (!kwek1.Visible)
                {
                    kwek1.BackColor = Color.Transparent;
                    kwek1.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
                    kwek1.Visible = true;
                    kwek1.BringToFront();
                }
                else if (!kwek2.Visible)
                {
                    kwek2.BackColor = Color.Transparent;
                    kwek2.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
                    kwek2.Visible = true;
                    kwek2.BringToFront();
                }
                else
                {
                    MessageBox.Show("Puno na ang pwesto ng kwek-kwek sa table!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentFryerState = FryerState.Empty;
                pictureBox6.Image = Properties.Resources.fryer;
                pbFryer.Visible = false;
            }
            else if (currentFryerState == FryerState.Overcooked)
            {
                timerFryer.Stop();
                currentFryerState = FryerState.Empty;
                pictureBox6.Image = Properties.Resources.fryer;
                pbFryer.Visible = false;
                MessageBox.Show("Nasunog ang kwek-kwek!", "Tapon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetProgressBarColor(ProgressBar pb, int state)
        {
            if (pb != null)
            {
                SendMessage(pb.Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
            }
        }

        private void UpdateScoreDisplay()
        {
            lblCurrentScore.Text = "₱" + currentScore.ToString("N0");
        }

        public void AddScore(int amount)
        {
            currentScore += amount;
            UpdateScoreDisplay();

            if (currentScore > GameData.EndlessHighScore)
            {
                GameData.EndlessHighScore = currentScore;
                lblHighscore.Text = "₱" + GameData.EndlessHighScore.ToString("N0");
            }
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox13_Click(object sender, EventArgs e) { }
    }
}