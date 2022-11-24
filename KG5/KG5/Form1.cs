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

namespace KG5
{
    public partial class Form1 : Form
    {
        bool f;
        float[,] pyramid = new float[6, 4]; // матрица тела
        float[,] osi = new float[4, 4]; // матрица координат осей
        float[,] matr_sdv = new float[4, 4]; //матрица преобразования
        int k, l, n; // элементы матрицы сдвига

        public Form1()
        {
            InitializeComponent();
        }

        //инициализация матрицы тела
        private void Init_pyramid()
        {
            pyramid[0, 0] = 0; pyramid[0, 1] = -100; pyramid[0, 2] = 50; pyramid[0, 3] = 1;
            pyramid[1, 0] = 50; pyramid[1, 1] = 30; pyramid[1, 2] = 0; pyramid[1, 3] = 1;
            pyramid[2, 0] = 0; pyramid[2, 1] = -100; pyramid[2, 2] = -50; pyramid[2, 3] = 1;
            pyramid[3, 0] = -50; pyramid[3, 1] = 30; pyramid[3, 2] = 0; pyramid[3, 3] = 1;
            pyramid[4, 0] = 50; pyramid[4, 1] = 100; pyramid[4, 2] = 0; pyramid[4, 3] = 1;
            pyramid[5, 0] = 0; pyramid[5, 1] = 10; pyramid[5, 2] = 10; pyramid[5, 3] = 1;

        }

        //инициализация матрицы осей
        private void Init_osi()
        {
            osi[0, 0] = -200; osi[0, 1] = 0; osi[0, 2] = 0; osi[0, 3] = 1;
            osi[1, 0] = 200; osi[1, 1] = 0; osi[1, 2] = 0; osi[1, 3] = 1;
            osi[2, 0] = 0; osi[2, 1] = 200; osi[2, 2] = 0; osi[2, 3] = 1;
            osi[3, 0] = 0; osi[3, 1] = -200; osi[3, 2] = 0; osi[3, 3] = 1;
        }

        //инициализация матрицы сдвига
        private void Init_matr_preob(int x1, int y1, int z1)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0; matr_sdv[0, 3] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0; matr_sdv[1, 3] = 0;
            matr_sdv[2, 0] = 0; matr_sdv[2, 1] = 0; matr_sdv[2, 2] = 1; matr_sdv[2, 3] = 0;
            matr_sdv[3, 0] = x1; matr_sdv[3, 1] = y1; matr_sdv[3, 2] = 1; matr_sdv[3, 3] = 1;
        }


        private void Draw_pyramid()
        {
            Init_pyramid(); //инициализация матрицы тела
            Init_matr_preob(k, l, n); //инициализация матрицы преобразования
            float[,] kv1 = Multiply_matr(pyramid, matr_sdv); //перемножение матриц

            Pen myPen = new Pen(Color.Blue, 2);// цвет линии и ширина

            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv1[0, 0], kv1[0, 1], kv1[1, 0], kv1[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv1[1, 0], kv1[1, 1], kv1[2, 0], kv1[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv1[2, 0], kv1[2, 1], kv1[3, 0], kv1[3, 1]);
            // рисуем 4 сторону квадрата 
           // g.DrawLine(myPen, kv1[3, 0], kv1[3, 1], kv1[0, 0], kv1[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Draw_osi();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //середина pictureBox
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            n = 1;
            Draw_pyramid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;

            button1.Text = "Стоп";
            if (f == true)
                timer1.Start();
            else
            {
                timer1.Stop();
                button1.Text = "Вращение";
            }
            f = !f;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            ///k++;
            Draw_pyramid();
            Thread.Sleep(100);
        }

        private float[,] Multiply_matr(float[,] a, float[,] b)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            float[,] r = new float[n, m];
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

        //вывод осей на экран
        private void Draw_osi()
        {
            Init_osi();
            Init_matr_preob(k, l, n);
            float[,] osi1 = Multiply_matr(osi, matr_sdv);

            Pen myPen = new Pen(Color.Red, 1);// цвет линии и ширина
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем ось ОХ
            g.DrawLine(myPen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1, 1]);
            // рисуем ось ОУ
            g.DrawLine(myPen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3, 1]);
            g.Dispose();
            myPen.Dispose();
        }


    }
}
