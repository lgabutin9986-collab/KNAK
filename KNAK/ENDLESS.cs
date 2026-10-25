using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        int currentScore = 0;   
        int highScore = 0; // O kung ano man ang nakalagay sa high score mo
        int strikeCount = 0;
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

        // --- EGG SYSTEM VARIABLES ---
        private int eggSecondsCount = 0;
        private string eggState = "EMPTY"; // "EMPTY", "COOKING", "COOKED", "BURNED"

        // Mga variables para sa Tapa
        string tapaState = "EMPTY";
        int tapaSecondsCount = 0;

        // Mga variables para sa Pares
        int paresServingsLeft = 3;// May capacity na 3bowls
        bool isCookingPares = false;
        int paresProgressValue = 0;

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
            lblRiceStatus.Text = "Handa na ang kanin (" + riceServingsLeft + "/3 Cups)";
            pbRiceProgress.Visible = false; // Nakatago ang progress bar habang may kanin pa
            lblRiceStatus.Text = "Handa na ang kanin (" + riceServingsLeft + "/5 cups)";
            pbRiceProgress.Visible = false;

            // Itago muna ang lahat ng 4 na trays at 4 na plates sa simula
            pbTray1.Visible = false;
            pictureBox5.Image = Properties.Resources.fullgulaman;
            coatedegg.Image = Properties.Resources.noeggbowl;

            pbTray.Visible = false;
            pbTray2.Visible = false;
            pbTray3.Visible = false;

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

        // --- RICE COOKER & PLATES LOGIC ---

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
                // Unahin natin ang Tray 1 / Plate 2
                if (!pbPlate2.Visible)
                {
                    riceServingsLeft--;
                    pbTray1.Visible = true;
                    pbPlate2.Image = Properties.Resources.plate_with_rice;
                    pbPlate2.Tag = "with_rice"; // <-- Nilagyan ng Tag para mabasa ng itlog at tapa
                    pbPlate2.Visible = true;
                    lblRiceStatus.Text = riceServingsLeft + "/3 cups";
                }
                else if (!pbPlate3.Visible)
                {
                    riceServingsLeft--;
                    pbTray2.Visible = true; // Tray 2
                    pbPlate3.Image = Properties.Resources.plate_with_rice;
                    pbPlate3.Tag = "with_rice"; // <-- Nilagyan ng Tag
                    pbPlate3.Visible = true;
                    lblRiceStatus.Text = riceServingsLeft + "/3 cups";
                }
                else if (!pbPlate4.Visible)
                {
                    riceServingsLeft--;
                    pbTray3.Visible = true; // Tray 3 (dahil tinanggal na ang Tray 4)
                    pbPlate4.Image = Properties.Resources.plate_with_rice;
                    pbPlate4.Tag = "with_rice"; // <-- Nilagyan ng Tag
                    pbPlate4.Visible = true;
                    lblRiceStatus.Text = riceServingsLeft + "/3 cups";
                }
                else
                {
                    lblRiceStatus.Text = "Puno na ang trays!";
                    return;
                }
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

                // Kapag naubos na ang 3 cups
                if (riceServingsLeft <= 0)
                {
                    pbRiceCooker.Image = Properties.Resources.rice_cooker_empty;
                    lblRiceStatus.Text = "Click to cook!";
                }
            }
            // KASO 2: Kung ubos na at magsaing muli
            else if (riceServingsLeft <= 0 && !isCooking)
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
                lblRiceStatus.Text = "Cooking...";
                riceProgressValue = 0;
                pbRiceProgress.Value = 0;
                pbRiceProgress.Visible = true;
                isCooking = true;
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

                riceServingsLeft = 5; // Bumalik sa 5 cups
                pbRiceCooker.Image = Properties.Resources.rice_cooker_full; // Babalik sa puno ang rice cooker
                lblRiceStatus.Text = "Cooked!";
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
        private void pbRawEgg_Click(object sender, EventArgs e)
        {
            // Pwede lang magluto kung talagang EMPTY at HINDI pa umaandar ang timer
            if (eggState == "EMPTY")
            {
                eggState = "COOKING";
                eggSecondsCount = 0;

                pbEggOnPan.Visible = true;
                pbEggOnPan.Image = Properties.Resources.cooking_egg;

                pbEggProgress.Visible = true;
                pbEggProgress.Value = 0;

                // Siguraduhing i-stop muna bago i-start para iwas bug
                timerEgg.Stop();
                timerEgg.Start();
            }
        }
        private void timerEgg_Tick(object sender, EventArgs e)
        {
            eggSecondsCount++;

            lblEggStatus.Visible = true;

            // 1 to 2 seconds: Nagluluto pa lang
            if (eggSecondsCount <= 2)
            {
                eggState = "COOKING";
                pbEggProgress.Value = 50; // Kalahati muna
                pbEggOnPan.Image = Properties.Resources.cooking_egg;

                lblEggStatus.Text = "Nagluluto...";
                lblEggStatus.ForeColor = Color.Green;
            }
            // Exactly 3 to 4 seconds: LUTO NA! (Dito natin pinalawak para sure na aabot)
            else if (eggSecondsCount >= 3 && eggSecondsCount <= 4)
            {
                eggState = "COOKED";
                pbEggOnPan.Image = Properties.Resources.cooked_egg;
                pbEggProgress.Value = 100;

                lblEggStatus.Text = "Luto na! Kunin na!";
                lblEggStatus.ForeColor = Color.Blue;
            }
            // 5 seconds: Malapit nang masunog (Warning)
            else if (eggSecondsCount == 5)
            {
                eggState = "COOKING";
                lblEggStatus.Text = "Malapit nang masunog!";
                lblEggStatus.ForeColor = Color.Orange;
            }
            // 6 seconds pataas: SUNOG NA!
            else if (eggSecondsCount >= 6)
            {
                eggState = "BURNED";
                timerEgg.Stop();

                pbEggOnPan.Image = Properties.Resources.burned_egg;
                pbEggProgress.Visible = false;

                lblEggStatus.Text = "Sunog! Tapon na!";
                lblEggStatus.ForeColor = Color.Red;
            }
        }

        // --- SCORE SYSTEM ---
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
        // Halimbawa ito ng method o event kapag nag-walkout ang isang customer
        private void CustomerWalkout()
        {
            strikeCount++;

            // Baguhin ang circle creatives depende sa bilang ng strike
            if (strikeCount == 1)
            {
                pbCircle1.Image = Properties.Resources.red_x; // O kung ano mang creative asset ng strike mo
            }
            else if (strikeCount == 2)
            {
                pbCircle2.Image = Properties.Resources.red_x;
            }
            else if (strikeCount >= 3)
            {
                pbCircle3.Image = Properties.Resources.red_x;

                // --- GAME OVER LOGIC ---
                timerEgg.Stop();
                timerTapa.Stop();
                timerRice.Stop();
                timerPares.Stop();

                MessageBox.Show("GAME OVER! Umabot sa 3 ang walkout/strikes mo.\nKabuuang Nakuha Mo: ₱" + currentScore.ToString("N0"), "Laro Tapos Na", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Pwede mo rito ilagay ang pagbalik sa Main Menu o pag-reset
            }
        }
        private void pbEggOnPan_Click(object sender, EventArgs e)
        {
            if (eggState == "COOKING")
            {
                lblEggStatus.Text = "Huwag kang atat, hindi pa luto!";
                lblEggStatus.ForeColor = Color.OrangeRed;
            }
            else if (eggState == "COOKED")
            {
                timerEgg.Stop();

                // SISIGURUHIN NATIN: Kay plain rice lang pwedeng lumapag ang itlog
                if (pbPlate2.Tag?.ToString() == "with_rice")
                {
                    pbPlate2.Image = Properties.Resources.plate_with_rice_and_egg;
                    pbPlate2.Tag = "with_egg"; // Markahan na may itlog na
                }
                else if (pbPlate3.Tag?.ToString() == "with_rice")
                {
                    pbPlate3.Image = Properties.Resources.plate_with_rice_and_egg;
                    pbPlate3.Tag = "with_egg";
                }
                else if (pbPlate4.Tag?.ToString() == "with_rice")
                {
                    pbPlate4.Image = Properties.Resources.plate_with_rice_and_egg;
                    pbPlate4.Tag = "with_egg";
                }
                else
                {
                    lblEggStatus.Text = "Walang platong may plain rice para sa itlog!";
                }

                eggState = "EMPTY";
                pbEggOnPan.Visible = false;
                pbEggProgress.Visible = false;
                lblEggStatus.Visible = false;
                eggSecondsCount = 0;
            }
            else if (eggState == "BURNED")
            {
                timerEgg.Stop();
                eggState = "EMPTY";
                pbEggOnPan.Visible = false;
                pbEggProgress.Visible = false;
                lblEggStatus.Visible = false;
                eggSecondsCount = 0;
            }
        }

        private void pbRawTapa_Click(object sender, EventArgs e)
        {
            if (tapaState == "EMPTY")
            {
                tapaState = "COOKING";
                tapaSecondsCount = 0;

                pbTapaOnPan.Visible = true;
                pbTapaOnPan.Image = Properties.Resources.cooking_tapa; // Litrato ng naglulutong tapa

                pbTapaProgress.Visible = true;
                pbTapaProgress.Value = 0;

                timerTapa.Stop();
                timerTapa.Start();
            }
        }

        private void timerTapa_Tick(object sender, EventArgs e)
        {
            tapaSecondsCount++;
            lblTapaStatus.Visible = true;

            // 1 to 4 Seconds: Nagluluto pa lang ang tapa
            if (tapaSecondsCount < 5)
            {
                tapaState = "COOKING";
                pbTapaProgress.Value = (tapaSecondsCount * 20); // 5 intervals para umabot sa 100%
                pbTapaOnPan.Image = Properties.Resources.cooking_tapa;

                lblTapaStatus.Text = "Nagluluto ang tapa...";
                lblTapaStatus.ForeColor = Color.White;
            }
            // 5th Second: Luto na!
            else if (tapaSecondsCount == 5)
            {
                tapaState = "COOKED";
                pbTapaOnPan.Image = Properties.Resources.cooked_tapa;
                pbTapaProgress.Value = 100;

                lblTapaStatus.Text = "Luto na ang tapa! Kunin na!";
                lblTapaStatus.ForeColor = Color.Blue;
            }
            // 6 to 7 Seconds: Palugit bago masunog (Warning)
            else if (tapaSecondsCount >= 6 && tapaSecondsCount < 8)
            {
                lblTapaStatus.Text = "Malapit nang masunog ang tapa!";
                lblTapaStatus.ForeColor = Color.Orange;
            }
            // 8 Seconds pataas: Sunog na!
            else if (tapaSecondsCount >= 8)
            {
                tapaState = "BURNED";
                timerTapa.Stop();

                pbTapaOnPan.Image = Properties.Resources.burned_tapa;
                pbTapaProgress.Visible = false;

                lblTapaStatus.Text = "Sunog ang tapa! Tapon na!";
                lblTapaStatus.ForeColor = Color.Red;
            }
        }

        private void pbTapaOnPan_Click(object sender, EventArgs e)
        {
            if (tapaState == "COOKING")
            {
                lblTapaStatus.Text = "Huwag kang atat, hilaw pa ang tapa!";
                lblTapaStatus.ForeColor = Color.OrangeRed;
            }
            else if (tapaState == "COOKED")
            {
                timerTapa.Stop();

                // SISIGURUHIN NATIN: Doon lang sa may itlog ("with_egg") pwedeng pumasok ang tapa para maging tapsilog
                if (pbPlate2.Tag?.ToString() == "with_egg")
                {
                    pbPlate2.Image = Properties.Resources.plate_with_tapsilog; // Palitan ng tamang resource name mo
                    pbPlate2.Tag = "with_tapsilog"; // Markahan na tapos na ang tapsilog
                }
                else if (pbPlate3.Tag?.ToString() == "with_egg")
                {
                    pbPlate3.Image = Properties.Resources.plate_with_tapsilog;
                    pbPlate3.Tag = "with_tapsilog";
                }
                else if (pbPlate4.Tag?.ToString() == "with_egg")
                {
                    pbPlate4.Image = Properties.Resources.plate_with_tapsilog;
                    pbPlate4.Tag = "with_tapsilog";
                }
                else
                {
                    lblTapaStatus.Text = "Kailangan muna ng plato na may itlog para sa tapa!";
                }

                tapaState = "EMPTY";
                pbTapaOnPan.Visible = false;
                pbTapaProgress.Visible = false;
                lblTapaStatus.Visible = false;
                tapaSecondsCount = 0;
            }
            else if (tapaState == "BURNED")
            {
                timerTapa.Stop();
                tapaState = "EMPTY";
                pbTapaOnPan.Visible = false;
                pbTapaProgress.Visible = false;
                lblTapaStatus.Visible = false;
                tapaSecondsCount = 0;
            }
        }

        private void timerPares_Tick(object sender, EventArgs e)
        {
            paresProgressValue++;
            pbParesProgress.Value = (paresProgressValue * 10); // 10 steps para umabot sa 100% (10 seconds)

            if (paresProgressValue >= 10)
            {
                timerPares.Stop();
                isCookingPares = false;
                paresServingsLeft = 5; // Balik sa 5 servings

                pbParesPot.Image = Properties.Resources.pares_pot_full; // Palitan ng tamang resource mo para sa puno na pot
                lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                pbParesProgress.Visible = false;
            }
        }

        private void pbParesPot_Click(object sender, EventArgs e)
        {
            // KASO 1: Kung may natitira pang pares bowls at HINDI pa nagluluto/nagrerefill
            if (paresServingsLeft > 0 && !isCookingPares)
            {
                // Ilalagay natin ang hiwalay na pares bowl sa bakanteng tray/spot (sunod-sunod sa Tray 1, Tray 2, Tray 3)
                // Dito natin iche-check kung alin ang bakanteng pares bowl picturebox sa mga tray niyo:
                if (!pbParesBowl1.Visible) // O kung ano man ang exact name ng pares bowl para sa unang tray
                {
                    paresServingsLeft--;
                    pbParesBowl1.Visible = true;
                    pbParesBowl1.Image = Properties.Resources.pares_bowl; // Ang sarili niyang creative asset
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else if (!pbParesBowl2.Visible)
                {
                    paresServingsLeft--;
                    pbParesBowl2.Visible = true;
                    pbParesBowl2.Image = Properties.Resources.pares_bowl;
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else if (!pbParesBowl3.Visible)
                {
                    paresServingsLeft--;
                    pbParesBowl3.Visible = true;
                    pbParesBowl3.Image = Properties.Resources.pares_bowl;
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else
                {
                    lblParesStatus.Text = "Puno na ang mga pares bowls sa trays!";
                    return;
                }

                // Kapag naubos na ang 5 bowls
                if (paresServingsLeft <= 0)
                {
                    pbParesPot.Image = Properties.Resources.pares_pot_empty; // Pot na walang laman
                    lblParesStatus.Text = "Click to refill!";
                }
            }
            // KASO 2: Kung ubos na at kailangan i-refill (10 seconds)
            else if (paresServingsLeft <= 0 && !isCookingPares)
            {
                lblParesStatus.Text = "Refilling Pares...";
                paresProgressValue = 0;
                pbParesProgress.Value = 0;
                pbParesProgress.Visible = true;
                isCookingPares = true;

                timerPares.Start();
            }
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox13_Click(object sender, EventArgs e) { }
    }
}