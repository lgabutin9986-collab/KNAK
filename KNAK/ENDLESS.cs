using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        private int currentScore = 0;
        private int totalSeconds = 0;

        // Rice Cooker at Plate Status Variables
        private int riceServingsLeft = 3;// Hanggang 5 cups/servings lang
        private int riceProgressValue = 0;
        private bool isCooking = false;

        // --- EGG SYSTEM VARIABLES ---
        private int eggSecondsCount = 0;
        private string eggState = "EMPTY"; // "EMPTY", "COOKING", "COOKED", "BURNED"

        // Mga variables para sa Tapa
        string tapaState = "EMPTY";
        int tapaSecondsCount = 0; 
        public ENDLESS()
        {
            InitializeComponent();
        }

        private void ENDLESS_Load(object sender, EventArgs e)
        {
            UpdateScoreDisplay();

            if (currentScore > GameData.EndlessHighScore)
            {
                GameData.EndlessHighScore = currentScore;
            }

            lblHighscore.Text = "₱" + GameData.EndlessHighScore.ToString("N0");

            // --- SIMULA NG LARO ---
            // Puno ang rice cooker
            pbRiceCooker.Image = Properties.Resources.rice_cooker_full;
            lblRiceStatus.Text = "Handa na ang kanin (" + riceServingsLeft + "/5 cups)";
            pbRiceProgress.Visible = false; // Nakatago ang progress bar habang may kanin pa

            // Itago muna ang lahat ng 4 na trays at 4 na plates sa simula
            pbTray1.Visible = false;
            pbTray2.Visible = false;
            pbTray3.Visible = false;

            pbPlate2.Visible = false;
            pbPlate3.Visible = false;
            pbPlate4.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            totalSeconds++;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            lblTimeElapsed.Text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }

        // --- RICE COOKER & PLATES LOGIC ---

        private void pbRiceCooker_Click(object sender, EventArgs e)
        {
            // KASO 1: Kung may natitira pang kanin at HINDI nagluluto
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

                // Kapag naubos na ang 3 cups
                if (riceServingsLeft <= 0)
                {
                    pbRiceCooker.Image = Properties.Resources.rice_cooker_empty;
                    lblRiceStatus.Text = "Click to cook!";
                }
            }
            // KASO 2: Kung ubos na at magsaing muli
            else if (riceServingsLeft <= 0 && !isCooking)
            {
                lblRiceStatus.Text = "Cooking...";
                riceProgressValue = 0;
                pbRiceProgress.Value = 0;
                pbRiceProgress.Visible = true;
                isCooking = true;

                timerRice.Start();
            }
        }

        // Timer para sa pagluluto kapag naubos ang kanin
        private void timerRice_Tick(object sender, EventArgs e)
        {
            riceProgressValue += 5; // Bilis ng pag-akyat ng progress bar

            if (riceProgressValue <= 100)
            {
                pbRiceProgress.Value = riceProgressValue;
            }

            // Kapag tapos na ang pagluluto (100%)
            if (riceProgressValue >= 100)
            {
                timerRice.Stop();
                isCooking = false;

                riceServingsLeft = 5; // Bumalik sa 5 cups
                pbRiceCooker.Image = Properties.Resources.rice_cooker_full; // Babalik sa puno ang rice cooker
                lblRiceStatus.Text = "Cooked!";

                pbRiceProgress.Visible = false; // Itatago ulit ang progress bar
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}