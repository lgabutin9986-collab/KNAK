using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace KNAK
{
    // =========================================================
    // 1. FORM CLASS (MAIN GAME FORM)
    // =========================================================
    public partial class GAME : Form
    {
        private MAIN? mainForm;
        private Bitmap? cachedBackground;

        // Image & Size storage for face swapping
        private Image? originalWorkerImage;
        private Size originalWorkerSize;

        private const int baseY = 14;   // Standard Y position for customers

        // --- 3 CUSTOMER SLOTS & RANDOM SPAWNING ---
        private CustomerSlot[] slots = new CustomerSlot[3];
        private readonly int[] slotTargetPositions = new int[] { 110, 385, 660 }; // Left, Center, Right slots
        private float speed = 3.2f; // Walk speed

        private readonly System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();
        private List<CustomerProfile> customerPool = new List<CustomerProfile>();
        private Random rand = new Random();

        // --- CORE OBJECTIVE & CONDITIONS ---
        private int currentRevenue = 0;
        private const int TargetRevenue = 500; // Quota: ₱500
        private int walkoutCount = 0;
        private const int MaxWalkouts = 3;     // 3 walkouts = Game Over
        private int customersServed = 0;
        private bool isMatchOver = false;

        // --- PAUSE MECHANICS STATE & DESIGNER CONTROLS ---
        private bool isPaused = false;
        private Control? ctrlPauseBox;
        private Control? ctrlBtnResume;
        private Control? ctrlRestart;
        private Control? ctrlMainMenu;

        // --- SHIFT CLEARED DESIGNER CONTROLS ---
        private Control? ctrlShiftBox;
        private Control? ctrlNextDay;
        private Control? ctrlShowQuota;
        private readonly Label lblQuotaTextDisplay = new Label();

        // Shift Timer
        private const int ShiftTotalSeconds = 120;
        private int shiftSecondsLeft = 120;
        private readonly System.Windows.Forms.Timer shiftTimer = new System.Windows.Forms.Timer();

        // Patience & Respawn Timers
        private const float MaxPatienceSeconds = 15.0f;
        private readonly System.Windows.Forms.Timer patienceTimer = new System.Windows.Forms.Timer();
        private const int MealBasePrice = 50;

        // HUD & Toast Controls
        private readonly Label lblTimerHud = new Label();
        private readonly Label lblRevenueHud = new Label();
        private readonly Label lblWalkoutsHud = new Label();

        private readonly Label lblFloatingToast = new Label();
        private readonly System.Windows.Forms.Timer toastTimer = new System.Windows.Forms.Timer();
        private int toastTicks = 0;

        // Game Over Result Modal (for Failure)
        private readonly Panel pnlResultModal = new Panel();
        private readonly Label lblModalTitle = new Label();
        private readonly Label lblModalSubtitle = new Label();
        private readonly Label lblModalStats = new Label();
        private readonly Button btnModalRestart = new Button();
        private readonly Button btnModalBack = new Button();

        public GAME()
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

            shiftTimer.Interval = 1000;
            shiftTimer.Tick += ShiftTimer_Tick;

            patienceTimer.Interval = 100;
            patienceTimer.Tick += PatienceTimer_Tick;

            spawnTimer.Interval = 5000;
            spawnTimer.Tick += SpawnTimer_Tick;

            toastTimer.Interval = 40;
            toastTimer.Tick += ToastTimer_Tick;

            Walktimer.Interval = 16; // ~60 FPS
            Walktimer.Tick += Walktimer_Tick;
        }

        public GAME(MAIN main) : this()
        {
            mainForm = main;
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

        private void GAME_Load(object sender, EventArgs e)
        {
            if (picFood != null) picFood.Visible = false;
            if (picBubbleChat != null) picBubbleChat.Visible = false;

            // ITAGO ANG DALAWA DAHIL SA PAUSE MENU NA ANG CONTROL

            originalWorkerSize = picCustomer.Size;
            originalWorkerImage = picCustomer.Image;

            InitializeCustomerPool();
            SetupCustomerSlotsUI();

            PreRenderBackground();

            // INGREDIENTS CLICK WITH PAUSE GUARD
            pboxEggsBasket.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🥚 Sariwang itlog ng pugo!", Color.FromArgb(245, 158, 11)); };
            pboxBatterBowl.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🥣 Kwek-kwek batter ready!", Color.FromArgb(249, 115, 22)); };
            pboxMeatBowl.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🥩 Malinamnam na beef pares!", Color.FromArgb(239, 68, 68)); };
            pboxFryer.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🔥 Mainit na mantika!", Color.FromArgb(234, 179, 8)); };
            pboxPares.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🍲 Kumukulong pares broth!", Color.FromArgb(244, 63, 94)); };
            pboxTray.Click += (s, e) => { if (!isPaused && !isMatchOver) ShowFloatingToast("🍱 Serving tray!", Color.FromArgb(148, 163, 184)); };

            // ATTACH CONTROLS
            SetupPauseIconControl();
            SetupPauseDesignerControls();
            SetupShiftClearDesignerControls();

            BuildInMatchHud();
            BuildFloatingToast();
            BuildResultModal();

            ResetGameMatch();
        }

        // --- HOOK PAUSEICON FUNCTIONALITY ---
        private void SetupPauseIconControl()
        {
            try
            {
                Control? pIcon = Controls.Find("pauseicon", true).FirstOrDefault();
                if (pIcon != null)
                {
                    pIcon.Cursor = Cursors.Hand;
                    pIcon.Click += (s, e) => TogglePauseGame();
                    pIcon.BringToFront();
                }
            }
            catch { }
        }

        // --- BIND PAUSE MENU CONTROLS ---
        private void SetupPauseDesignerControls()
        {
            try
            {
                ctrlPauseBox = Controls.Find("pausebox", true).FirstOrDefault();
                ctrlBtnResume = Controls.Find("btnresume", true).FirstOrDefault();
                ctrlRestart = Controls.Find("restart", true).FirstOrDefault();
                ctrlMainMenu = Controls.Find("mainmenu", true).FirstOrDefault();

                if (ctrlBtnResume != null)
                {
                    ctrlBtnResume.Cursor = Cursors.Hand;
                    ctrlBtnResume.Click += (s, e) => ResumeGame();
                }

                if (ctrlRestart != null)
                {
                    ctrlRestart.Cursor = Cursors.Hand;
                    ctrlRestart.Click += (s, e) => { ResumeGame(); ResetGameMatch(); };
                }

                if (ctrlMainMenu != null)
                {
                    ctrlMainMenu.Cursor = Cursors.Hand;
                    ctrlMainMenu.Click += (s, e) => btnBack_Click(s, e);
                }

                SetPauseUIVisibility(false);
            }
            catch { }
        }

        private void SetPauseUIVisibility(bool visible)
        {
            if (ctrlPauseBox != null) ctrlPauseBox.Visible = visible;
            if (ctrlBtnResume != null) ctrlBtnResume.Visible = visible;
            if (ctrlRestart != null) ctrlRestart.Visible = visible;
            if (ctrlMainMenu != null) ctrlMainMenu.Visible = visible;

            if (visible)
            {
                ctrlPauseBox?.BringToFront();
                ctrlBtnResume?.BringToFront();
                ctrlRestart?.BringToFront();
                ctrlMainMenu?.BringToFront();
            }
        }

        // --- BIND SHIFT CLEARED CONTROLS (shiftbox, nextday1, showquota) ---
        private void SetupShiftClearDesignerControls()
        {
            try
            {
                ctrlShiftBox = Controls.Find("shiftbox", true).FirstOrDefault();
                ctrlNextDay = Controls.Find("nextday1", true).FirstOrDefault();
                ctrlShowQuota = Controls.Find("showquota", true).FirstOrDefault();

                if (ctrlNextDay != null)
                {
                    ctrlNextDay.Cursor = Cursors.Hand;
                    ctrlNextDay.Click += (s, e) =>
                    {
                        SetShiftClearUIVisibility(false);
                        ResetGameMatch();
                    };
                }

                if (ctrlShowQuota != null)
                {
                    lblQuotaTextDisplay.Parent = ctrlShowQuota;
                    lblQuotaTextDisplay.Dock = DockStyle.Fill;
                    lblQuotaTextDisplay.BackColor = Color.Transparent;
                    lblQuotaTextDisplay.ForeColor = Color.FromArgb(255, 215, 0); // Gold Text
                    lblQuotaTextDisplay.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                    lblQuotaTextDisplay.TextAlign = ContentAlignment.MiddleCenter;
                    lblQuotaTextDisplay.Text = "₱0";
                }

                SetShiftClearUIVisibility(false);
            }
            catch { }
        }

        private void SetShiftClearUIVisibility(bool visible)
        {
            if (ctrlShiftBox != null) ctrlShiftBox.Visible = visible;
            if (ctrlNextDay != null) ctrlNextDay.Visible = visible;
            if (ctrlShowQuota != null) ctrlShowQuota.Visible = visible;

            if (visible)
            {
                ctrlShiftBox?.BringToFront();
                ctrlShowQuota?.BringToFront();
                ctrlNextDay?.BringToFront();
            }
        }

        // --- PAUSE & RESUME LOGIC ---
        private void TogglePauseGame()
        {
            if (isMatchOver) return;

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            if (isMatchOver || isPaused) return;

            isPaused = true;

            // Ihinto ang lahat ng timers
            shiftTimer.Stop();
            patienceTimer.Stop();
            spawnTimer.Stop();
            Walktimer.Stop();

            SetPauseUIVisibility(true);
            ApplyZOrder();
        }

        private void ResumeGame()
        {
            if (!isPaused) return;

            isPaused = false;
            SetPauseUIVisibility(false);

            // Ipagpatuloy ang mga kinakailangang timers
            shiftTimer.Start();
            spawnTimer.Start();

            bool anyArrived = slots.Any(s => s != null && s.IsOccupied && s.IsArrived && !s.IsLeaving);
            if (anyArrived)
            {
                patienceTimer.Start();
            }

            bool anyMoving = slots.Any(s => s != null && s.IsOccupied && ((!s.IsArrived) || s.IsLeaving));
            if (anyMoving)
            {
                Walktimer.Start();
            }

            ApplyZOrder();
        }

        private void InitializeCustomerPool()
        {
            customerPool.Clear();

            // 1. Worker
            customerPool.Add(new CustomerProfile
            {
                Name = "Worker",
                NormalImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\worker_normal_face - removebg - preview.png"),
                AngryImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\worker angry face.png")
            });

            // 2. Mother
            customerPool.Add(new CustomerProfile
            {
                Name = "Mother",
                NormalImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\mother normal face.png"),
                AngryImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\mother angry face.png")
            });

            // 3. Student
            customerPool.Add(new CustomerProfile
            {
                Name = "Student",
                NormalImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\student normal face.png"),
                AngryImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\student angry face.png")
            });

            // 4. Teacher
            customerPool.Add(new CustomerProfile
            {
                Name = "Teacher",
                NormalImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\teacher normal face.png"),
                AngryImage = TryLoadImage(@"C:\Users\Levin Gabutin\Downloads\teacher angry face.png")
            });
        }

        private Image? TryLoadImage(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch { }
            return originalWorkerImage;
        }

        private Image GetTapsilogImage()
        {
            if (picFood != null && picFood.Image != null)
                return picFood.Image;

            Image? embedded = LoadOrderImage();
            if (embedded != null)
                return embedded;

            return Properties.Resources.story_mode_bg;
        }

        // --- SETUP KONSISTENT AT BALANSENG SPEECH BUBBLE & PICFOOD ---
        private void SetupCustomerSlotsUI()
        {
            Image tapsilogImg = GetTapsilogImage();

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = new CustomerSlot();
                slot.SlotIndex = i;
                slot.TargetX = slotTargetPositions[i];

                if (i == 0)
                {
                    slot.PicCustomer = picCustomer;
                }
                else
                {
                    slot.PicCustomer = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand,
                        Visible = false
                    };
                    Controls.Add(slot.PicCustomer);
                }

                slot.PicCustomer.Size = new Size(260, 255);
                slot.PicCustomer.SizeMode = PictureBoxSizeMode.Normal;
                slot.PicCustomer.Controls.Clear();

                // COMPACT AT PANTAY NA SPEECH BUBBLE (135x105)
                Panel bubble = new Panel
                {
                    Size = new Size(135, 105),
                    Location = new Point(100, 5),
                    BackgroundImage = Properties.Resources.speech_bubble_,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand,
                    Visible = false
                };

                // PICFOOD (55x55) - NAKAGITNANG-GITNA SA PUTING BUBBLE AREA
                PictureBox pboxFood = new PictureBox
                {
                    Image = tapsilogImg,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(55, 55),
                    Location = new Point(38, 16),
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand
                };

                bubble.Controls.Add(pboxFood);

                // PATIENCE BAR BELOW THE BUBBLE
                Panel patienceBar = new Panel
                {
                    Size = new Size(120, 20),
                    Location = new Point(100, 115),
                    BackColor = Color.Transparent,
                    Visible = false
                };

                slot.PicCustomer.Controls.Add(bubble);
                slot.PicCustomer.Controls.Add(patienceBar);

                slot.PnlBubble = bubble;
                slot.PnlPatienceBar = patienceBar;

                int index = i;
                slot.PicCustomer.Click += (s, e) => TryServeSlot(index);
                bubble.Click += (s, e) => TryServeSlot(index);
                pboxFood.Click += (s, e) => TryServeSlot(index);

                patienceBar.Paint += (s, e) => DrawSlotPatienceBar(e.Graphics, slots[index]);

                slots[i] = slot;
            }
        }

        // --- SPAWN TIMER TICK ---
        private void SpawnTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver || isPaused) return;

            for (int i = 0; i < 3; i++)
            {
                if (!slots[i].IsOccupied)
                {
                    SpawnCustomerInSlot(slots[i]);
                    break;
                }
            }
        }

        private void SpawnCustomerInSlot(CustomerSlot slot)
        {
            slot.Profile = customerPool[rand.Next(customerPool.Count)];
            slot.IsOccupied = true;
            slot.IsArrived = false;
            slot.IsLeaving = false;
            slot.IsAngry = false;

            slot.CurrentPatience = MaxPatienceSeconds;
            slot.StepCount = 0;

            slot.SpawnFromLeft = (rand.Next(0, 2) == 0);
            slot.LeaveToRight = (rand.Next(0, 2) == 0);

            slot.CurrentX = slot.SpawnFromLeft ? -180 : 1100;
            slot.PicCustomer.Location = new Point((int)slot.CurrentX, baseY);

            if (slot.Profile.NormalImage != null)
            {
                using (Bitmap bmp = new Bitmap(slot.Profile.NormalImage))
                {
                    Bitmap trimmed = TrimTransparentEdges(bmp);
                    slot.PicCustomer.Image = new Bitmap(trimmed, originalWorkerSize);
                }
            }

            slot.PicCustomer.Visible = true;
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            if (!Walktimer.Enabled && !isPaused)
                Walktimer.Start();

            ApplyZOrder();
        }

        // --- WALKING ANIMATION LOGIC ---
        private void Walktimer_Tick(object? sender, EventArgs e)
        {
            if (isPaused) return;

            bool anyMoving = false;

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = slots[i];
                if (!slot.IsOccupied) continue;

                if (!slot.IsLeaving)
                {
                    if (slot.SpawnFromLeft)
                    {
                        if (slot.CurrentX < slot.TargetX)
                        {
                            slot.CurrentX += speed;
                            slot.StepCount++;
                            int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                            slot.CurrentX = Math.Min(slot.CurrentX, slot.TargetX);

                            slot.PicCustomer.Location = new Point((int)slot.CurrentX, baseY + bob);
                            anyMoving = true;

                            if (slot.CurrentX >= slot.TargetX)
                            {
                                slot.IsArrived = true;
                                OnSlotArrivedAtCounter(slot);
                            }
                        }
                    }
                    else
                    {
                        if (slot.CurrentX > slot.TargetX)
                        {
                            slot.CurrentX -= speed;
                            slot.StepCount++;
                            int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                            slot.CurrentX = Math.Max(slot.CurrentX, slot.TargetX);

                            slot.PicCustomer.Location = new Point((int)slot.CurrentX, baseY + bob);
                            anyMoving = true;

                            if (slot.CurrentX <= slot.TargetX)
                            {
                                slot.IsArrived = true;
                                OnSlotArrivedAtCounter(slot);
                            }
                        }
                    }
                }
                else
                {
                    if (slot.LeaveToRight)
                    {
                        slot.CurrentX += speed;
                        slot.StepCount++;
                        int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);

                        slot.PicCustomer.Location = new Point((int)slot.CurrentX, baseY + bob);
                        anyMoving = true;

                        if (slot.CurrentX >= 1100)
                        {
                            ResetSlotState(slot);
                        }
                    }
                    else
                    {
                        slot.CurrentX -= speed;
                        slot.StepCount++;
                        int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);

                        slot.PicCustomer.Location = new Point((int)slot.CurrentX, baseY + bob);
                        anyMoving = true;

                        if (slot.CurrentX <= -220)
                        {
                            ResetSlotState(slot);
                        }
                    }
                }
            }

            if (!anyMoving)
            {
                Walktimer.Stop();
            }
        }

        private void ResetSlotState(CustomerSlot slot)
        {
            slot.IsOccupied = false;
            slot.IsArrived = false;
            slot.IsLeaving = false;
            slot.PicCustomer.Visible = false;
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;
        }

        private void OnSlotArrivedAtCounter(CustomerSlot slot)
        {
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = true;
            slot.PnlPatienceBar.Visible = true;

            if (!patienceTimer.Enabled && !isPaused)
                patienceTimer.Start();

            ApplyZOrder();
        }

        // --- PATIENCE & ANGRY FACE SYSTEM ---
        private void PatienceTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver || isPaused) return;

            bool anyArrived = false;

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = slots[i];
                if (!slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) continue;

                anyArrived = true;
                slot.CurrentPatience = Math.Max(0, slot.CurrentPatience - 0.1f);
                slot.PnlPatienceBar.Invalidate();

                float ratio = slot.CurrentPatience / MaxPatienceSeconds;

                if (ratio < 0.25f && !slot.IsAngry)
                {
                    slot.IsAngry = true;
                    ApplyAngryFaceToSlot(slot);
                }

                if (slot.CurrentPatience <= 0)
                {
                    HandleSlotWalkOut(slot);
                }
            }

            if (!anyArrived)
            {
                patienceTimer.Stop();
            }
        }

        private void ApplyAngryFaceToSlot(CustomerSlot slot)
        {
            if (slot.Profile?.AngryImage != null)
            {
                using (Bitmap bmp = new Bitmap(slot.Profile.AngryImage))
                {
                    Bitmap trimmed = TrimTransparentEdges(bmp);
                    slot.PicCustomer.Image = new Bitmap(trimmed, originalWorkerSize);
                }
            }
            ApplyZOrder();
        }

        private void TryServeSlot(int slotIndex)
        {
            CustomerSlot slot = slots[slotIndex];
            if (isMatchOver || isPaused || !slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) return;

            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            float ratio = slot.CurrentPatience / MaxPatienceSeconds;
            int payout;
            string feedbackText;
            Color feedbackColor;

            if (ratio >= 0.60f)
            {
                payout = MealBasePrice + 20;
                feedbackText = "+₱70 ⭐ (+₱20 Tip!)";
                feedbackColor = Color.FromArgb(34, 197, 94);
            }
            else if (ratio >= 0.25f)
            {
                payout = MealBasePrice;
                feedbackText = "+₱50";
                feedbackColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                payout = Math.Max(0, MealBasePrice - 10);
                feedbackText = "+₱40 (-₱10 Disc)";
                feedbackColor = Color.FromArgb(249, 115, 22);
            }

            currentRevenue += payout;
            customersServed++;
            UpdateHudDisplay();

            ShowFloatingToast(feedbackText, feedbackColor, (int)slot.CurrentX);

            if (currentRevenue >= TargetRevenue)
            {
                HandleVictory();
                return;
            }

            slot.IsLeaving = true;
            if (!Walktimer.Enabled && !isPaused)
                Walktimer.Start();
        }

        private void HandleSlotWalkOut(CustomerSlot slot)
        {
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            walkoutCount++;
            currentRevenue = Math.Max(0, currentRevenue - 50);
            UpdateHudDisplay();

            ShowFloatingToast("-₱50 Penalty! 💔 Walkout", Color.FromArgb(239, 68, 68), (int)slot.CurrentX);

            if (walkoutCount >= MaxWalkouts)
            {
                HandleGameOver("3 customers walked out!");
                return;
            }

            slot.IsLeaving = true;
            if (!Walktimer.Enabled && !isPaused)
                Walktimer.Start();
        }

        private void DrawSlotPatienceBar(Graphics g, CustomerSlot slot)
        {
            if (!slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = slot.PnlPatienceBar.Width;
            int h = slot.PnlPatienceBar.Height;
            Rectangle barRect = new Rectangle(0, 0, w - 1, h - 1);

            using (GraphicsPath bgPath = GetRoundedRectanglePath(barRect, 8))
            {
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(230, 15, 23, 42)))
                {
                    g.FillPath(bgBrush, bgPath);
                }
                using (Pen borderPen = new Pen(Color.FromArgb(71, 85, 105), 1.2f))
                {
                    g.DrawPath(borderPen, bgPath);
                }
            }

            float ratio = Math.Max(0f, Math.Min(1f, slot.CurrentPatience / MaxPatienceSeconds));
            int fillW = Math.Max(0, (int)((w - 4) * ratio));

            if (fillW > 4)
            {
                Rectangle fillRect = new Rectangle(2, 2, fillW, h - 4);
                Color c1, c2;
                string zoneText;

                if (ratio >= 0.60f)
                {
                    c1 = Color.FromArgb(34, 197, 94);
                    c2 = Color.FromArgb(22, 163, 74);
                    zoneText = "😊 +₱20 TIP";
                }
                else if (ratio >= 0.25f)
                {
                    c1 = Color.FromArgb(245, 158, 11);
                    c2 = Color.FromArgb(217, 119, 6);
                    zoneText = "😐 ₱50 Base";
                }
                else
                {
                    c1 = Color.FromArgb(239, 68, 68);
                    c2 = Color.FromArgb(220, 38, 38);
                    zoneText = "😠 -₱10 DISC";
                }

                using (GraphicsPath fillPath = GetRoundedRectanglePath(fillRect, 6))
                using (LinearGradientBrush fillBrush = new LinearGradientBrush(fillRect, c1, c2, LinearGradientMode.Vertical))
                {
                    g.FillPath(fillBrush, fillPath);
                }

                using (Font font = new Font("Segoe UI", 7.5F, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(zoneText, font, textBrush, barRect, sf);
                }
            }
        }

        // --- RESET MATCH & INITIALIZE CUSTOMERS ---
        private void ResetGameMatch()
        {
            pnlResultModal.Visible = false;
            SetPauseUIVisibility(false);
            SetShiftClearUIVisibility(false);

            isMatchOver = false;
            isPaused = false;

            currentRevenue = 0;
            walkoutCount = 0;
            customersServed = 0;
            shiftSecondsLeft = ShiftTotalSeconds;

            shiftTimer.Stop();
            patienceTimer.Stop();
            spawnTimer.Stop();
            toastTimer.Stop();
            Walktimer.Stop();

            for (int i = 0; i < 3; i++)
            {
                if (slots[i] != null)
                {
                    ResetSlotState(slots[i]);
                }
            }

            UpdateHudDisplay();

            shiftTimer.Start();
            spawnTimer.Start();

            SpawnCustomerInSlot(slots[0]);
        }

        private void BuildInMatchHud()
        {
            lblStage.Location = new Point(362, 0);
            lblStage.Size = new Size(276, 34);
            lblStage.Text = "STAGE 1  •  QUOTA: ₱500";
            lblStage.BackColor = Color.FromArgb(220, 20, 24, 33);
            lblStage.ForeColor = Color.FromArgb(255, 193, 7);
            lblStage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStage.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(lblStage);

            lblTimerHud.Location = new Point(650, 12);
            lblTimerHud.Size = new Size(105, 36);
            lblTimerHud.BackColor = Color.FromArgb(220, 20, 24, 33);
            lblTimerHud.ForeColor = Color.FromArgb(255, 215, 0);
            lblTimerHud.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTimerHud.TextAlign = ContentAlignment.MiddleCenter;
            lblTimerHud.Text = "⏱️ 02:00";
            Controls.Add(lblTimerHud);

            lblRevenueHud.Location = new Point(763, 12);
            lblRevenueHud.Size = new Size(145, 36);
            lblRevenueHud.BackColor = Color.FromArgb(220, 20, 24, 33);
            lblRevenueHud.ForeColor = Color.FromArgb(74, 222, 128);
            lblRevenueHud.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRevenueHud.TextAlign = ContentAlignment.MiddleCenter;
            lblRevenueHud.Text = "💰 ₱0 / ₱500";
            Controls.Add(lblRevenueHud);

            lblWalkoutsHud.Location = new Point(916, 12);
            lblWalkoutsHud.Size = new Size(94, 36);
            lblWalkoutsHud.BackColor = Color.FromArgb(220, 20, 24, 33);
            lblWalkoutsHud.ForeColor = Color.FromArgb(248, 113, 113);
            lblWalkoutsHud.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblWalkoutsHud.TextAlign = ContentAlignment.MiddleCenter;
            lblWalkoutsHud.Text = "💔 0 / 3";
            Controls.Add(lblWalkoutsHud);
        }

        private void BuildFloatingToast()
        {
            lblFloatingToast.AutoSize = false;
            lblFloatingToast.Size = new Size(240, 36);
            lblFloatingToast.BackColor = Color.Transparent;
            lblFloatingToast.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFloatingToast.TextAlign = ContentAlignment.MiddleCenter;
            lblFloatingToast.Visible = false;
            Controls.Add(lblFloatingToast);
        }

        private void BuildResultModal()
        {
            pnlResultModal.Size = new Size(460, 330);
            pnlResultModal.Location = new Point((ClientSize.Width - 460) / 2, (ClientSize.Height - 330) / 2);
            pnlResultModal.BackColor = Color.FromArgb(245, 15, 23, 42);
            pnlResultModal.Visible = false;
            pnlResultModal.Paint += PnlResultModal_Paint;

            lblModalTitle.Location = new Point(20, 24);
            lblModalTitle.Size = new Size(420, 42);
            lblModalTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblModalTitle.ForeColor = Color.FromArgb(255, 215, 0);
            lblModalTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblModalTitle.BackColor = Color.Transparent;
            pnlResultModal.Controls.Add(lblModalTitle);

            lblModalSubtitle.Location = new Point(20, 68);
            lblModalSubtitle.Size = new Size(420, 32);
            lblModalSubtitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            lblModalSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
            lblModalSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblModalSubtitle.BackColor = Color.Transparent;
            pnlResultModal.Controls.Add(lblModalSubtitle);

            lblModalStats.Location = new Point(40, 110);
            lblModalStats.Size = new Size(380, 130);
            lblModalStats.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblModalStats.ForeColor = Color.White;
            lblModalStats.TextAlign = ContentAlignment.MiddleCenter;
            lblModalStats.BackColor = Color.FromArgb(160, 30, 41, 59);
            pnlResultModal.Controls.Add(lblModalStats);

            btnModalRestart.Location = new Point(60, 260);
            btnModalRestart.Size = new Size(160, 44);
            btnModalRestart.FlatStyle = FlatStyle.Flat;
            btnModalRestart.FlatAppearance.BorderSize = 0;
            btnModalRestart.BackColor = Color.FromArgb(34, 197, 94);
            btnModalRestart.ForeColor = Color.White;
            btnModalRestart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnModalRestart.Text = "↻ Play Again";
            btnModalRestart.Cursor = Cursors.Hand;
            btnModalRestart.Click += (s, e) => ResetGameMatch();
            AttachButtonHover(btnModalRestart, Color.FromArgb(34, 197, 94), Color.FromArgb(22, 163, 74));
            pnlResultModal.Controls.Add(btnModalRestart);

            btnModalBack.Location = new Point(240, 260);
            btnModalBack.Size = new Size(160, 44);
            btnModalBack.FlatStyle = FlatStyle.Flat;
            btnModalBack.FlatAppearance.BorderSize = 0;
            btnModalBack.BackColor = Color.FromArgb(100, 116, 139);
            btnModalBack.ForeColor = Color.White;
            btnModalBack.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnModalBack.Text = "← Main Menu";
            btnModalBack.Cursor = Cursors.Hand;
            btnModalBack.Click += btnBack_Click;
            AttachButtonHover(btnModalBack, Color.FromArgb(100, 116, 139), Color.FromArgb(71, 85, 105));
            pnlResultModal.Controls.Add(btnModalBack);

            Controls.Add(pnlResultModal);
        }

        private void PnlResultModal_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = GetRoundedRectanglePath(new Rectangle(0, 0, pnlResultModal.Width - 1, pnlResultModal.Height - 1), 16))
            {
                Color borderColor = Color.FromArgb(239, 68, 68);
                using (Pen pen = new Pen(borderColor, 2.5f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void ShiftTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver || isPaused) return;

            shiftSecondsLeft--;
            UpdateHudDisplay();

            if (shiftSecondsLeft <= 0)
            {
                shiftTimer.Stop();
                if (currentRevenue >= TargetRevenue)
                    HandleVictory();
                else
                    HandleGameOver("Shift ended before meeting the ₱500 quota!");
            }
        }

        private void ShowFloatingToast(string text, Color color, int posX = 400)
        {
            lblFloatingToast.Text = text;
            lblFloatingToast.ForeColor = color;
            lblFloatingToast.Location = new Point(posX, baseY + 140);
            lblFloatingToast.Visible = true;
            lblFloatingToast.BringToFront();

            toastTicks = 0;
            toastTimer.Stop();
            toastTimer.Start();
        }

        private void ToastTimer_Tick(object? sender, EventArgs e)
        {
            toastTicks++;
            lblFloatingToast.Top -= 2;

            if (toastTicks >= 25)
            {
                toastTimer.Stop();
                lblFloatingToast.Visible = false;
            }
        }

        private void UpdateHudDisplay()
        {
            int mins = shiftSecondsLeft / 60;
            int secs = shiftSecondsLeft % 60;
            lblTimerHud.Text = $"⏱️ {mins:00}:{secs:00}";
            lblRevenueHud.Text = $"💰 ₱{currentRevenue} / ₱{TargetRevenue}";
            lblWalkoutsHud.Text = $"💔 {walkoutCount} / {MaxWalkouts}";
        }

        // --- HANDLE VICTORY (SHIFT CLEARED WITH YOUR DESIGNER UI) ---
        private void HandleVictory()
        {
            isMatchOver = true;
            shiftTimer.Stop();
            patienceTimer.Stop();
            spawnTimer.Stop();
            Walktimer.Stop();

            // LALAGAY ANG NAIPONG HALAGA SA LIGHT BROWN SQUARE (showquota)
            lblQuotaTextDisplay.Text = $"₱{currentRevenue}";

            SetShiftClearUIVisibility(true);
            ApplyZOrder();
        }

        // --- HANDLE GAME OVER (SHIFT FAILED) ---
        private void HandleGameOver(string reason)
        {
            isMatchOver = true;
            shiftTimer.Stop();
            patienceTimer.Stop();
            spawnTimer.Stop();
            Walktimer.Stop();

            pnlResultModal.Visible = true;

            lblModalTitle.Text = "❌ SHIFT FAILED!";
            lblModalTitle.ForeColor = Color.FromArgb(239, 68, 68);
            lblModalSubtitle.Text = reason;

            lblModalStats.Text =
                $"💰 Final Revenue: ₱{currentRevenue} (Target: ₱{TargetRevenue})\n\n" +
                $"👥 Customers Served: {customersServed}\n" +
                $"💔 Walkouts: {walkoutCount} / {MaxWalkouts}\n" +
                $"⏱️ Shift Status: Quota not met";

            ApplyZOrder();
        }

        private void ApplyZOrder()
        {
            for (int i = 0; i < 3; i++)
            {
                if (slots[i] != null)
                {
                    slots[i].PicCustomer.BringToFront();
                }
            }

            pboxTray.BringToFront();
            pboxBatterBowl.BringToFront();
            pboxEggsBasket.BringToFront();
            pboxMeatBowl.BringToFront();
            pboxPares.BringToFront();
            pboxFryer.BringToFront();

            lblStage.BringToFront();
            lblTimerHud.BringToFront();
            lblRevenueHud.BringToFront();
            lblWalkoutsHud.BringToFront();
            lblFloatingToast.BringToFront();

            Control? pIcon = Controls.Find("pauseicon", true).FirstOrDefault();
            if (pIcon != null) pIcon.BringToFront();

            if (isPaused)
            {
                SetPauseUIVisibility(true);
            }

            if (isMatchOver && (ctrlShiftBox != null && ctrlShiftBox.Visible))
            {
                SetShiftClearUIVisibility(true);
            }

            if (pnlResultModal.Visible)
            {
                pnlResultModal.BringToFront();
            }
        }

        private Bitmap? LoadOrderImage()
        {
            try
            {
                using (var stream = typeof(GAME).Assembly.GetManifestResourceStream("tapsilog.png"))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            return new Bitmap(img);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private Image LoadStage1Background()
        {
            try
            {
                using (var stream = typeof(GAME).Assembly.GetManifestResourceStream("stage1_bg.png"))
                {
                    if (stream != null)
                    {
                        using (var img = Image.FromStream(stream))
                        {
                            return new Bitmap(img);
                        }
                    }
                }
            }
            catch { }
            return new Bitmap(Properties.Resources.story_mode_bg);
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
                    using (Image stage1Bg = LoadStage1Background())
                    {
                        g.DrawImage(stage1Bg, 0, 0, ClientSize.Width, ClientSize.Height);
                    }
                }
                cachedBackground?.Dispose();
                cachedBackground = bmp;

                BackgroundImage = cachedBackground;
                BackgroundImageLayout = ImageLayout.None;
            }
            catch { }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGameMatch();
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
            Close();
        }

        private Bitmap TrimTransparentEdges(Bitmap source)
        {
            int minX = source.Width, minY = source.Height, maxX = -1, maxY = -1;

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    if (source.GetPixel(x, y).A > 10)
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            if (maxX < minX || maxY < minY) return source;
            Rectangle cropRect = new Rectangle(minX, minY, (maxX - minX) + 1, (maxY - minY) + 1);
            return source.Clone(cropRect, source.PixelFormat);
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

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            shiftTimer.Stop();
            shiftTimer.Dispose();

            patienceTimer.Stop();
            patienceTimer.Dispose();

            spawnTimer.Stop();
            spawnTimer.Dispose();

            toastTimer.Stop();
            toastTimer.Dispose();

            Walktimer.Stop();
            Walktimer.Dispose();

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

        private void pnlDialogue_Paint(object sender, PaintEventArgs e) { }
        private void ConstructionWorker_Click(object sender, EventArgs e) { }
        private void pboxKwekkwek_Click(object sender, EventArgs e) { }
    }

    // =========================================================
    // 2. HELPER CLASSES
    // =========================================================
    public class CustomerProfile
    {
        public string Name { get; set; } = "";
        public Image? NormalImage { get; set; }
        public Image? AngryImage { get; set; }
    }

    public class CustomerSlot
    {
        public int SlotIndex { get; set; }
        public int TargetX { get; set; }
        public bool IsOccupied { get; set; } = false;
        public bool IsArrived { get; set; } = false;
        public bool IsLeaving { get; set; } = false;
        public bool IsAngry { get; set; } = false;

        public bool SpawnFromLeft { get; set; } = true;
        public bool LeaveToRight { get; set; } = true;

        public float CurrentX { get; set; }
        public float CurrentPatience { get; set; } = 15.0f;
        public int StepCount { get; set; } = 0;

        public CustomerProfile? Profile { get; set; }

        public PictureBox PicCustomer { get; set; } = null!;
        public Control PnlBubble { get; set; } = null!;
        public Panel PnlPatienceBar { get; set; } = null!;
    }
}