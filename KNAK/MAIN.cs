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
            // Attach subtle hover scaling effects to menu buttons
            AttachHoverEffect(btnplay, 192, 46, 282, 280);
            AttachHoverEffect(btnexit, 192, 44, 282, 345);
        }

        private void AttachHoverEffect(Button btn, int w, int h, int x, int y)
        {
            btn.MouseEnter += (s, e) =>
            {
                btn.Size = new Size(w + 8, h + 4);
                btn.Location = new Point(x - 4, y - 2);
            };
            btn.MouseLeave += (s, e) =>
            {
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
