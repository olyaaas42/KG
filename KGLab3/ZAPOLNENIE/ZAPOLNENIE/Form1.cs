using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZAPOLNENIE
{
    public partial class Form1 : Form
    {
        public int xn, yn, xk, yk; // концы отрезка
        Bitmap mybitmap; // объект Bitmap для вывода отрезка
        Color current_color; // текущий цвет отрезка
        Color current_color1; //цвет заливки
        Point click;
        public int xk1, yk1, xn1, yn1; // концы отрезка\
        

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == System.Windows.Forms.DialogResult.OK)
            {
                current_color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //отключаем кнопки
            button1.Enabled = false;
            button3.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            //создаем новый экземпляр Bitmap размером с элемент
            //pictureBox1 (mybitmap)
            //на нем выводим попиксельно отрезок
            mybitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            {
                if (radioButton1.Checked == true)
                {
                    //рисуем прямоугольник
                    CDA(10, 10, 10, 110);
                    CDA(10, 10, 110, 10);
                    CDA(10, 110, 110, 110);
                    CDA(110, 10, 110, 110);
                    //рисуем треугольник
                    CDA(150, 50, 150, 150);
                    CDA(150, 50, 150, 150);
                    CDA(250, 50, 150, 150);
                    CDA(150, 50, 250, 150);
                    //рисуем многоугольник
                    CDA(10, 150, 10, 250);
                    CDA(10, 150, 50, 150);
                    CDA(10, 250, 100, 250);
                    CDA(50, 150, 100, 250);
                    CDA(100, 250, 150, 300);
                    CDA(100, 250, 150, 250);
                    CDA(150, 250, 150, 300);
                }
                else
                {
                    if (radioButton2.Checked == true)
                    {
                        //получаем растр созданного рисунка в mybitmap
                        mybitmap = pictureBox1.Image as Bitmap;

                        // задаем координаты затравки
                        xn = click.X;
                        yn = click.Y;
                        // вызываем рекурсивную процедуру заливки с затравкой
                        Zaliv(xn, yn);
                    }
                }
                //передаем полученный растр mybitmap в элемент pictureBox
                pictureBox1.Image = mybitmap;
                // обновляем pictureBox и активируем кнопки
                pictureBox1.Refresh();
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult D1 = colorDialog2.ShowDialog();
            if (D1 == System.Windows.Forms.DialogResult.OK)
            {
                current_color1 = colorDialog2.Color;
                ((Button)sender).BackColor = colorDialog2.Color;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                click = e.Location;
            }
            if (radioButton1.Checked == true)
            {
                xn1 = e.X;
                yn1 = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int i, n;
            double xt, yt, dx, dy;

            xn = xn1;
            yn = yn1;
            xk = 0;
            yk = 0;
            xk = e.X;
            yk = e.Y;
            dx = xk - xn1;
            dy = yk - yn1;
            n = 100;
            xt = xn1;
            yt = yn1;
            for (i = 1; i <= n; i++)
            {
                Pen pen = new Pen(current_color, 1);
                mybitmap.SetPixel((int)xt, (int)yt, current_color);
                xt = xt + dx / n;
                yt = yt + dy / n;
            }
        }

        // вывод отрезка
        private void CDA(int x1, int y1, int x2, int y2)
        {
            int i, n;
            double xt, yt, dx, dy;

            xn = x1;
            yn = y1;
            xk = x2;
            yk = y2;
            dx = xk - xn;
            dy = yk - yn;
            n = 100;
            xt = xn;
            yt = yn;
            for (i = 1; i <= n; i++)
            {
                Pen myPen = new Pen(current_color, 1);
                mybitmap.SetPixel((int)xt, (int)yt, current_color);
                xt = xt + dx / n;
                yt = yt + dy / n;
            }
        }

        private void Zaliv(int x1, int y1)
        {
            // получаем цвет текущего пикселя с координатами x1, y1
            Color old_color = mybitmap.GetPixel(x1, y1);

            // сравнение цветов происходит в формате RGB
            // для этого используем метод ToArgb объекта Color
            if ((old_color.ToArgb() != current_color.ToArgb()) && (old_color.ToArgb() != current_color1.ToArgb()))
            {
                //перекрашиваем пиксель
                mybitmap.SetPixel(x1, y1, current_color1);

                //вызываем метод для 4-х соседних пикселей
                Zaliv(x1 + 1, y1);
                Zaliv(x1 - 1, y1);
                Zaliv(x1, y1 + 1);
                Zaliv(x1, y1 - 1);
            }
            else
            {
                //выходим из метода 
                return;
            }

        }

    }
}
