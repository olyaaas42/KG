using Dino.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dino
{
    public partial class Form1 : Form
    {
        Player player;
        Timer mainTimer;
        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.Width = 700;
            this.Height = 300;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(DrawGame);
            mainTimer = new Timer();
            mainTimer.Interval = 10;
            mainTimer.Tick += new EventHandler(Update);

            Init();
        }
        public void Init()
        {
            GameController.Init();
            player = new Player(new PointF(0, 149), new Size(150, 50));
            mainTimer.Start();
            Invalidate();
        }

        public void Update(object sender , EventArgs e)
        {
            GameController.MoveMap();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player.DrawSprite(g);
            GameController.DrawObjets(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
