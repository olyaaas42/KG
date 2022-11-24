using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2D
{
    public partial class Form1 : Form
    {
        int[,] kv = new int[4, 3]; // матрица тела
        int[,] osi = new int[4, 3]; // матрица координат осей
        int[,] matr_sdv = new int[4, 3]; //матрица преобразования
        int k, l; // элементы матрицы сдвига
        int k2, l2; // элементы матрицы сдвига
        bool f = true;

       

        public Form1()
        {
            InitializeComponent();
        }

        //инициализация матрицы тела
        private void Init_kvadrat()
        {
            kv[0, 0] = -50; kv[0, 1] = 0; kv[0, 2] = 1;
            kv[1, 0] = 0; kv[1, 1] = 50; kv[1, 2] = 1;
            kv[2, 0] = 50; kv[2, 1] = 0; kv[2, 2] = 1;
            kv[3, 0] = 0; kv[3, 1] = -50; kv[3, 2] = 1;
        }

        //инициализация матрицы сдвига
        private void Init_matr_preob(int k1, int l1)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k1; matr_sdv[2, 1] = l1; matr_sdv[2, 2] = 1;
        }

        //инициализация матрицы осей
        private void Init_osi()
        {
            osi[0, 0] = -200; osi[0, 1] = 0; osi[0, 2] = 1;
            osi[1, 0] = 200; osi[1, 1] = 0; osi[1, 2] = 1;
            osi[2, 0] = 0; osi[2, 1] = 200; osi[2, 2] = 1;
            osi[3, 0] = 0; osi[3, 1] = -200; osi[3, 2] = 1;

        }

        //умножение матриц
        private int[,] Multiply_matr(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            int[,] r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int ii = 0; ii < m; ii++)
                    {
                        r[i, j] += a[i, ii] * b[ii, j];
                    }
                }
            }
            return r;
        }

        //вывод квадрата на экран
        private void Draw_Kv()
        {
            Init_kvadrat(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            int[,] kv1 = Multiply_matr(kv, matr_sdv); //перемножение матриц

            Pen myPen = new Pen(Color.Black, 2);// цвет линии и ширина
            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[2, 0], kv1[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            // рисуем 4 сторону квадрата 
            g.DrawLine(myPen, kv1[3, 0], kv1[3, 1], kv1[0, 0], kv1[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        
        //вывод квадратика в центре pictureBox
        private void button2_Click(object sender, EventArgs e)
        {
            //середина pictureBox
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            //вывод квадратика в середине
            Draw_Kv();
        }

        //вывод осей на экран
        private void Draw_osi()
        {
            Init_osi();
            Init_matr_preob(k2, l2);
            int[,] osi1 = Multiply_matr(osi, matr_sdv);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            Pen pen = new Pen(Color.Red, 1);// цвет линии и ширина
            // рисуем ось ОХ
            g.DrawLine(pen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1,
            1]);
            // рисуем ось ОУ
            g.DrawLine(pen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3,
            1]);
        }

        //вывод осей в центре pictureBox
        private void button1_Click(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
        }

        //сдвиг вправо
        private void button4_Click(object sender, EventArgs e)
        {
            k += 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Kv(); //вывод квадратика
        }

        //непрерывное перемещение
        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;

            button8.Text = "Стоп";
            if (f == true)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                button8.Text = "Старт";
            }
            f = !f;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            k -= 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Kv(); //вывод квадратика
        }

        private void button6_Click(object sender, EventArgs e)
        {
            l += 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Kv(); //вывод квадратика
        }

        private void button7_Click(object sender, EventArgs e)
        {
            l -= 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Kv(); //вывод квадратика
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer2.Interval = 100;

            button9.Text = "Стоп";
            if (f == true)
            {
                timer2.Start();
            }
            else
            {
                timer2.Stop();
                button9.Text = "Старт";
            }
            f = !f;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer3.Interval = 100;

            button10.Text = "Стоп";
            if (f == true)
            {
                timer3.Start();
            }
            else
            {
                timer3.Stop();
                button10.Text = "Старт";
            }
            f = !f;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer4.Interval = 100;

            button11.Text = "Стоп";
            if (f == true)
            {
                timer4.Start();
            }
            else
            {
                timer4.Stop();
                button11.Text = "Старт";
            }
            f = !f;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            k--;
            Draw_Kv();
            Thread.Sleep(100);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            l++;
            Draw_Kv();
            Thread.Sleep(100);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            l--;
            Draw_Kv();
            Thread.Sleep(100);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            k++;
            Draw_Kv();
            Thread.Sleep(100);  //Время измеряется в миллисекундах.
                                //задержку выполнения текущего потока на заданный интервал времени
        }



    }
}
