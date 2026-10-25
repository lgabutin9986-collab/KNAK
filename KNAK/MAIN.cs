using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace KNAK
{
    public partial class MAIN : Form
    {
        private int currentTutorialIndex = 0;
        private Image[] tutorialPages = new Image[0];

        // Para sa smooth fade animation
        private System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
        private float animationAlpha = 1.0f;
        private int nextIndex = 0;
        private bool isTransitioning = false;

        public MAIN()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            // Setup Smooth Animation Timer
            fadeTimer.Interval = 15;
            fadeTimer.Tick += FadeTimer_Tick;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (iwas flicker)
                return cp;
            }
        }

        private void InitializeTutorialImages()
        {
            tutorialPages = new Image[]
            {
                Properties.Resources.tutorial1,
                Properties.Resources.tutorial2,
                Properties.Resources.tutorial3,
                Properties.Resources.tutorial4,
                Properties.Resources.tutorial5
            };
        }

        private void KNAK_Load(object sender, EventArgs e)
        {
            // Main menu buttons hover effects
            AttachHoverEffect(btnplay, 220, 50, 267, 210);
            AttachHoverEffect(btnEndless, 229, 67, 262, 260);
            AttachHoverEffect(btnExit, 220, 50, 267, 330);
            AttachHoverEffect(btnShop, 50, 50, 352, 395); // Eksaktong nakagitna sa ibaba ng Exit button

            // Tutorial navigation and "i" buttons hover effects
            AttachHoverEffect(pictureBox2, 47, 58, 686, 421);
            AttachHoverEffect(btnBack, 55, 60, 5, 180);
            AttachHoverEffect(btnNext, 55, 60, 575, 180);

            InitializeTutorialImages();

            // Setup Overlay
            panelOverlay.Dock = DockStyle.Fill;
            panelOverlay.BackColor = Color.Transparent;
            panelOverlay.Visible = false;

            // Setup Tutorial Popup Box sa Gitna
            panelTutorial.Size = new Size(635, 430);
            panelTutorial.Location = new Point((this.ClientSize.Width - panelTutorial.Width) / 2, (this.ClientSize.Height - panelTutorial.Height) / 2);
            panelTutorial.BackColor = Color.Transparent;
            panelTutorial.Visible = false;

            panelTutorial.Click += (s, ev) => { };
            pictureBoxTutorial.Click += (s, ev) => { };

            // I-enable ang Paint event para sa custom smooth proportional image rendering
            pictureBoxTutorial.Paint += PictureBoxTutorial_Paint;

            btnShop.Click += btnShop_Click;
        }

        private void AttachHoverEffect(Control ctrl, int w, int h, int x, int y)
        {
            ctrl.MouseEnter += (s, e) =>
            {
                ctrl.Size = new Size(w + 6, h + 3);
                ctrl.Location = new Point(x - 3, y - 1);
            };
            ctrl.MouseLeave += (s, e) =>
            {
                ctrl.Size = new Size(w, h);
                ctrl.Location = new Point(x, y);
            };
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            currentTutorialIndex = 0;
            animationAlpha = 1.0f;
            pictureBoxTutorial.Invalidate();
            UpdateNavButtons();

            panelOverlay.Visible = true;
            panelOverlay.BringToFront();

            panelTutorial.Visible = true;
            panelTutorial.BringToFront();

            pictureBox2.Visible = false; // Itinago ang "i" button habang bukas ang tutorial
            btnShop.Visible = false;     // Itinago rin muna ang Shop button
        }

        private void panelOverlay_Click(object sender, EventArgs e)
        {
            panelTutorial.Visible = false;
            panelOverlay.Visible = false;
            pictureBox2.Visible = true; // Ibinalik ang "i" button kapag nagsara na
            btnShop.Visible = true;     // Ibinalik din ang Shop button
        }

        private void DrawImageProportional(Graphics g, Image img, float alpha)
        {
            if (img == null) return;

            float targetW = pictureBoxTutorial.Width;
            float targetH = pictureBoxTutorial.Height;
            float imgW = img.Width;
            float imgH = img.Height;

            float ratioX = targetW / imgW;
            float ratioY = targetH / imgH;
            float ratio = Math.Min(ratioX, ratioY);

            int newW = (int)(imgW * ratio);
            int newH = (int)(imgH * ratio);
            int posX = (int)((targetW - newW) / 2);
            int posY = (int)((targetH - newH) / 2);

            using (var attr = new System.Drawing.Imaging.ImageAttributes())
            {
                var matrix = new System.Drawing.Imaging.ColorMatrix();
                matrix.Matrix33 = alpha;
                attr.SetColorMatrix(matrix);

                g.DrawImage(
                    img,
                    new Rectangle(posX, posY, newW, newH),
                    0, 0, imgW, imgH,
                    GraphicsUnit.Pixel, attr);
            }
        }

        private void PictureBoxTutorial_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (tutorialPages != null && tutorialPages.Length > 0)
            {
                if (isTransitioning)
                {
                    DrawImageProportional(e.Graphics, tutorialPages[currentTutorialIndex], animationAlpha);
                    DrawImageProportional(e.Graphics, tutorialPages[nextIndex], 1.0f - animationAlpha);
                }
                else
                {
                    DrawImageProportional(e.Graphics, tutorialPages[currentTutorialIndex], 1.0f);
                }
            }
        }

        private void StartTransition(int targetIdx)
        {
            if (isTransitioning || targetIdx == currentTutorialIndex) return;

            nextIndex = targetIdx;
            isTransitioning = true;
            animationAlpha = 1.0f;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            animationAlpha -= 0.2f;

            if (animationAlpha <= 0.0f)
            {
                fadeTimer.Stop();
                currentTutorialIndex = nextIndex;
                isTransitioning = false;
                animationAlpha = 1.0f;
                UpdateNavButtons();
            }

            pictureBoxTutorial.Invalidate();
        }

        private void UpdateNavButtons()
        {
            btnBack.Visible = (currentTutorialIndex > 0);
            btnNext.Visible = (currentTutorialIndex < tutorialPages.Length - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!isTransitioning && currentTutorialIndex < tutorialPages.Length - 1)
            {
                StartTransition(currentTutorialIndex + 1);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!isTransitioning && currentTutorialIndex > 0)
            {
                StartTransition(currentTutorialIndex - 1);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnplay_Click(object sender, EventArgs e)
        {
            GAME gameForm = new GAME(this);
            gameForm.Show();
            this.Hide();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            SHOP shopForm = new SHOP();
            shopForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void btnexit_Click_1(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
    }
}