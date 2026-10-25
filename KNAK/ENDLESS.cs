using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        private MAIN? mainForm;

        // === ENDLESS MODE STATE ===
        private int wave = 1;
        private int score = 0;
        private int customersServedThisWave = 0;
        private const int CustomersPerWave = 5; // Customers per wave before next wave

        // Walking physics
        private float currentCustomerX;
        private int targetPosition = 400; // Center of canteen scene
        private float walkSpeed = 2.8f;
        private bool isLeaving = false;

        // Patience timers
        private System.Windows.Forms.Timer leaveTimer = new();
        private System.Windows.Forms.Timer angryTimer = new();

        // Dialogue lines for endless customers
        private static readonly string[] customerNames =
        {
            "Mang Berting (Manggagawa)",
            "Aling Rosa (Maestra)",
            "Kuya Jun (Tindero)",
            "Ate Len (Estudyante)",
            "Tatay Nanding (Driver)"
        };

        private static readonly string[] orderLines =
        {
            "Pabili po ng isang kanin at ulam!",
            "May arroz caldo pa po ba kayo?",
            "Isang baso ng malamigang tubig po at tinapay!",
            "Pabili po ng meryenda, dalawang lumpiang shanghai!",
            "May palabok pa po ba? Para sa almusal ko!"
        };

        private static readonly string[] servedLines =
        {
            "Salamat po! Ang sarap talaga dito sa kantina!",
            "Masarap! Bumalik ako bukas ha!",
            "Wow ang bilis! Salamat nang marami!",
            "Ang galing ninyo! Lagi akong babalik!",
            "Sulit ang pera ko dito, palagi akong nalilipasan ng gutom!"
        };

        private static readonly string[] talkLines =
        {
            "Matagal na akong kumakain dito, lagi pang masarap!",
            "Ang daming tao ngayon, sikat talaga itong kantina!",
            "Mainit ngayon ha, pero ang sarap ng pagkain dito.",
            "Kailangan ko munang kumain bago bumalik sa trabaho.",
            "Ang tipid dito! Masustansya pa!"
        };

        private int currentCustomerIndex = 0;
        private int talkIndex = 0;

        public ENDLESS()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
            UpdateStyles();

            // Leave after 10s of no service
            leaveTimer.Interval = 10000;
            leaveTimer.Tick += LeaveTimer_Tick;

            // Angry face after 5s
            angryTimer.Interval = 5000;
            angryTimer.Tick += AngryTimer_Tick;

            // Enable double buffering on CustomerPic
            try
            {
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(CustomerPic, true, null);
            }
            catch { }
        }

        public ENDLESS(MAIN main) : this()
        {
            mainForm = main;
        }

        // Auto-crops invisible transparent padding from PNG images
        private System.Drawing.Bitmap TrimTransparentEdges(System.Drawing.Bitmap source)
        {
            int minX = source.Width, minY = source.Height, maxX = -1, maxY = -1;
            for (int y = 0; y < source.Height; y++)
                for (int x = 0; x < source.Width; x++)
                    if (source.GetPixel(x, y).A > 10)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
            if (maxX < minX || maxY < minY) return source;
            var rect = new System.Drawing.Rectangle(minX, minY, (maxX - minX) + 1, (maxY - minY) + 1);
            return source.Clone(rect, source.PixelFormat);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void ENDLESS_Load(object sender, EventArgs e)
        {
            AttachButtonHover(btnBack, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 220, 53, 69));
            AttachButtonHover(btnServe, Color.FromArgb(40, 167, 69), Color.FromArgb(46, 204, 113));
            AttachButtonHover(btnTalk, Color.FromArgb(23, 162, 184), Color.FromArgb(52, 152, 219));

            UpdateHUD();
            SpawnNextCustomer();
        }

        // =========================================================
        // SPAWN & WALK LOGIC
        // =========================================================

        private void SpawnNextCustomer()
        {
            leaveTimer.Stop();
            angryTimer.Stop();
            WalkTimer.Stop();
            SpawnTimer.Stop();

            pnlDialogue.Visible = false;
            lblStatus.Text = "";

            // Reset customer appearance — full body, normal face
            CustomerPic.Image = Properties.Resources.worker_normal_face_removebg_preview;
            CustomerPic.Size = new Size(200, 252);
            CustomerPic.SizeMode = PictureBoxSizeMode.Zoom;
            CustomerPic.Visible = true;

            btnServe.Enabled = true;
            btnServe.BackColor = Color.FromArgb(40, 167, 69);
            talkIndex = 0;
            isLeaving = false;

            // Pick a customer for this slot
            currentCustomerIndex = (customersServedThisWave + (wave - 1)) % customerNames.Length;
            lblSpeaker.Text = customerNames[currentCustomerIndex];
            lblDialogue.Text = orderLines[currentCustomerIndex];

            // Walk in from the right
            currentCustomerX = ClientSize.Width + 20;
            CustomerPic.Location = new Point((int)currentCustomerX, 30);
            WalkTimer.Start();
        }

        private void WalkTimer_Tick(object sender, EventArgs e)
        {
            if (currentCustomerX > targetPosition)
            {
                currentCustomerX -= walkSpeed;
                int bob = (int)(Math.Sin(currentCustomerX * 0.18) * 2);

                if (currentCustomerX <= targetPosition)
                {
                    currentCustomerX = targetPosition;
                    CustomerPic.Location = new Point((int)currentCustomerX, 30);
                    WalkTimer.Stop();

                    if (!isLeaving)
                        OnCustomerArrivedAtCounter();
                    else
                        AfterCustomerLeft();
                }
                else
                {
                    CustomerPic.Location = new Point((int)currentCustomerX, 30 + bob);
                }
            }
            else
            {
                WalkTimer.Stop();
                CustomerPic.Location = new Point(targetPosition, 30);
                if (!isLeaving)
                    OnCustomerArrivedAtCounter();
                else
                    AfterCustomerLeft();
            }
        }

        private void OnCustomerArrivedAtCounter()
        {
            pnlDialogue.Visible = true;
            lblStatus.Text = "🕐 Naghihintay...";
            leaveTimer.Start();
            angryTimer.Start();
        }

        private void AngryTimer_Tick(object? sender, EventArgs e)
        {
            angryTimer.Stop();
            lblStatus.Text = "😠 Nainip na ang customer!";
            lblDialogue.Text = "Napakatagal naman ng order ko...";

            // Swap in angry face image (same logic as GAME.cs)
            try
            {
                string imagePath = @"C:\Users\Levin Gabutin\Downloads\worker angry face.png";
                string targetPath = System.IO.File.Exists(imagePath)
                    ? imagePath
                    : System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "worker angry face.png");

                if (System.IO.File.Exists(targetPath))
                {
                    using (var rawBmp = new System.Drawing.Bitmap(targetPath))
                    {
                        CustomerPic.Image = TrimTransparentEdges(rawBmp);
                    }
                    CustomerPic.Size = new Size(200, 252);
                    CustomerPic.SizeMode = PictureBoxSizeMode.Zoom;
                    CustomerPic.Location = new Point(targetPosition, 30);
                    CustomerPic.Invalidate();
                    CustomerPic.Update();
                }
            }
            catch { /* angry face unavailable — text feedback still shown */ }
        }

        private void LeaveTimer_Tick(object? sender, EventArgs e)
        {
            leaveTimer.Stop();
            angryTimer.Stop();
            pnlDialogue.Visible = false;
            lblStatus.Text = "❌ Umalis ang customer! -10 pts";
            score = Math.Max(0, score - 10);
            UpdateHUD();

            isLeaving = true;
            targetPosition = -220;
            WalkTimer.Start();
        }

        private void AfterCustomerLeft()
        {
            targetPosition = 400; // reset for next customer
            CustomerPic.Visible = false;

            customersServedThisWave++;
            if (customersServedThisWave >= CustomersPerWave)
            {
                customersServedThisWave = 0;
                wave++;
                walkSpeed = Math.Min(8f, walkSpeed + 0.3f); // Gets faster each wave!
                leaveTimer.Interval = Math.Max(4000, leaveTimer.Interval - 500); // Patience shrinks
                angryTimer.Interval = Math.Max(2000, angryTimer.Interval - 300);
                lblStatus.Text = $"🎉 Wave {wave}! Mas maraming customer!";
                UpdateHUD();
            }

            SpawnTimer.Start(); // short pause then spawn next
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnTimer.Stop();
            SpawnNextCustomer();
        }

        // =========================================================
        // BUTTON HANDLERS
        // =========================================================

        private void btnServe_Click(object sender, EventArgs e)
        {
            leaveTimer.Stop();
            angryTimer.Stop();

            int points = 50 + (wave - 1) * 10; // More points per wave
            score += points;
            UpdateHUD();

            lblDialogue.Text = servedLines[currentCustomerIndex];
            lblStatus.Text = $"⭐ Served! +{points} pts";
            btnServe.Enabled = false;
            btnServe.BackColor = Color.FromArgb(108, 117, 125);

            // Leave after short delay showing thanks
            isLeaving = true;
            targetPosition = -220;
            WalkTimer.Interval = 16;

            // Wait 1.5s before customer walks out
            var thankTimer = new System.Windows.Forms.Timer { Interval = 1500 };
            thankTimer.Tick += (s, ev) =>
            {
                thankTimer.Stop();
                thankTimer.Dispose();
                pnlDialogue.Visible = false;
                WalkTimer.Start();
            };
            thankTimer.Start();
        }

        private void btnTalk_Click(object sender, EventArgs e)
        {
            lblDialogue.Text = talkLines[talkIndex % talkLines.Length];
            talkIndex++;
        }

        private void CustomerPic_Click(object sender, EventArgs e)
        {
            if (!WalkTimer.Enabled)
                btnTalk_Click(sender, e);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
                mainForm.Show();
            this.Close();
        }

        // =========================================================
        // HUD
        // =========================================================

        private void UpdateHUD()
        {
            lblWave.Text = $"Wave: {wave}";
            lblScore.Text = $"Score: {score}";
        }

        // =========================================================
        // HELPERS
        // =========================================================

        private void AttachButtonHover(Button btn, Color normalColor, Color hoverColor)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) =>
            {
                if (btn.Enabled) btn.BackColor = normalColor;
            };
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            leaveTimer.Stop();
            leaveTimer.Dispose();
            angryTimer.Stop();
            angryTimer.Dispose();
            WalkTimer.Stop();
            SpawnTimer.Stop();
            SpawnTimer.Dispose();

            if (mainForm != null && !mainForm.IsDisposed)
                mainForm.Show();
            else
                Application.Exit();
        }
    }
}
