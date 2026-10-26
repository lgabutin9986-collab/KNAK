using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KNAK
{
    public partial class ENDLESS : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        private const int PBM_SETSTATE = 0x1010;
        private const int PBST_NORMAL = 1; // Green
        private const int PBST_ERROR = 2;  // Red

        int currentScore = 0;
        int highScore = 0; // O kung ano man ang nakalagay sa high score mo
        int strikeCount = 0;

        // Rice Cooker Status Variables (3 servings, 6 seconds refill)
        private int riceServingsLeft = 3;
        private double riceProgressValue = 0;
        private bool isCooking = false;

        // Gulaman Status Variables - 8 seconds refill
        private int gulamanServingsLeft = 2;
        private double refillProgressValue = 0;
        private bool isRefilling = false;

        // Kwek-Kwek Status Variables
        private bool isEggMixed = false;
        private bool isEggMixing = false;
        private double eggMixProgressValue = 0;

        private enum FryerState { Empty, Cooking, Cooked, Overcooked }
        private FryerState currentFryerState = FryerState.Empty;
        private double fryerProgressValue = 0;
        private int fryerPhaseSeconds = 0;

        public class CustomerProfile
        {
            public string Name { get; set; } = "";
            public System.Drawing.Image? NormalImage { get; set; }
            public System.Drawing.Image? AngryImage { get; set; }
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
        // --- CUSTOMER SLOTS & RANDOM SPAWNING ---
        private CustomerSlot[] slots = new CustomerSlot[3];
        private readonly int[] slotTargetPositions = new int[] { 110, 385, 660 }; // Left, Center, Right slots
        private float customerSpeed = 3.2f;

        private readonly System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer patienceTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer walkTimer = new System.Windows.Forms.Timer();
        private List<CustomerProfile> customerPool = new List<CustomerProfile>();
        private Random rand = new Random();

        private const float MaxPatienceSeconds = 15.0f;
        private const int MaxWalkouts = 3; // 3 strikes = Game Over
        private string currentOrder = "tapsilog"; // Current active order style

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
            return null;
        }

        private void SpawnTimer_Tick(object? sender, EventArgs e)
        {
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
            slot.PicCustomer.Location = new Point((int)slot.CurrentX, 14);

            if (slot.Profile.NormalImage != null)
            {
                slot.PicCustomer.Image = new Bitmap(slot.Profile.NormalImage, new Size(260, 255));
            }

            slot.PicCustomer.Visible = true;
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            if (!walkTimer.Enabled)
                walkTimer.Start();
        }

        private void Walktimer_Tick(object? sender, EventArgs e)
        {
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
                            slot.CurrentX += customerSpeed;
                            slot.StepCount++;
                            int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                            slot.CurrentX = Math.Min(slot.CurrentX, slot.TargetX);

                            slot.PicCustomer.Location = new Point((int)slot.CurrentX, 14 + bob);
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
                            slot.CurrentX -= customerSpeed;
                            slot.StepCount++;
                            int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                            slot.CurrentX = Math.Max(slot.CurrentX, slot.TargetX);

                            slot.PicCustomer.Location = new Point((int)slot.CurrentX, 14 + bob);
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
                        slot.CurrentX += customerSpeed;
                        slot.StepCount++;
                        int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                        slot.PicCustomer.Location = new Point((int)slot.CurrentX, 14 + bob);
                        anyMoving = true;

                        if (slot.CurrentX >= 1100) ResetSlotState(slot);
                    }
                    else
                    {
                        slot.CurrentX -= customerSpeed;
                        slot.StepCount++;
                        int bob = (int)(Math.Sin(slot.StepCount * 0.28) * 2);
                        slot.PicCustomer.Location = new Point((int)slot.CurrentX, 14 + bob);
                        anyMoving = true;

                        if (slot.CurrentX <= -220) ResetSlotState(slot);
                    }
                }
            }

            if (!anyMoving) walkTimer.Stop();
        }

        private void OnSlotArrivedAtCounter(CustomerSlot slot)
        {
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = true;
            slot.PnlPatienceBar.Visible = true;

            if (!patienceTimer.Enabled)
                patienceTimer.Start();
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

        private void PatienceTimer_Tick(object? sender, EventArgs e)
        {
            bool anyArrived = false;

            for (int i = 0; i < 3; i++)
            {
                CustomerSlot slot = slots[i];
                if (!slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) continue;

                anyArrived = true;
                slot.CurrentPatience = Math.Max(0, slot.CurrentPatience - 0.1f);
                slot.PnlPatienceBar.Invalidate();

                float ratio = slot.CurrentPatience / MaxPatienceSeconds;

                // Magiging angry face kapag umabot na sa ibaba ng 25% ang patience
                if (ratio < 0.25f && !slot.IsAngry)
                {
                    slot.IsAngry = true;
                    if (slot.Profile?.AngryImage != null)
                    {
                        slot.PicCustomer.Image = new Bitmap(slot.Profile.AngryImage, new Size(260, 255));
                    }
                }

                // Kapag umabot sa 0 ang patience = Walkout / Strike!
                if (slot.CurrentPatience <= 0)
                {
                    HandleSlotWalkOut(slot);
                }
            }

            if (!anyArrived) patienceTimer.Stop();
        }

        private void HandleSlotWalkOut(CustomerSlot slot)
        {
            if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
            slot.PnlPatienceBar.Visible = false;

            // Tawagin ang iyong 3-strike system dito
            strikeCount++;
            if (strikeCount == 1) pbCircle1.Image = Properties.Resources.red_x;
            else if (strikeCount == 2) pbCircle2.Image = Properties.Resources.red_x;
            else if (strikeCount >= 3)
            {
                pbCircle3.Image = Properties.Resources.red_x;
                MessageBox.Show("GAME OVER! Umabot sa 3 ang strikes mo.", "Tapos na ang Laro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Ilagay ang game over exit o restart dito
            }

            slot.IsLeaving = true;
            if (!walkTimer.Enabled) walkTimer.Start();
        }

        // --- VERIFICATION OF ORDER & POINTS ---
        private void TryServeSlot(int slotIndex, string plateTag)
        {
            CustomerSlot slot = slots[slotIndex];
            if (!slot.IsOccupied || !slot.IsArrived || slot.IsLeaving) return;

            if (plateTag == currentOrder) // Tugma sa hiniling ng customer
            {
                if (slot.PnlBubble != null) slot.PnlBubble.Visible = false;
                slot.PnlPatienceBar.Visible = false;

                int earnedScore = (currentOrder == "tapsilog") ? 150 : (currentOrder == "pares" ? 120 : 100);
                AddScore(earnedScore); // Tatawag sa AddScore method mo para mag-update at mag-check ng highscore

                slot.IsLeaving = true;
                if (!walkTimer.Enabled) walkTimer.Start();
            }
            else
            {
                MessageBox.Show("Maling order ang ibinigay mo sa customer na ito!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            

        // --- EGG SYSTEM VARIABLES (4s luto = green, 3s bago masunog = red) ---
        private int eggSecondsCount = 0;
        private string eggState = "EMPTY";
        private double eggProgressValue = 0;

        // --- TAPA SYSTEM VARIABLES (5s luto = green, 3s bago masunog = red) ---
        private string tapaState = "EMPTY";
        private int tapaSecondsCount = 0;
        private double tapaProgressValue = 0;

        // --- PARES SYSTEM VARIABLES (8s refill) ---
        private int paresServingsLeft = 3;
        private bool isCookingPares = false;
        private double paresProgressValue = 0;

        // Progress Bars para sa bawat istasyon
        private ProgressBar pbRefill = new ProgressBar();
        private ProgressBar pbMix = new ProgressBar();
        private ProgressBar pbFryer = new ProgressBar();
        private ProgressBar pbParesProgress = new ProgressBar();
        private ProgressBar pbEggProgress = new ProgressBar();
        private ProgressBar pbTapaProgress = new ProgressBar();

        // Timers
        private System.Windows.Forms.Timer timerRefill = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerEggMix = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerFryer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerEgg = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerTapa = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerPares = new System.Windows.Forms.Timer();

        public ENDLESS()
        {
            InitializeComponent();

            rawegg.Click += rawegg_Click;
            coatedegg.Click += coatedegg_Click;
            pictureBox6.Click += pictureBox6_Click;
            bowlegg.Click += bowlegg_Click;
            uncookedegg.Click += uncookedegg_Click;
            bowltapa.Click += bowltapa_Click;
            rawtapa.Click += rawtapa_Click;
            parespot.Click += parespot_Click;

            // I-setup ang mga Progress Bars
            SetupCustomProgressBar(pbRefill, pictureBox5);
            SetupCustomProgressBar(pbMix, coatedegg);
            SetupCustomProgressBar(pbFryer, pictureBox6);
            SetupCustomProgressBar(pbParesProgress, parespot);
            SetupCustomProgressBar(pbEggProgress, uncookedegg);
            SetupCustomProgressBar(pbTapaProgress, rawtapa);

            // Timer Intervals
            timerRefill.Interval = 80;
            timerRefill.Tick += timerRefill_Tick;

            timerEggMix.Interval = 60;
            timerEggMix.Tick += timerEggMix_Tick;

            timerFryer.Interval = 1000;
            timerFryer.Tick += timerFryer_Tick;

            timerRice.Interval = 60;
            timerRice.Tick += timerRice_Tick;

            timerEgg.Interval = 1000;
            timerEgg.Tick += timerEgg_Tick;

            timerTapa.Interval = 1000;
            timerTapa.Tick += timerTapa_Tick;

            timerPares.Interval = 80;
            timerPares.Tick += timerPares_Tick;
        }

        private void SetupCustomProgressBar(ProgressBar pb, Control targetControl)
        {
            if (targetControl != null)
            {
                pb.Size = new Size(targetControl.Width, 16);
                pb.Location = new Point(targetControl.Location.X, targetControl.Location.Y + targetControl.Height + 4);
                pb.Visible = false;
                Controls.Add(pb);
                pb.BringToFront();
            }
        }

        private void ENDLESS_Load(object sender, EventArgs e)
        {
            UpdateScoreDisplay();

            if (currentScore > GameData.EndlessHighScore)
            {
                GameData.EndlessHighScore = currentScore;
            }

            lblHighscore.Text = "₱" + GameData.EndlessHighScore.ToString("N0");

            // --- SIMULA NG LARO (DEFAULT STATES) ---
            pbRiceCooker.Image = Properties.Resources.rice_cooker_full;
            lblRiceStatus.Text = "3/3 cups";
            pbRiceProgress.Visible = false;

            pictureBox5.Image = Properties.Resources.fullgulaman;
            coatedegg.Image = Properties.Resources.noeggbowl;
            parespot.Image = Properties.Resources.parespot;

            uncookedegg.Image = null;
            uncookedegg.Visible = false;
            uncookedegg.Enabled = false;

            rawtapa.Image = null;
            rawtapa.Visible = false;
            rawtapa.Enabled = false;

            eggState = "EMPTY";
            tapaState = "EMPTY";

            pbTray.Visible = false;
            pbTray2.Visible = false;
            pbTray3.Visible = false;

            pbPlate1.Visible = false;
            pbPlate2.Visible = false;
            pbPlate3.Visible = false;

            basogulaman1.Visible = false;
            basogulaman2.Visible = false;
            basogulaman3.Visible = false;

            kwek1.Visible = false;
            kwek2.Visible = false;

            pares1.Visible = false;
            pares2.Visible = false;
            pares3.Visible = false;

            pbRefill.Visible = false;
            pbMix.Visible = false;
            pbFryer.Visible = false;
            pbParesProgress.Visible = false;
            pbEggProgress.Visible = false;
            pbTapaProgress.Visible = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            coatedegg.Image = Properties.Resources.noeggbowl;
            kwek1.Visible = false;
            kwek2.Visible = false;

            uncookedegg.Image = null;
            uncookedegg.Visible = false;
            rawtapa.Image = null;
            rawtapa.Visible = false;
            pbRiceProgress.Visible = false;
        }

        // --- RICE COOKER LOGIC ---
        private void pbRiceCooker_Click(object sender, EventArgs e)
        {
            if (riceServingsLeft > 0 && !isCooking)
            {
                if (!pbPlate1.Visible) { riceServingsLeft--; pbTray.Visible = true; pbPlate1.Image = Properties.Resources.plate_with_rice; pbPlate1.Tag = "with_rice"; pbPlate1.Visible = true; }
                else if (!pbPlate2.Visible) { riceServingsLeft--; pbTray2.Visible = true; pbPlate2.Image = Properties.Resources.plate_with_rice; pbPlate2.Tag = "with_rice"; pbPlate2.Visible = true; }
                else if (!pbPlate3.Visible) { riceServingsLeft--; pbTray3.Visible = true; pbPlate3.Image = Properties.Resources.plate_with_rice; pbPlate3.Tag = "with_rice"; pbPlate3.Visible = true; }
                else { return; }

                lblRiceStatus.Text = riceServingsLeft + "/3 cups";
                if (riceServingsLeft <= 0) pbRiceCooker.Image = Properties.Resources.rice_cooker_empty;
            }
            else if (riceServingsLeft <= 0 && !isCooking)
            {
                riceProgressValue = 0;
                pbRiceProgress.Value = 0;
                SetProgressBarColor(pbRiceProgress, PBST_NORMAL);
                pbRiceProgress.Visible = true;
                isCooking = true;
                timerRice.Start();
            }
        }

        private void timerRice_Tick(object sender, EventArgs e)
        {
            riceProgressValue += 1;

            if (riceProgressValue <= 100) pbRiceProgress.Value = (int)riceProgressValue;

            if (riceProgressValue >= 100)
            {
                timerRice.Stop();
                isCooking = false;
                riceServingsLeft = 3;
                pbRiceCooker.Image = Properties.Resources.rice_cooker_full;
                pbRiceProgress.Visible = false;
                lblRiceStatus.Text = "3/3 cups";
            }
        }

        // --- GULAMAN REFILL LOGIC ---
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (gulamanServingsLeft > 0 && !isRefilling)
            {
                if (!basogulaman1.Visible) { gulamanServingsLeft--; basogulaman1.BackColor = Color.Transparent; basogulaman1.Image = Properties.Resources.gulaman_removebg_preview; basogulaman1.Visible = true; basogulaman1.BringToFront(); lblGulamanStatus.Text = gulamanServingsLeft + "/2 cups"; }
                else if (!basogulaman2.Visible) { gulamanServingsLeft--; basogulaman2.BackColor = Color.Transparent; basogulaman2.Image = Properties.Resources.gulaman_removebg_preview; basogulaman2.Visible = true; basogulaman2.BringToFront(); lblGulamanStatus.Text = gulamanServingsLeft + "/2 cups"; }
                else if (!basogulaman3.Visible) { gulamanServingsLeft--; basogulaman3.BackColor = Color.Transparent; basogulaman3.Image = Properties.Resources.gulaman_removebg_preview; basogulaman3.Visible = true; basogulaman3.BringToFront(); lblGulamanStatus.Text = gulamanServingsLeft + "/2 cups"; }
                else { MessageBox.Show("Puno na ang lahat ng baso sa table!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (gulamanServingsLeft <= 0)
                {
                    pictureBox5.Image = Properties.Resources.emptygulaman;
                    lblGulamanStatus.Text = "Click to refill!";
                }
            }
            else if (gulamanServingsLeft <= 0 && !isRefilling)
            {
                isRefilling = true;
                lblGulamanStatus.Text = "Refilling...";
                refillProgressValue = 0;
                pbRefill.Value = 0;
                SetProgressBarColor(pbRefill, PBST_NORMAL);
                pbRefill.Visible = true;
                timerRefill.Start();
            }
        }

        private void timerRefill_Tick(object sender, EventArgs e)
        {
            refillProgressValue += 1.25;

            if (refillProgressValue <= 100)
            {
                pbRefill.Value = (int)refillProgressValue;
            }

            if (refillProgressValue >= 100)
            {
                timerRefill.Stop();
                isRefilling = false;
                gulamanServingsLeft = 2;
                pictureBox5.Image = Properties.Resources.fullgulaman;
                pbRefill.Visible = false;
                lblGulamanStatus.Text = gulamanServingsLeft + "/2 cups";
            }
        }

        // --- KWEK-KWEK STIRRING LOGIC ---
        private void rawegg_Click(object sender, EventArgs e)
        {
            if (!isEggMixed && !isEggMixing)
            {
                isEggMixing = true;
                eggMixProgressValue = 0;
                coatedegg.Image = Properties.Resources.stiregg;

                pbMix.Value = 0;
                SetProgressBarColor(pbMix, PBST_NORMAL);
                pbMix.Visible = true;
                timerEggMix.Start();
            }
        }

        private void timerEggMix_Tick(object sender, EventArgs e)
        {
            eggMixProgressValue += 3.33;

            if (eggMixProgressValue <= 100)
            {
                pbMix.Value = (int)eggMixProgressValue;
            }

            if (eggMixProgressValue >= 100)
            {
                timerEggMix.Stop();
                isEggMixing = false;
                isEggMixed = true;
                pbMix.Visible = false;
            }
        }

        private void coatedegg_Click(object sender, EventArgs e)
        {
            if (isEggMixed && currentFryerState == FryerState.Empty)
            {
                isEggMixed = false;
                coatedegg.Image = Properties.Resources.noeggbowl;

                currentFryerState = FryerState.Cooking;
                fryerProgressValue = 0;
                fryerPhaseSeconds = 0;
                pictureBox6.Image = Properties.Resources.cooking;

                pbFryer.Value = 0;
                SetProgressBarColor(pbFryer, PBST_NORMAL);
                pbFryer.Visible = true;
                timerFryer.Start();
            }
        }

        // --- KWEK-KWEK FRYER LOGIC ---
        private void timerFryer_Tick(object sender, EventArgs e)
        {
            fryerPhaseSeconds++;

            if (currentFryerState == FryerState.Cooking)

            {
                fryerProgressValue += 20;
                if (fryerProgressValue > 100) fryerProgressValue = 100;

                pbFryer.Value = (int)fryerProgressValue;

                if (fryerPhaseSeconds >= 5)
                {
                    currentFryerState = FryerState.Cooked;
                    pictureBox6.Image = Properties.Resources.cookedkwek2;
                    lblKwekStatus.Text = "Cooking..";
                    fryerPhaseSeconds = 0;
                    fryerProgressValue = 0;
                    SetProgressBarColor(pbFryer, PBST_NORMAL);
                }
            }
            else if (currentFryerState == FryerState.Cooked)
            {
                fryerProgressValue += 20;
                if (fryerProgressValue > 100) fryerProgressValue = 100;
                lblKwekStatus.Text = "Cooked!";

                pbFryer.Value = (int)fryerProgressValue;

                if (fryerPhaseSeconds >= 3)
                {
                    SetProgressBarColor(pbFryer, PBST_ERROR);
                
                }

                if (fryerPhaseSeconds >= 5)
                {
                    currentFryerState = FryerState.Overcooked;
                    lblKwekStatus.Text = "Burned!";
                    pictureBox6.Image = Properties.Resources.overcookedkwek2;
                    timerFryer.Stop();
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (currentFryerState == FryerState.Cooked)
            {
                timerFryer.Stop();

                if (!kwek1.Visible)
                {
                    kwek1.BackColor = Color.Transparent;
                    kwek1.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
                    kwek1.Visible = true;
                    kwek1.BringToFront();
                }
                else if (!kwek2.Visible)
                {
                    kwek2.BackColor = Color.Transparent;
                    kwek2.Image = Properties.Resources.ready_to_serve_kwekkwek_removebg_preview;
                    kwek2.Visible = true;
                    kwek2.BringToFront();
                }
                else
                {
                    MessageBox.Show("Puno na ang pwesto ng kwek-kwek sa table!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentFryerState = FryerState.Empty;
                pictureBox6.Image = Properties.Resources.fryer;
                pbFryer.Visible = false;
            }
            else if (currentFryerState == FryerState.Overcooked)
            {
                timerFryer.Stop();
                currentFryerState = FryerState.Empty;
                pictureBox6.Image = Properties.Resources.fryer;
                pbFryer.Visible = false;
                MessageBox.Show("Nasunog ang kwek-kwek!", "Tapon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- EGG SYSTEM LOGIC (4s Green / 3s Red Overcooked) ---
        private void bowlegg_Click(object sender, EventArgs e)
        {
            if (eggState == "EMPTY")
            {
                eggState = "COOKING";
                eggSecondsCount = 0;
                eggProgressValue = 0;

                uncookedegg.Visible = true;
                uncookedegg.Enabled = true;
                uncookedegg.Image = Properties.Resources.uncookedegg;

                pbEggProgress.Value = 0;
                SetProgressBarColor(pbEggProgress, PBST_NORMAL); // Kulay Green sa simula (Paluto)
                pbEggProgress.Visible = true;

                timerEgg.Stop();
                timerEgg.Start();
            }
        }

        private void timerEgg_Tick(object sender, EventArgs e)
        {
            eggSecondsCount++;

            if (eggSecondsCount <= 4)
            {
                // Yugto 1: Paluto pa lang (Green) - 4 seconds (25% bawat segundo)
                eggState = "COOKING";
                lblEggStatus.Text = "Cooking...";

                uncookedegg.Image = Properties.Resources.uncookedegg;
                eggProgressValue = eggSecondsCount * 25;
                if (eggProgressValue > 100) eggProgressValue = 100;
                pbEggProgress.Value = (int)eggProgressValue;
                SetProgressBarColor(pbEggProgress, PBST_NORMAL);
            }
            else if (eggSecondsCount > 4 && eggSecondsCount <= 7)
            {
                // Yugto 2: Luto na pero nag-aabang ma-overcook (Red) - 3 seconds
                eggState = "COOKED";
                lblEggStatus.Text = "Cooked";
                uncookedegg.Image = Properties.Resources.cookedegg;

                // Muling ibaba sa 0 para mag-reboot o umakyat ang red progress bar habang naghihintay masunog
                double redProgress = (eggSecondsCount - 4) * 33.3;
                lblEggStatus.Text = "Cooked!";
                if (redProgress > 100) redProgress = 100;
                pbEggProgress.Value = (int)redProgress;
                SetProgressBarColor(pbEggProgress, PBST_ERROR); // Kulay Red (Pa-overcook na)
            }
            else if (eggSecondsCount > 7)
            {
                // Sunog na
                eggState = "BURNED";
                lblEggStatus.Text = "Burned!";
                timerEgg.Stop();
                uncookedegg.Image = Properties.Resources.burnedegg;
                SetProgressBarColor(pbEggProgress, PBST_ERROR);
            }
        }

        private void uncookedegg_Click(object sender, EventArgs e)
        {
            GuidanceEggOrTapaClick("egg");
        }

        // --- TAPA SYSTEM LOGIC (5s Green / 3s Red Overcooked) ---
        private void bowltapa_Click(object sender, EventArgs e)
        {
            if (tapaState == "EMPTY")
            {
                tapaState = "COOKING";
                tapaSecondsCount = 0;
                tapaProgressValue = 0;

                rawtapa.Visible = true;
                rawtapa.Enabled = true;
                rawtapa.Image = Properties.Resources.rawtapa;

                pbTapaProgress.Value = 0;
                SetProgressBarColor(pbTapaProgress, PBST_NORMAL); // Kulay Green sa simula (Paluto)
                pbTapaProgress.Visible = true;

                timerTapa.Stop();
                timerTapa.Start();
            }
        }

        private void timerTapa_Tick(object sender, EventArgs e)
        {
            tapaSecondsCount++;

            if (tapaSecondsCount <= 5)
            {
                // Yugto 1: Paluto pa lang (Green) - 5 seconds (20% bawat segundo)
                tapaState = "COOKING";
                lblTapaStatus.Text = "Cooking...";
                rawtapa.Image = Properties.Resources.rawtapa;
                tapaProgressValue = tapaSecondsCount * 20;
                if (tapaProgressValue > 100) tapaProgressValue = 100;
                pbTapaProgress.Value = (int)tapaProgressValue;
                SetProgressBarColor(pbTapaProgress, PBST_NORMAL);
            }
            else if (tapaSecondsCount > 5 && tapaSecondsCount <= 8)
            {
                // Yugto 2: Luto na pero nag-aabang ma-overcook (Red) - 3 seconds
                tapaState = "COOKED";
                lblTapaStatus.Text = "Cooked!";
                rawtapa.Image = Properties.Resources.cookedtapa;

                double redProgress = (tapaSecondsCount - 5) * 33.3;
                if (redProgress > 100) redProgress = 100;
                pbTapaProgress.Value = (int)redProgress;
                lblTapaStatus.Text = "Cooked!";
                SetProgressBarColor(pbTapaProgress, PBST_ERROR); // Kulay Red (Pa-overcook na)
            }
            else if (tapaSecondsCount > 8)
            {
                // Sunog na
                tapaState = "BURNED";
                lblTapaStatus.Text = "Burned!";
                timerTapa.Stop();
                rawtapa.Image = Properties.Resources.burnedtapa;
                SetProgressBarColor(pbTapaProgress, PBST_ERROR);
            }
        }

        private void rawtapa_Click(object sender, EventArgs e)
        {
            GuidanceEggOrTapaClick("tapa");
        }

        // STRICT SEQUENTIAL CHECK PARA SA PAGLAGAY SA PLATO (Plate 1 -> Plate 2 -> Plate 3)
        private void GuidanceEggOrTapaClick(string itemType)
        {
            if (itemType == "egg")
            {
                if (eggState == "COOKED")
                {
                    timerEgg.Stop();

                    if (pbPlate1.Visible && pbPlate1.Tag?.ToString() == "with_rice")
                    {
                        pbPlate1.Image = Properties.Resources.eggrice;
                        pbPlate1.Tag = "with_egg";
                    }
                    else if (pbPlate1.Visible && pbPlate1.Tag?.ToString() == "with_tapa")
                    {
                        pbPlate1.Image = Properties.Resources.tapsilog;
                        pbPlate1.Tag = "with_tapsilog";
                    }
                    else if (pbPlate2.Visible && pbPlate2.Tag?.ToString() == "with_rice")
                    {
                        pbPlate2.Image = Properties.Resources.eggrice;
                        pbPlate2.Tag = "with_egg";
                    }
                    else if (pbPlate2.Visible && pbPlate2.Tag?.ToString() == "with_tapa")
                    {
                        pbPlate2.Image = Properties.Resources.tapsilog;
                        pbPlate2.Tag = "with_tapsilog";
                    }
                    else if (pbPlate3.Visible && pbPlate3.Tag?.ToString() == "with_rice")
                    {
                        pbPlate3.Image = Properties.Resources.eggrice;
                        pbPlate3.Tag = "with_egg";
                    }
                    else if (pbPlate3.Visible && pbPlate3.Tag?.ToString() == "with_tapa")
                    {
                        pbPlate3.Image = Properties.Resources.tapsilog;
                        pbPlate3.Tag = "with_tapsilog";
                    }

                    eggState = "EMPTY";
                    uncookedegg.Image = null;
                    uncookedegg.Visible = false;
                    uncookedegg.Enabled = false;
                    pbEggProgress.Visible = false;
                    eggSecondsCount = 0;
                }
                else if (eggState == "BURNED")
                {
                    timerEgg.Stop();
                    eggState = "EMPTY";
                    uncookedegg.Image = null;
                    uncookedegg.Visible = false;
                    uncookedegg.Enabled = false;
                    pbEggProgress.Visible = false;
                    eggSecondsCount = 0;
                }
            }
            else if (itemType == "tapa")
            {
                if (tapaState == "COOKED")
                {
                    timerTapa.Stop();

                    if (pbPlate1.Visible && pbPlate1.Tag?.ToString() == "with_rice")
                    {
                        pbPlate1.Image = Properties.Resources.tapsi;
                        pbPlate1.Tag = "with_tapa";
                    }
                    else if (pbPlate1.Visible && pbPlate1.Tag?.ToString() == "with_egg")
                    {
                        pbPlate1.Image = Properties.Resources.tapsilog;
                        pbPlate1.Tag = "with_tapsilog";
                    }
                    else if (pbPlate2.Visible && pbPlate2.Tag?.ToString() == "with_rice")
                    {
                        pbPlate2.Image = Properties.Resources.tapsi;
                        pbPlate2.Tag = "with_tapa";
                    }
                    else if (pbPlate2.Visible && pbPlate2.Tag?.ToString() == "with_egg")
                    {
                        pbPlate2.Image = Properties.Resources.tapsilog;
                        pbPlate2.Tag = "with_tapsilog";
                    }
                    else if (pbPlate3.Visible && pbPlate3.Tag?.ToString() == "with_rice")
                    {
                        pbPlate3.Image = Properties.Resources.tapsi;
                        pbPlate3.Tag = "with_tapa";
                    }
                    else if (pbPlate3.Visible && pbPlate3.Tag?.ToString() == "with_egg")
                    {
                        pbPlate3.Image = Properties.Resources.tapsilog;
                        pbPlate3.Tag = "with_tapsilog";
                    }

                    tapaState = "EMPTY";
                    rawtapa.Image = null;
                    rawtapa.Visible = false;
                    rawtapa.Enabled = false;
                    pbTapaProgress.Visible = false;
                    tapaSecondsCount = 0;
                }
                else if (tapaState == "BURNED")
                {
                    timerTapa.Stop();
                    tapaState = "EMPTY";
                    rawtapa.Image = null;
                    rawtapa.Visible = false;
                    rawtapa.Enabled = false;
                    pbTapaProgress.Visible = false;
                    tapaSecondsCount = 0;
                }
            }
        }

        // --- PARES SYSTEM LOGIC ---
        private void parespot_Click(object sender, EventArgs e)
        {
            if (paresServingsLeft > 0 && !isCookingPares)
            {
                if (!pares1.Visible)
                {
                    paresServingsLeft--;
                    pares1.Visible = true;
                    pares1.Image = Properties.Resources.pares;
                    pares1.BringToFront();
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else if (!pares2.Visible)
                {
                    paresServingsLeft--;
                    pares2.Visible = true;
                    pares2.Image = Properties.Resources.pares;
                    pares2.BringToFront();
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else if (!pares3.Visible)
                {
                    paresServingsLeft--;
                    pares3.Visible = true;
                    pares3.Image = Properties.Resources.pares;
                    pares3.BringToFront();
                    lblParesStatus.Text = paresServingsLeft + "/3 bowls";
                }
                else
                {
                    MessageBox.Show("Puno na ang mga pares bowls sa table!", "Paalala", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (paresServingsLeft <= 0)
                {
                    parespot.Image = Properties.Resources.emptypot;
                    lblParesStatus.Text = "Click to refill!";
                }
            }
            else if (paresServingsLeft <= 0 && !isCookingPares)
            {
                isCookingPares = true;
                lblParesStatus.Text = "Refilling...";
                paresProgressValue = 0;
                pbParesProgress.Value = 0;
                SetProgressBarColor(pbParesProgress, PBST_NORMAL);
                pbParesProgress.Visible = true;
                timerPares.Start();
            }
        }

        private void timerPares_Tick(object sender, EventArgs e)
        {
            paresProgressValue += 1.25;

            if (paresProgressValue <= 100)
            {
                pbParesProgress.Value = (int)paresProgressValue;
            }

            if (paresProgressValue >= 100)
            {
                timerPares.Stop();
                isCookingPares = false;
                paresServingsLeft = 3;
                lblParesStatus.Text = paresServingsLeft + "/3 bowls";

                parespot.Image = Properties.Resources.parespot;
                pbParesProgress.Visible = false;
            }
        }

        private void SetProgressBarColor(ProgressBar pb, int state)
        {
            if (pb != null)
            {
                SendMessage(pb.Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
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

        // Halimbawa ito ng method o event kapag nag-walkout ang isang customer
        private void CustomerWalkout()
        {
            strikeCount++;

            // Baguhin ang circle creatives depende sa bilang ng strike
            if (strikeCount == 1)
            {
                pbCircle1.Image = Properties.Resources.red_x; // O kung ano mang creative asset ng strike mo
            }
            else if (strikeCount == 2)
            {
                pbCircle2.Image = Properties.Resources.red_x;
            }
            else if (strikeCount >= 3)
            {
                pbCircle3.Image = Properties.Resources.red_x;

                // --- GAME OVER LOGIC ---
                timerEgg.Stop();
                timerTapa.Stop();
                timerRice.Stop();
                timerPares.Stop();

                MessageBox.Show("GAME OVER! Umabot sa 3 ang walkout/strikes mo.\nKabuuang Nakuha Mo: ₱" + currentScore.ToString("N0"), "Laro Tapos Na", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Pwede mo rito ilagay ang pagbalik sa Main Menu o pag-reset
            }
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void pictureBox13_Click(object sender, EventArgs e) { }
    }
}