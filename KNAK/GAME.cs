using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;

namespace KNAK
{
    public partial class GAME : Form
    {
        private MAIN? mainForm;
        private Bitmap? cachedBackground;

        // Walking physics & animation
        private float currentWorkerX;
        private int targetPosition = 427; // Center position behind the counter for 1024px width
        private const int baseY = 14;
        private float speed = 2.4f; // Smooth walking speed per 16ms tick
        private int stepCount = 0;

        // Dialogue dialogue cycle
        private int talkCycleIndex = 0;
        private readonly string[] workerQuotes = new[]
        {
            "Mainit ang panahon sa construction site kanina, buti bukas ang kantina ninyo!",
            "Ang sarap ng meryenda dito, laging paborito ng mga kasamahan ko sa trabaho!",
            "Sipag at tiyaga lang araw-araw para sa pamilya. Salamat sa mainit na pag-asikaso!",
            "Babalik kami mamayang hapon pagkatapos ng buhos ng semento!"
        };

        public GAME()
        {
            InitializeComponent();

            // Enable maximum performance double buffering to eliminate any background tearing or flickering
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
            this.UpdateStyles();

            // Enable double buffering directly on the PictureBox to stop flickering
            try
            {
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.SetValue(ConstructionWorker, true, null);
            }
            catch { }
        }

        public GAME(MAIN main) : this()
        {
            this.mainForm = main;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // WS_EX_COMPOSITED (0x02000000): Native Windows compositor double-buffers all child controls
                // and renders everything seamlessly from bottom to top, preventing any background jitter.
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void GAME_Load(object sender, EventArgs e)
        {
            // 1. Pre-render the 1920x1080 background to the exact 1024x576 form size once.
            // With ImageLayout.None, GDI+ never stretches on tick repaints, keeping the background 100% rock solid.
            PreRenderBackground();

            // 2. Setup interactive button hover animations
            AttachButtonHover(btnBack, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 220, 53, 69));
            AttachButtonHover(btnRestart, Color.FromArgb(200, 30, 35, 48), Color.FromArgb(240, 0, 123, 255));
            AttachButtonHover(btnServe, Color.FromArgb(40, 167, 69), Color.FromArgb(46, 204, 113));
            AttachButtonHover(btnTalk, Color.FromArgb(23, 162, 184), Color.FromArgb(52, 152, 219));

            // 3. Start the worker walking animation
            StartWalkingSequence();
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

                this.BackgroundImage = cachedBackground;
                this.BackgroundImageLayout = ImageLayout.None; // 1:1 direct blit: zero background jitter or stretch during paint
            }
            catch { }
        }

        private void StartWalkingSequence()
        {
            pnlDialogue.Visible = false;
            lblStatus.Text = "";
            btnServe.Enabled = true;
            btnServe.BackColor = Color.FromArgb(40, 167, 69);
            lblDialogue.Text = "Pabili po! Isang malamig na tubig at tinapay para sa meryenda.";

            // Start worker off-screen from the right entrance
            currentWorkerX = ClientSize.Width + 20;
            stepCount = 0;
            ConstructionWorker.Location = new Point((int)currentWorkerX, baseY);

            Walktimer.Interval = 16; // 60 FPS silky-smooth movement
            Walktimer.Start();
        }

        private void Walktimer_Tick(object sender, EventArgs e)
        {
            if (currentWorkerX > targetPosition)
            {
                currentWorkerX -= speed;
                stepCount++;

                // Subtle natural walking step bobbing (1-2px vertical wave)
                int bob = (int)(Math.Sin(stepCount * 0.28) * 2);

                if (currentWorkerX <= targetPosition)
                {
                    currentWorkerX = targetPosition;
                    ConstructionWorker.Location = new Point((int)currentWorkerX, baseY);
                    Walktimer.Stop();
                    OnWorkerArrivedAtCounter();
                }
                else
                {
                    ConstructionWorker.Location = new Point((int)currentWorkerX, baseY + bob);
                }
            }
            else
            {
                Walktimer.Stop();
                ConstructionWorker.Location = new Point(targetPosition, baseY);
                OnWorkerArrivedAtCounter();
            }
        }

        private void OnWorkerArrivedAtCounter()
        {
            // Show the dialogue card once worker is in place
            pnlDialogue.Visible = true;
            lblStatus.Text = "Ready for order";
        }

        private void btnServe_Click(object sender, EventArgs e)
        {
            lblDialogue.Text = "Salamat po nang marami! Ang sarap ng meryenda, pampalakas bago bumalik sa construction!";
            lblStatus.Text = "⭐ Order Served! (+50 pts)";
            btnServe.Enabled = false;
            btnServe.BackColor = Color.FromArgb(108, 117, 125);
        }

        private void btnTalk_Click(object sender, EventArgs e)
        {
            lblDialogue.Text = workerQuotes[talkCycleIndex % workerQuotes.Length];
            talkCycleIndex++;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartWalkingSequence();
        }

        private void ConstructionWorker_Click(object sender, EventArgs e)
        {
            if (!Walktimer.Enabled)
            {
                btnTalk_Click(sender, e);
            }
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
}