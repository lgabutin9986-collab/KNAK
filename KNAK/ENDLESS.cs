using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        private MAIN? mainForm;
        private Bitmap? cachedBackground;

        // Walking physics & mechanics
        private float currentWorkerX;
        private int targetPosition = 427; // Center opening behind the counter for 1024px width
        private const int baseY = 28; // Positioned firmly at the counter shelf level (no floating gap)
        private float baseSpeed = 5.2f; // Faster than Stage 1 (2.4f) so they appear quicker
        private float currentSpeed = 5.2f;
        private int stepCount = 0;
        private WorkerState currentState = WorkerState.WalkingIn;

        // Customer & Endless Rush tracking
        private int servedCount = 0;
        private int currentStreak = 0;
        private int totalScore = 0;
        private int talkCycleIndex = 0;
        private int currentCustomerIndex = 0;
        private Random rng = new Random();

        // Customer roster with authentic Filipino canteen personalities and orders
        private readonly CustomerProfile[] customerRoster = new CustomerProfile[]
        {
            new CustomerProfile
            {
                Name = "Mang Berting",
                Title = "Karpintero",
                OrderText = "Pabili po! Dalawang mainit na pandesal at malamig na tubig para sa meryenda!",
                Quotes = new[]
                {
                    "Mainit sa buhos kanina sa construction site, buti bukas ang kantina ninyo!",
                    "Ang sarap ng meryenda dito, laging paborito ng mga kasamahan ko sa trabaho!",
                    "Sipag at tiyaga lang araw-araw para sa pamilya. Salamat sa mainit na pag-asikaso!"
                },
                ThankText = "Salamat bossing! Pampalakas bago bumalik sa construction!",
                Points = 50,
                CashReward = 30
            },
            new CustomerProfile
            {
                Name = "Boyet",
                Title = "Jeepney Driver",
                OrderText = "Ate/Kuya! Isang malamig na melon juice at dalawang turon bago mag-rush hour pasada!",
                Quotes = new[]
                {
                    "Grabe ang traffic sa kanto kanina, buti nakapag-break muna para magmeryenda!",
                    "Barya lang sa umaga, pero busog ang biyahe kapag dito kumain!",
                    "Matagal pa bago mag-garahe, kailangan ng pampalamig!"
                },
                ThankText = "Ayos na ayos! Tuloy ang pasada, maraming salamat!",
                Points = 55,
                CashReward = 35
            },
            new CustomerProfile
            {
                Name = "Kuya Jun",
                Title = "Delivery Rider",
                OrderText = "Bossing, mainit na kape at dalawang empanada! Dami pang idedeliver ngayong hapon!",
                Quotes = new[]
                {
                    "Ulan man o araw, tuloy ang arangkada para sa mga parcels!",
                    "Suki na ako rito, mabilis lagi ang serbisyo ninyo kaya 'di nahuhuli sa deliveries!",
                    "Kailangan ng pampagising sa kalsada!"
                },
                ThankText = "Maraming salamat bossing! Ride safe sa ating lahat!",
                Points = 60,
                CashReward = 40
            },
            new CustomerProfile
            {
                Name = "Aling Nena",
                Title = "Palengke Vendor",
                OrderText = "Isang mangkok ng mainit na sopas at toasted bread, pampalakas ng boses!",
                Quotes = new[]
                {
                    "Mura at napakasarap ng tinda ninyo, panalo sa lasa ng masa!",
                    "Maghapon akong sumisigaw sa tapat ng gulayan, kailangan ng mainit na higop!",
                    "Basta masarap ang timpla, laging babalik-balikan ng mga suki!"
                },
                ThankText = "Napakasarap talaga! Pagpalain ang inyong masarap na tindahan!",
                Points = 50,
                CashReward = 30
            },
            new CustomerProfile
            {
                Name = "Totoy",
                Title = "Estudyante",
                OrderText = "Ate/Kuya, isang order ng paboritong pansit canton at juice po! Gutom na galing school!",
                Quotes = new[]
                {
                    "Pahirapan ang quiz namin kanina sa Math, pero solve ang gutom dito!",
                    "Dito lagi kami tumatambay ng mga kaklase ko kapag uwian!",
                    "Dagdag energy bago magbasa ng reviewer mamayang gabi!"
                },
                ThankText = "Yehey! Busog na naman, maraming salamat po!",
                Points = 45,
                CashReward = 25
            },
            new CustomerProfile
            {
                Name = "Mang Tomas",
                Title = "Tricycle Driver",
                OrderText = "Pabili po ng isang banana cue at sago't gulaman boss, pampawi ng matinding init!",
                Quotes = new[]
                {
                    "Tirik ang araw sa paradahan ng tricycle kanina, kailangan ng pampalamig!",
                    "Basta masipag sa pasada, tiyak may maiuuwing biyaya sa pamilya!",
                    "Mabilis pa sa kidlat ang serbisyo dito sa kantina ninyo!"
                },
                ThankText = "Bilis ng serbisyo bossing! Saludo ako sa inyo!",
                Points = 50,
                CashReward = 30
            },
            new CustomerProfile
            {
                Name = "Officer Dante",
                Title = "Security Guard",
                OrderText = "Isang mainit na lugaw na may itlog bago mag-duty sa night shift mamaya!",
                Quotes = new[]
                {
                    "Handang magbantay buong magdamag, may pampainit na ng tiyan!",
                    "Laging malinis at maasikaso ang kantina ninyo, saludo sa inyong sipag!",
                    "Importante ang sapat na sustansya para alerto sa pagroronda!"
                },
                ThankText = "Maraming salamat! Bantay-sarado ang ating komunidad!",
                Points = 65,
                CashReward = 45
            },
            new CustomerProfile
            {
                Name = "Ate Sarah",
                Title = "Call Center Agent",
                OrderText = "Isang matapang na kape at toasted cheese sandwich bossing bago mag-log in sa shift!",
                Quotes = new[]
                {
                    "Pang-night shift ang laban, kailangan ng pampagising sa utak!",
                    "Mabuti na lang bukas ang paborito kong canteen, panalo ang kape niyo!",
                    "Ready nang humarap sa calls pagkatapos ng masarap na meryenda!"
                },
                ThankText = "Pampagising talaga! Salamat sa mabilis na serbisyo!",
                Points = 60,
                CashReward = 35
            }
        };

        public ENDLESS()
        {


            // Run runtime-only optimizations only when not in Visual Studio Designer
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                this.DoubleBuffered = true;
                this.SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw,
                    true);
                this.UpdateStyles();

                try
                {
                    typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.SetValue(ConstructionWorker, true, null);
                }
                catch { }
            }
        }

        public ENDLESS(MAIN main) : this()
        {
            this.mainForm = main;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Apply WS_EX_COMPOSITED only at runtime, never at design-time
                if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    cp.ExStyle |= 0x02000000;
                }
                return cp;
            }
        }

        private void ENDLESS_Load(object sender, EventArgs e)
        {
            // 1. Pre-render the 1024x576 endless mode background once
            PreRenderBackground();

            // 2. Setup interactive button hover animations
            AttachButtonHover(btnBack, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 220, 53, 69));
            AttachButtonHover(btnRestart, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 255, 140, 0));
            AttachButtonHover(btnServe, Color.FromArgb(40, 167, 69), Color.FromArgb(46, 204, 113));
            AttachButtonHover(btnTalk, Color.FromArgb(23, 162, 184), Color.FromArgb(52, 152, 219));

            // 3. Reset HUD and begin endless wave
            ResetEndlessSession();
        }

        private void PreRenderBackground()
        {
            try
            {
                if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;
                Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height, PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(Properties.Resources.endless_mode_bg, 0, 0, ClientSize.Width, ClientSize.Height);
                }
                cachedBackground?.Dispose();
                cachedBackground = bmp;

                this.BackgroundImage = cachedBackground;
                this.BackgroundImageLayout = ImageLayout.None; // 1:1 direct blit: zero background jitter or stretch during paint
            }
            catch { }
        }

        private void ResetEndlessSession()
        {
            servedCount = 0;
            currentStreak = 0;
            totalScore = 0;
            UpdateHUD();
            SpawnNextCustomer();
        }

        private void SpawnNextCustomer()
        {
            pnlDialogue.Visible = false;
            lblStatus.Text = "";
            btnServe.Enabled = true;
            btnServe.BackColor = Color.FromArgb(40, 167, 69);

            // Select next customer
            currentCustomerIndex = rng.Next(customerRoster.Length);
            talkCycleIndex = 0;
            CustomerProfile customer = customerRoster[currentCustomerIndex];

            lblSpeaker.Text = $"{customer.Name} ({customer.Title})";
            lblDialogue.Text = customer.OrderText;

            // In endless mode, customers appear quicker! Speed increases slightly as streak builds up
            currentSpeed = Math.Min(9.0f, baseSpeed + (currentStreak * 0.15f));

            // Spawn worker from off-screen right
            currentWorkerX = ClientSize.Width + 20;
            stepCount = 0;
            currentState = WorkerState.WalkingIn;
            ConstructionWorker.Location = new Point((int)currentWorkerX, baseY);

            Walktimer.Interval = 16; // Silky-smooth 60 FPS
            Walktimer.Start();
        }

        private void Walktimer_Tick(object sender, EventArgs e)
        {
            if (currentState == WorkerState.WalkingIn)
            {
                // Customer walking quickly to the counter
                if (currentWorkerX > targetPosition)
                {
                    currentWorkerX -= currentSpeed;
                    stepCount++;

                    // Natural walking step bobbing (keeps torso attached to counter)
                    int bob = (int)(Math.Sin(stepCount * 0.40) * 1.2);

                    if (currentWorkerX <= targetPosition)
                    {
                        currentWorkerX = targetPosition;
                        ConstructionWorker.Location = new Point((int)currentWorkerX, baseY);
                        currentState = WorkerState.AtCounter;
                        Walktimer.Stop();
                        OnCustomerArrivedAtCounter();
                    }
                    else
                    {
                        ConstructionWorker.Location = new Point((int)currentWorkerX, baseY + bob);
                    }
                }
            }
            else if (currentState == WorkerState.WalkingOut)
            {
                // Customer quickly walks away to the left to let the next customer arrive promptly
                currentWorkerX -= (currentSpeed * 1.35f);
                stepCount++;
                int bob = (int)(Math.Sin(stepCount * 0.45) * 2);

                if (currentWorkerX < -200)
                {
                    Walktimer.Stop();
                    // Immediately spawn the next customer in the endless rush!
                    SpawnNextCustomer();
                }
                else
                {
                    ConstructionWorker.Location = new Point((int)currentWorkerX, baseY + bob);
                }
            }
        }

        private void OnCustomerArrivedAtCounter()
        {
            pnlDialogue.Visible = true;
            lblStatus.Text = "Ready for order";
            btnServe.Enabled = true;
            btnServe.BackColor = Color.FromArgb(40, 167, 69);
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            if (currentState != WorkerState.AtCounter) return;

            CustomerProfile customer = customerRoster[currentCustomerIndex];

            // Calculate score and rewards
            servedCount++;
            currentStreak++;
            int streakBonus = (currentStreak - 1) * 10;
            int pointsEarned = customer.Points + streakBonus;
            int cashEarned = customer.CashReward + (currentStreak * 2);

            totalScore += pointsEarned;


            // Update dialogue and visual feedback
            lblDialogue.Text = customer.ThankText;
            lblStatus.Text = currentStreak > 1
                ? $"⭐ +{pointsEarned} pts! 🔥 Streak x{currentStreak}!"
                : $"⭐ +{pointsEarned} pts!";

            btnServe.Enabled = false;
            btnServe.BackColor = Color.FromArgb(108, 117, 125);

            UpdateHUD();

            // Swiftly begin walking out after a short acknowledgement pause
            currentState = WorkerState.WalkingOut;
            stepCount = 0;
            Walktimer.Start();
        }

        private void btnTalk_Click(object sender, EventArgs e)
        {
            if (currentState == WorkerState.AtCounter)
            {
                CustomerProfile customer = customerRoster[currentCustomerIndex];
                if (customer.Quotes.Length > 0)
                {
                    lblDialogue.Text = customer.Quotes[talkCycleIndex % customer.Quotes.Length];
                    talkCycleIndex++;
                }
            }
        }

        private void ConstructionWorker_Click(object sender, EventArgs e)
        {
            if (currentState == WorkerState.AtCounter)
            {
                btnTalk_Click(sender, e);
            }
        }

        private void UpdateHUD()
        {
            lblServed.Text = $"👥 Served: {servedCount}";
            lblStreak.Text = $"🔥 Streak: {currentStreak}";
            lblScore.Text = $"⭐ Score: {totalScore}";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetEndlessSession();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
            this.Close();
        }

        private void AttachButtonHover(Button btn, Color normalColor, Color hoverColor)
        {
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) =>
            {
                if (btn.Enabled)
                    btn.BackColor = normalColor;
            };
        }

      
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Walktimer.Stop();
            cachedBackground?.Dispose();
            cachedBackground = null;

            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
            else
            {
                Application.Exit();
            }
        }
    }

    public enum WorkerState
    {
        WalkingIn,
        AtCounter,
        WalkingOut
    }

    public class CustomerProfile
    {
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string OrderText { get; set; } = "";
        public string[] Quotes { get; set; } = Array.Empty<string>();
        public string ThankText { get; set; } = "";
        public int Points { get; set; } = 50;
        public int CashReward { get; set; } = 25;
    }
}
