using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TrackBar = System.Windows.Forms.TrackBar;

namespace LINII
{
    public partial class Form1 : Form
    {
        public int xn, yn, xk, yk;
        Pen pen = new Pen(Color.Black, 1);
        Pen myPen = new Pen(Color.Black, 1);
        

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true 
                || radioButton4.Checked == true || radioButton5.Checked == true)
            {
                xn = e.X;
                yn = e.Y;
       
                if (radioButton3.Checked == true)
                {
                    Graphics myGraphics = pictureBox1.CreateGraphics();
                    myGraphics.DrawRectangle(myPen, 0, 0, 50, 50);

                }
                if (radioButton4.Checked == true)
                {
                    Graphics myGraphics = pictureBox1.CreateGraphics();
                    myGraphics.DrawEllipse(myPen, 0, 0, 100, 100);

                }
                if (radioButton5.Checked == true)
                {
                    Graphics myGraphics = pictureBox1.CreateGraphics();
                    PointF point1 = new PointF(0, 0);
                    PointF point2 = new PointF(0, 200.0F);
                    PointF point3 = new PointF(250.0F, 150.0F);
                    PointF point4 = new PointF(250.0F, 250.0F);
                    PointF point5 = new PointF(50.0F, 0);
                    PointF[] curvePoints =
                        {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 };
                    myGraphics.DrawPolygon(myPen, curvePoints);

                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали алгоритм вывода фигуры!");
            } 

            float kx, ky, kx1, ky1;
            kx = 0; ky = 0; kx1 = 0; ky1 = 0;
            if (textBox1.Text == "")
            {
                return;
            }
            kx = int.Parse(textBox1.Text);
            if (textBox2.Text == "")
            {
                return;
            }
            ky = int.Parse(textBox2.Text);
            if (textBox3.Text == "")
            {
                return;
            }
            kx1 = int.Parse(textBox3.Text);
            if (textBox4.Text == "")
            {
                return;
            }
            ky1 = int.Parse(textBox4.Text);
            if (kx != 0 && ky != 0 && kx1 != 0 && ky1 != 0)
            {
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                g.DrawLine(myPen, kx, ky, kx1, ky1);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int i, n;
            double xt, yt, dx, dy;
            xk = 0;
            yk = 0;
            xk = e.X;
            yk = e.Y;

            dx = xk - xn;
            dy = yk - yn;
            n = 100;
            xt = xn;
            yt = yn;
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                for (i = 1; i <= n; i++)
                {
                    //Объявляем объект "g" класса Graphics и предоставляем
                    //ему возможность рисования на pictureBox1:
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    //Рисуем прямоугольник:
                    g.DrawRectangle(pen, (int)xt, (int)yt, 2, 2);
                    //Рисуем закрашенный прямоугольник:
                    //Объявляем объект "redBrush", задающий цвет кисти
                    //SolidBrush redBrush = new SolidBrush(Color.Black);
                    //Рисуем закрашенный прямоугольник
                    //g.FillRectangle(redBrush, (int)xt, (int)yt, 2, 2);
                    xt = xt + dx / n;
                    yt = yt + dy / n;
                }
            }
            if (radioButton2.Checked == true)
            {
                for (i = 1; i <= n; i++)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    //Объявляем объект "g" класса Graphics и предоставляем
                    //ему возможность рисования на pictureBox1:
                    Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                    //Рисуем прямоугольник:
                    //g.DrawLine(pen, new Point((int)xt), new Point((int)yt));
                    g.DrawLine(pen, e.X, e.Y, (int)xt, (int)yt);
                    //Рисуем закрашенный прямоугольник:
                    //Объявляем объект "redBrush", задающий цвет кисти
                    //SolidBrush redBrush = new SolidBrush(Color.Red);
                    //Рисуем закрашенный прямоугольник
                    //g.FillRectangle(redBrush, (int)xt, (int)yt, 2, 2);
                    xt = xt + dx / n;
                    yt = yt + dy / n;

                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    //Задаем цвет пикселя по схеме RGB (от 0 до 255 для каждого цвета)
                    Color newColor = Color.FromArgb(247, 249, 239);
                    myBitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = myBitmap;
            // pictureBox1.Image = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
