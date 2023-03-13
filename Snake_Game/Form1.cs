namespace Snake_Game
{
    using System.Diagnostics;


    public partial class Form1 : Form
    {
        private bool Move = false;

        private float X = 1;
        private float Y1 = 0;
        private float Y2 = 20;

        private int MoveX = 1;
        private int MoveY = 20;
        private int Velocity = 100;

        private int CellSize = 20;
        private int NumCells;

        private int LogCallNumber = 0;

        private System.Windows.Forms.Timer? _timer = null;

        private Pen BlackPen = new Pen(Color.Black, 1);
        private Pen RedPen = new Pen(Color.Red, 1);
        private Pen WhitePen = new Pen(Color.White, 1);

        public Form1()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(221, 221, 221);
            this.CenterToScreen();
            this.NumCells = (int)Math.Floor((float)this.ClientSize.Width / CellSize);
        }

        private void Form1_Paint(object sender, PaintEventArgs pe)
        { 
            Graphics g = pe.Graphics;

            for (int i = 1; i < NumCells; i++)
            {
                //Vertical Lines
                g.DrawLine(BlackPen, i * CellSize, 0, i * CellSize, NumCells * CellSize);

                //Horizontal Lines
                g.DrawLine(BlackPen, 0, i * CellSize, NumCells * CellSize, i * CellSize);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            Pen blackPen = new Pen(Color.Black, 20);

            if (this.Move == true)
            {
                var lpnt1 = new PointF(X * CellSize / 2, Y1 * 1);
                var lpnt2 = new PointF(X * CellSize / 2, Y2 * 1);
                g.DrawLine(blackPen, lpnt1, lpnt2);
            }

            Log();
        }

        private void MoveLeft()
        {
            while (Move == true)
            {
                if (X <= this.NumCells)
                {
                    X = 57;
                    this.Invalidate();
                    this.Update();
                    break;
                }

                X -= MoveX;
                this.Invalidate();
                this.Update();
            }
        }

        protected override void OnKeyDown(KeyEventArgs ke)
        {
            base.OnKeyDown(ke);

            if (ke.KeyCode == Keys.Escape)
            {
                this._timer.Dispose();
                this.Close();
            }

            if (this._timer == null)
            {
                this._timer = new System.Windows.Forms.Timer();
                this._timer.Enabled = false;
                this._timer.Interval = Velocity;
                this._timer.Tick += (s, e) =>
                {
                    this.X = this.X > this.NumCells * 2 ? 1 : this.X + this.MoveX;
                };
            }

            if (ke.KeyCode == Keys.D) 
            {
                this._timer.Enabled = true;
            }

            if (ke.KeyCode == Keys.A)
            {
                this.Move = true;

                while (Move == true)
                {
                    if (X <= this.NumCells)
                    {
                        X = 57;
                        this.Invalidate();
                        this.Update();
                        break;
                    }

                    X -= MoveX;
                    this.Invalidate();
                    this.Update();
                }
            }

            if (ke.KeyCode == Keys.W)
            {

                this.Move = true;

                while (Move == true)
                {
                    Debug.WriteLine(Y1);
                    Debug.WriteLine(this.Height);
                    if (Y1 < this.Height)
                    {
                        Y1 = 540;
                        Y2 = 560;
                        this.Invalidate();
                        this.Update();
                        break;
                    }

                    Y1 -= MoveY;
                    Y2 -= MoveY;
                    this.Invalidate();
                    this.Update();
                }
            }

            if (ke.KeyCode == Keys.S)
            {

                this.Move = true;

                while (Move == true)
                {
                    if (Y1 >= this.Height)
                    {
                        Y1 = 0;
                        Y2 = 20;
                        this.Invalidate();
                        this.Update();
                        break;
                    }

                    Y1 += MoveY;
                    Y2 += MoveY;
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        private void Log()
        {
            LogCallNumber++;
            Debug.WriteLine
            (
                $"LogCallNum: {this.LogCallNumber}\n" +
                $"ClientSize: {this.ClientSize}\n" +
                $"Width: {this.Width}\n" +
                $"Height: {this.Height}\n" +
                $"MoveX: {this.MoveX}\n" +
                $"MoveY: {this.MoveY}\n" +
                $"Velocity: {this.Velocity}\n" +
                $"CellSize: {this.CellSize}\n" +
                $"NumCells: {this.NumCells}\n" +
                $"X: {this.X}\n" +
                $"Y1: {this.Y1}\n" +
                $"Y2: {this.Y2}\n\n" 
            );
        }
    }
}