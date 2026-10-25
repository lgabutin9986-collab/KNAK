using System;
using System.Drawing;
using System.Windows.Forms;

namespace KNAK
{
    public partial class MAIN : Form
    {
        public MAIN()
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (prevents flickering and tearing)
                return cp;
            }
        }

        private void KNAK_Load(object sender, EventArgs e)
        {
            // 1. Siguraduhing NAKA-DISABLE ang AutoSize para hindi lumiliit ang buttons
            btnplay.AutoSize = false;
            btnexit.AutoSize = false;
            btnEndless.AutoSize = false;

            // 2. I-lock ang eksaktong sukat at pwesto sa simula
            btnplay.Size = new Size(192, 46);
            btnplay.Location = new Point(282, 280);

            btnexit.Size = new Size(192, 44);
            btnexit.Location = new Point(282, 345);

            btnEndless.Size = new Size(192, 46);
            btnEndless.Location = new Point(282, 344);

            // 3. I-attach ang hover scaling effects
            AttachHoverEffect(btnplay, 192, 46, 282, 280);
            AttachHoverEffect(btnexit, 192, 44, 282, 345);
            AttachHoverEffect(btnEndless, 192, 46, 282, 344);
        }

        private void AttachHoverEffect(Button btn, int w, int h, int x, int y)
        {
            btn.MouseEnter += (s, e) =>
            {
                // Palakihin nang kaunti at ipantay sa gitna (-4 sa X, -2 sa Y)
                btn.Size = new Size(w + 8, h + 4);
                btn.Location = new Point(x - 4, y - 2);
            };

            btn.MouseLeave += (s, e) =>
            {
                // Ibalik sa orihinal na sukat at pwesto
                btn.Size = new Size(w, h);
                btn.Location = new Point(x, y);
            };
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

        private void btnEndless_Click(object sender, EventArgs e)
        {
            ENDLESS endlessForm = new ENDLESS(this);
            endlessForm.Show();
            this.Hide();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
    }
}