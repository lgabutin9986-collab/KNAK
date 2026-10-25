using System;
using System.Windows.Forms;

namespace KNAK
{
    public partial class SHOP : Form
    {
        public SHOP()
        {
            InitializeComponent();
            UpdateMoneyDisplay();
            CheckPurchasedItems();
        }

        private void UpdateMoneyDisplay()
        {
            // Gumagamit na ito ng Label kaya hindi na pwedeng i-typean
            lblSales.Text = "₱" + GameData.PlayerMoney.ToString();
        }

        private void CheckPurchasedItems()
        {
            if (GameData.HasDoubleBurner)
            {
                btnBuy1.Enabled = false;
                btnBuy1.Text = "OWNED";
            }
            if (GameData.HasHeavyDeepFryer)
            {
                btnBuy2.Enabled = false;
                btnBuy2.Text = "OWNED";
            }
            if (GameData.HasCommercialDispenser)
            {
                btnBuy3.Enabled = false;
                btnBuy3.Text = "OWNED";
            }
            if (GameData.HasPremiumBeef)
            {
                btnBuy4.Enabled = false;
                btnBuy4.Text = "OWNED";
            }
        }

        private void btnBuy1_Click(object sender, EventArgs e)
        {
            int cost = 1000;
            if (GameData.PlayerMoney >= cost)
            {
                GameData.PlayerMoney -= cost;
                GameData.HasDoubleBurner = true;
                UpdateMoneyDisplay();
                btnBuy1.Enabled = false;
                btnBuy1.Text = "OWNED";
                MessageBox.Show("Nabili na ang Double-Burner Stove!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kulang ang iyong benta, Bes!", "Bigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuy2_Click(object sender, EventArgs e)
        {
            int cost = 1000;
            if (GameData.PlayerMoney >= cost)
            {
                GameData.PlayerMoney -= cost;
                GameData.HasHeavyDeepFryer = true;
                UpdateMoneyDisplay();
                btnBuy2.Enabled = false;
                btnBuy2.Text = "OWNED";
                MessageBox.Show("Nabili na ang Heavy-Duty Deep Fryer!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kulang ang iyong benta, Bes!", "Bigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuy3_Click(object sender, EventArgs e)
        {
            int cost = 800;
            if (GameData.PlayerMoney >= cost)
            {
                GameData.PlayerMoney -= cost;
                GameData.HasCommercialDispenser = true;
                UpdateMoneyDisplay();
                btnBuy3.Enabled = false;
                btnBuy3.Text = "OWNED";
                MessageBox.Show("Nabili na ang Commercial Dispenser!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kulang ang iyong benta, Bes!", "Bigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuy4_Click(object sender, EventArgs e)
        {
            int cost = 800;
            if (GameData.PlayerMoney >= cost)
            {
                GameData.PlayerMoney -= cost;
                GameData.HasPremiumBeef = true;
                UpdateMoneyDisplay();
                btnBuy4.Enabled = false;
                btnBuy4.Text = "OWNED";
                MessageBox.Show("Nabili na ang Premium Beef Cut!", "Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kulang ang iyong benta, Bes!", "Bigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCloseShop_Click(object sender, EventArgs e)
        {
            // 1. Buksan ulit ang Main Menu 
            // (Siguraduhing palitan ang 'MAIN' ng eksaktong pangalan ng Main Menu form ninyo kung iba, halimbawa 'MainMenu')
            MAIN mainMenu = new MAIN();
            mainMenu.Show();

            // 2. Isasara ang Shop form
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e) { }
        private void SHOP_Load(object sender, EventArgs e) { }
    }
}