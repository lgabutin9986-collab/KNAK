using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            pbTray.Visible = false;
            pbTray2.Visible = false;
            pbTray3.Visible = false;
            pbTray4.Visible = false;

            pbPlate1.Visible = false;
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
            // KASO 1: Kung may natitira pang kanin at HINDI nagluluto, maglalagay ng plato na may kanin sa sunod na bakanteng tray
            if (riceServingsLeft > 0 && !isCooking)
            {
                // Hanapin kung aling tray/plate ang bakante
                if (!pbPlate1.Visible)
                {
                    riceServingsLeft--;
                    pbTray.Visible = true;
                    pbPlate1.Image = Properties.Resources.plate_with_rice;
                    pbPlate1.Visible = true;
                    lblRiceStatus.Text = "Nalagay sa Tray 1. Natitira: " + riceServingsLeft + "/5 cups";
                }
                else if (!pbPlate2.Visible)
                {
                    riceServingsLeft--;
                    pbTray2.Visible = true;
                    pbPlate2.Image = Properties.Resources.plate_with_rice;
                    pbPlate2.Visible = true;
                    lblRiceStatus.Text = "Nalagay sa Tray 2. Natitira: " + riceServingsLeft + "/5 cups";
                }
                else if (!pbPlate3.Visible)
                {
                    riceServingsLeft--;
                    pbTray3.Visible = true;
                    pbPlate3.Image = Properties.Resources.plate_with_rice;
                    pbPlate3.Visible = true;
                    lblRiceStatus.Text = "Nalagay sa Tray 3. Natitira: " + riceServingsLeft + "/5 cups";
                }
                else if (!pbPlate4.Visible)
                {
                    riceServingsLeft--;
                    pbTray4.Visible = true;
                    pbPlate4.Image = Properties.Resources.plate_with_rice;
                    pbPlate4.Visible = true;
                    lblRiceStatus.Text = "Nalagay sa Tray 4. Natitira: " + riceServingsLeft + "/5 cups";
                }
                else
                {
                    // Kung puno ang lahat ng 4 na trays/plates
                    lblRiceStatus.Text = "Puno ang lahat ng trays! I-serve o tapusin muna ang iba.";
                    return;
                }

                // Kung naubos na ang 5 cups matapos kumuha
                if (riceServingsLeft <= 0)
                {
                    pbRiceCooker.Image = Properties.Resources.rice_cooker_empty;
                    lblRiceStatus.Text = "Ubos na ang kanin! Pindutin para magsaing.";
                }
            }
            // KASO 2: Kung ubos na ang kanin at pinindot para magsaing
            else if (riceServingsLeft <= 0 && !isCooking)
            {
                lblRiceStatus.Text = "Nagluluto na ng kanin...";
                riceProgressValue = 0;
                pbRiceProgress.Value = 0;
                pbRiceProgress.Visible = true; // Lilitaw na ang progress bar kapag nagluluto na
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
                lblRiceStatus.Text = "Luto na ang kanin! (5/5 cups)";

                pbRiceProgress.Visible = false; // Itatago ulit ang progress bar
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}