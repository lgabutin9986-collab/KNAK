using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace KNAK
{
    // =========================================================
    // 1. DAPAT UNANG CLASS ANG FORM CLASS PARA SA DESIGNER
    // =========================================================
    public partial class GAME : Form
    {
        private MAIN? mainForm;
        private Bitmap? cachedBackground;

        // Image & Size storage for face swapping
        private Image? originalWorkerImage;
        private Size originalWorkerSize;

        private const int baseY = 14;   // Standard Y position

        // --- 3 CUSTOMER SLOTS & RANDOM SPAWNING ---
        private CustomerSlot[] slots = new CustomerSlot[3];
        private readonly int[] slotTargetPositions = new int[] { 140, 425, 680 }; // Left, Center, Right slots
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
        private readonly Panel pnlPatienceBar = new Panel();

        private readonly Label lblFloatingToast = new Label();
        private readonly System.Windows.Forms.Timer toastTimer = new System.Windows.Forms.Timer();
        private int toastTicks = 0;

        // Game Over / Win Result Modal
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

            // SPAWN TIMER: 5000 ms (5 segundo bago pumasok ang bagong customer)
            spawnTimer.Interval = 5000;
            spawnTimer.Tick += SpawnTimer_Tick;

            toastTimer.Interval = 40;
            toastTimer.Tick += ToastTimer_Tick;

            Walktimer.Interval = 16; // ~60 FPS
            Walktimer.Tick += Walktimer_Tick;

            try
            {
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(picCustomer, true, null);
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(pnlBubbleChat, true, null);
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(pnlPatienceBar, true, null);
            }
            catch { }
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
            originalWorkerSize = picCustomer.Size;
            picCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
            originalWorkerImage = picCustomer.Image;

            InitializeCustomerPool();
            SetupCustomerSlotsUI();

            PreRenderBackground();

            AttachButtonHover(btnBack, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 220, 53, 69));
            AttachButtonHover(btnRestart, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 0, 123, 255));

            try
            {
                picFood.BackgroundImage = null;
                picFood.Image = TrimTransparentEdges(Properties.Resources.ready_to_serve_kwekkwek);
                picFood.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch { }

            pboxEggsBasket.Click += (s, e) => ShowFloatingToast("🥚 Sariwang itlog ng pugo!", Color.FromArgb(245, 158, 11));
            pboxBatterBowl.Click += (s, e) => ShowFloatingToast("🥣 Kwek-kwek batter ready!", Color.FromArgb(249, 115, 22));
            pboxMeatBowl.Click += (s, e) => ShowFloatingToast("🥩 Malinamnam na beef pares!", Color.FromArgb(239, 68, 68));
            pboxFryer.Click += (s, e) => ShowFloatingToast("🔥 Mainit na mantika!", Color.FromArgb(234, 179, 8));
            pboxPares.Click += (s, e) => ShowFloatingToast("🍲 Kumukulong pares broth!", Color.FromArgb(244, 63, 94));
            pboxTray.Click += (s, e) => ShowFloatingToast("🍱 Serving tray!", Color.FromArgb(148, 163, 184));

            BuildInMatchHud();
            BuildPatienceBar();
            BuildFloatingToast();
            BuildResultModal();

            ResetGameMatch();
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

        // --- SETUP UNIFORM 3-CUSTOMER SLOTS UI ---
        private void SetupCustomerSlotsUI()
        {
            pnlBubbleChat.Size = new Size(150, 85);
            picFood.Location = new Point(25, 20);
            picFood.Size = new Size(42, 42);
            lblQuantity.Location = new Point(72, 28);
            lblQuantity.Size = new Size(45, 25);

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = new CustomerSlot();
                slot.SlotIndex = i;
                slot.TargetX = slotTargetPositions[i];

                if (i == 0)
                {
                    slot.PicCustomer = picCustomer;
                    slot.PnlBubble = pnlBubbleChat;
                    slot.PnlPatienceBar = pnlPatienceBar;
                }
                else
                {
                    slot.PicCustomer = new PictureBox
                    {
                        Size = originalWorkerSize,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand,
                        Visible = false
                    };

                    slot.PnlBubble = new Panel
                    {
                        Size = new Size(150, 85),
                        BackgroundImage = Properties.Resources.speech_bubble_,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackColor = Color.Transparent,
                        Visible = false
                    };

                    PictureBox pboxFood = new PictureBox
                    {
                        Image = picFood.Image,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(42, 42),
                        Location = new Point(25, 20),
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand
                    };

                    Label lblQty = new Label
                    {
                        Text = "× 1",
                        Font = lblQuantity.Font,
                        ForeColor = lblQuantity.ForeColor,
                        Size = new Size(45, 25),
                        Location = new Point(72, 28),
                        BackColor = Color.Transparent,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    slot.PnlBubble.Controls.Add(pboxFood);
                    slot.PnlBubble.Controls.Add(lblQty);

                    slot.PnlPatienceBar = new Panel
                    {
                        Size = new Size(115, 20),
                        BackColor = Color.Transparent,
                        Visible = false
                    };

                    Controls.Add(slot.PicCustomer);
                    Controls.Add(slot.PnlBubble);
                    Controls.Add(slot.PnlPatienceBar);
                }

                slot.PnlBubble.Size = new Size(150, 85);
                slot.PnlPatienceBar.Size = new Size(115, 20);
                slot.PnlPatienceBar.BackColor = Color.Transparent;

                int index = i;
                slot.PicCustomer.Click += (s, e) => TryServeSlot(index);
                slot.PnlBubble.Click += (s, e) => TryServeSlot(index);
                slot.PnlPatienceBar.Paint += (s, e) => DrawSlotPatienceBar(e.Graphics, slots[index]);

                slots[i] = slot;
            }
        }

        // --- 5-SECOND SPAWN TIMER TICK ---
        private void SpawnTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver) return;

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
                    slot.PicCustomer.Image = TrimTransparentEdges(bmp);
                }
            }

            slot.PicCustomer.Size = originalWorkerSize;
            slot.PicCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
            slot.PicCustomer.Visible = true;
            slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            if (!Walktimer.Enabled)
                Walktimer.Start();

            ApplyZOrder();
        }

        // --- WALKING ANIMATION LOGIC ---
        private void Walktimer_Tick(object? sender, EventArgs e)
        {
            bool anyMoving = false;

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = slots[i];
                if (!slot.IsOccupied) continue;

                if (!slot.IsLeaving)
                {
                    // WALKING TO COUNTER
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
                    // LEAVING COUNTER
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

                if (slot.IsArrived && !slot.IsLeaving)
                {
                    UpdateSlotBubblePositions(slot);
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
            slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;
        }

        private void OnSlotArrivedAtCounter(CustomerSlot slot)
        {
            UpdateSlotBubblePositions(slot);
            slot.PnlBubble.Visible = true;
            slot.PnlPatienceBar.Visible = true;

            if (!patienceTimer.Enabled)
                patienceTimer.Start();

            ApplyZOrder();
        }

        private void UpdateSlotBubblePositions(CustomerSlot slot)
        {
            int bubbleX = slot.PicCustomer.Left + 110;
            int bubbleY = slot.PicCustomer.Top + 5;

            slot.PnlBubble.Size = new Size(150, 85);
            slot.PnlBubble.Location = new Point(bubbleX, bubbleY);

            slot.PnlPatienceBar.Size = new Size(115, 20);
            slot.PnlPatienceBar.Location = new Point(bubbleX + 15, bubbleY + 88);
        }

        // --- PATIENCE & ANGRY FACE SYSTEM ---
        private void PatienceTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver) return;

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
                    slot.PicCustomer.Image = TrimTransparentEdges(bmp);
                }
            }
            slot.PicCustomer.SizeMode = PictureBoxSizeMode.StretchImage;
            ApplyZOrder();
        }

        private void TryServeSlot(int slotIndex)
        {
            CustomerSlot slot = slots[slotIndex];
            if (isMatchOver || !slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) return;

            slot.PnlBubble.Visible = false;
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
            if (!Walktimer.Enabled)
                Walktimer.Start();
        }

        private void HandleSlotWalkOut(CustomerSlot slot)
        {
            slot.PnlBubble.Visible = false;
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
            if (!Walktimer.Enabled)
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

                using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
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
            isMatchOver = false;

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

        private void BuildPatienceBar()
        {
            pnlPatienceBar.Size = new Size(115, 20);
            pnlPatienceBar.BackColor = Color.Transparent;
            pnlPatienceBar.Visible = false;
            Controls.Add(pnlPatienceBar);
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
                Color borderColor = currentRevenue >= TargetRevenue
                    ? Color.FromArgb(250, 204, 21)
                    : Color.FromArgb(239, 68, 68);
                using (Pen pen = new Pen(borderColor, 2.5f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void ShiftTimer_Tick(object? sender, EventArgs e)
        {
            if (isMatchOver) return;

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
            lblFloatingToast.Location = new Point(posX, baseY + 40);
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

        private void HandleVictory()
        {
            isMatchOver = true;
            shiftTimer.Stop();
            patienceTimer.Stop();
            spawnTimer.Stop();
            Walktimer.Stop();

            pnlResultModal.Visible = true;

            lblModalTitle.Text = "🎉 SHIFT COMPLETED!";
            lblModalTitle.ForeColor = Color.FromArgb(250, 204, 21);
            lblModalSubtitle.Text = "Daily Revenue Quota Achieved! Outstanding Job!";

            int mins = shiftSecondsLeft / 60;
            int secs = shiftSecondsLeft % 60;
            lblModalStats.Text =
                $"💰 Final Revenue: ₱{currentRevenue} (Target: ₱{TargetRevenue})\n\n" +
                $"👥 Customers Served: {customersServed}\n" +
                $"💔 Walkouts: {walkoutCount} / {MaxWalkouts}\n" +
                $"⏱️ Shift Time Left: {mins:00}:{secs:00}";

            ApplyZOrder();
        }

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
                    slots[i].PnlBubble.BringToFront();
                    slots[i].PnlPatienceBar.BringToFront();
                }
            }

            pboxFryer.BringToFront();
            pboxEggsBasket.BringToFront();
            pboxBatterBowl.BringToFront();
            pboxMeatBowl.BringToFront();
            pboxTray.BringToFront();
            pboxPares.BringToFront();

            lblStage.BringToFront();
            lblTimerHud.BringToFront();
            lblRevenueHud.BringToFront();
            lblWalkoutsHud.BringToFront();
            lblFloatingToast.BringToFront();

            if (btnBack != null) btnBack.BringToFront();
            if (btnRestart != null) btnRestart.BringToFront();

            if (pnlResultModal.Visible)
            {
                pnlResultModal.BringToFront();
            }
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
                    g.DrawImage(Properties.Resources.story_mode_bg, 0, 0, ClientSize.Width, ClientSize.Height);
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
    // 2. MGA HELPER CLASSES (INILIPAT SA BABA NG GAME CLASS)
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

        // Walking Direction Flags
        public bool SpawnFromLeft { get; set; } = true;
        public bool LeaveToRight { get; set; } = true;

        public float CurrentX { get; set; }
        public float CurrentPatience { get; set; } = 15.0f;
        public int StepCount { get; set; } = 0;

        public CustomerProfile? Profile { get; set; }

        public PictureBox PicCustomer { get; set; } = null!;
        public Panel PnlBubble { get; set; } = null!;
        public Panel PnlPatienceBar { get; set; } = null!;
    }
}