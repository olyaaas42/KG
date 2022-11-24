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
    public partial class Form2 : Form
    {
        
        public Form2() => InitializeComponent();

        int[,] fig = new int[4, 3]; // матрица тела
        int[,] osi = new int[4, 3]; // матрица координат осей
        int[,] matr_sdv = new int[4, 3]; //матрица преобразования
        int[,] matr_sdv1 = new int[4, 3]; //матрица преобразования
        int[,] matr_sdv2 = new int[4, 3]; //матрица преобразования
        int[,] matr_sdv3 = new int[4, 3]; //матрица преобразования
        int k, l; // элементы матрицы сдвига
        int k2, l2; // элементы матрицы сдвига
        int m, n; 
        bool f = true;

        //инициализация матрицы тела
        private void Init_fig()
        {
            fig[0, 0] = -50; fig[0, 1] = -50; fig[0, 2] = 1;
            fig[1, 0] = -50; fig[1, 1] = 70; fig[1, 2] = 1;
            fig[2, 0] = 50; fig[2, 1] = 60; fig[2, 2] = 1;
            fig[3, 0] = 0; fig[3, 1] = -50; fig[3, 2] = 1;
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


        //умножение матриц
        private int[,] Multiply_matr1(int[,] a, int[,] b, int x, int y)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            int[,] r = new int[n, m];
            if (x > 1)
            {
                r[0, 0] = a[0, 0] * b[0, 0]; r[0, 1] = a[0, 1]; r[0, 2] = a[0, 2];
                r[1, 0] = a[1, 0] * b[0, 0]; r[1, 1] = a[1, 1]; r[1, 2] = a[1, 2];
                r[2, 0] = a[2, 0] * b[0, 0]; r[2, 1] = a[2, 1]; r[2, 2] = a[2, 2];
                r[3, 0] = a[3, 0] * b[0, 0]; r[3, 1] = a[3, 1]; r[3, 2] = a[3, 2];

            }
            if (y > 1)
            {
                r[0, 0] = a[0, 0]; r[0, 1] = a[0, 1] * b[1, 1]; r[0, 2] = a[0, 2];
                r[1, 0] = a[1, 0]; r[1, 1] = a[1, 1] * b[1, 1]; r[1, 2] = a[1, 2];
                r[2, 0] = a[2, 0]; r[2, 1] = a[2, 1] * b[1, 1]; r[2, 2] = a[2, 2];
                r[3, 0] = a[3, 0]; r[3, 1] = a[3, 1] * b[1, 1]; r[3, 2] = a[3, 2];
            }
            return r;
        }

        //вывод квадрата на экран
        private void Draw_Fig()
        {
            Init_fig(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            int[,] kv1 = Multiply_matr(fig, matr_sdv); //перемножение матриц

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

        private void Draw_Fig1()
        {
            Init_fig(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            Init_matr_scale(n, m);
            int[,] kv3 = Multiply_matr1(fig, matr_sdv1, n, m); //перемножение матриц
            int[,] kv2 = Multiply_matr(kv3, matr_sdv); //перемножение матриц

            Pen myPen = new Pen(Color.Black, 2);// цвет линии и ширина
            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv2[0, 0], kv2[0, 1], kv2[1, 0], kv2[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv2[1, 0], kv2[1, 1], kv2[2, 0], kv2[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv2[2, 0], kv2[2, 1], kv2[3, 0], kv2[3, 1]);
            // рисуем 4 сторону квадрата 
            g.DrawLine(myPen, kv2[3, 0], kv2[3, 1], kv2[0, 0], kv2[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
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
            g.DrawLine(pen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1,1]);
            // рисуем ось ОУ
            g.DrawLine(pen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3,1]);
            g.Dispose();
            pen.Dispose();
        }

        //вывод осей в центре pictureBox
        private void button1_Click_1(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();

            k += 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Fig(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();

            k -= 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Fig();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            
            l += 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Fig();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            l -= 5; //изменение соответствующего элемента матрицы сдвига 
            Draw_Fig();
        }

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

        //вывод фигуры в центре pictureBox
        private void button2_Click_1(object sender, EventArgs e)
        {
            //середина pictureBox
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            //вывод фигуры в середине
            Draw_Fig();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            k--;
            Draw_Fig();
            Thread.Sleep(100);
        }

        private void timer3_Tick_1(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            l++;
            Draw_Fig();
            Thread.Sleep(100);
        }

        private void timer4_Tick_1(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            l--;
            Draw_Fig();
            Thread.Sleep(100);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            n = 2;
            m = 1;
            Draw_Fig1();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            n = 1;
            m = 2;
            Draw_Fig1();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            k2 = pictureBox1.Width / 2;
            l2 = pictureBox1.Height / 2;
            Draw_osi();
            pictureBox1.Image = null;
            k++;
            Draw_Fig();
            Thread.Sleep(100);  
        }

        private void button19_Click(object sender, EventArgs e)
        {
            n = 1;
            m = -1;
            Draw_Fig2();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            n = -1;
            m = 1;
            Draw_Fig2();
        }

        //масштабирование
        private void Init_matr_scale(int k1, int l1)
        {
            matr_sdv1[0, 0] = k1; matr_sdv1[0, 1] = 0; matr_sdv1[0, 2] = 0;
            matr_sdv1[1, 0] = 0; matr_sdv1[1, 1] = l1; matr_sdv1[1, 2] = 0;
            matr_sdv1[2, 0] = 0; matr_sdv1[2, 1] = 0; matr_sdv1[2, 2] = 1;
        }

        private void button20_Click(object sender, EventArgs e)
        {
           
            n = -1;
            m = -1;
            Draw_Fig2();
        }

        //поворот
        private void Init_matr_turn(int k1, int l1)
        {
            matr_sdv2[0, 0] = 0; matr_sdv2[0, 1] = k1; matr_sdv2[0, 2] = 0;
            matr_sdv2[1, 0] = l1; matr_sdv2[1, 1] = 0; matr_sdv2[1, 2] = 0;
            matr_sdv2[2, 0] = 0; matr_sdv2[2, 1] = 0; matr_sdv2[2, 2] = 1;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            n = 1;
            m = -1;
            Draw_Fig3();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            n = -1;
            m = -1;
            Draw_Fig3();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            n = -1;
            m = 1;
            Draw_Fig3();
        }


        //отражение
        private void Init_matr_mir(int k1, int l1)
        {
            matr_sdv3[0, 0] = k1; matr_sdv3[0, 1] = 0; matr_sdv3[0, 2] = 0;
            matr_sdv3[1, 0] = 0; matr_sdv3[1, 1] = l1; matr_sdv3[1, 2] = 0;
            matr_sdv3[2, 0] = 0; matr_sdv3[2, 1] = 0; matr_sdv3[2, 2] = 1;
        }

        private void Draw_Fig2()
        {
            Init_fig(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            Init_matr_mir(n, m);
            int[,] kv3 = Multiply_matr(fig, matr_sdv3); //перемножение матриц
            int[,] kv2 = Multiply_matr(kv3, matr_sdv); //перемножение матриц

            Pen myPen = new Pen(Color.Black, 2);// цвет линии и ширина
            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv2[0, 0], kv2[0, 1], kv2[1, 0], kv2[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv2[1, 0], kv2[1, 1], kv2[2, 0], kv2[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv2[2, 0], kv2[2, 1], kv2[3, 0], kv2[3, 1]);
            // рисуем 4 сторону квадрата 
            g.DrawLine(myPen, kv2[3, 0], kv2[3, 1], kv2[0, 0], kv2[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }
        private void Draw_Fig3()
        {
            Init_fig(); //инициализация матрицы тела
            Init_matr_preob(k, l); //инициализация матрицы преобразования
            Init_matr_turn(n, m);
            int[,] kv3 = Multiply_matr(fig, matr_sdv2); //перемножение матриц
            int[,] kv2 = Multiply_matr(kv3, matr_sdv); //перемножение матриц

            Pen myPen = new Pen(Color.Black, 2);// цвет линии и ширина
            //создаем новый объект Graphics (поверхность рисования) из pictureBox
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            // рисуем 1 сторону квадрата
            g.DrawLine(myPen, kv2[0, 0], kv2[0, 1], kv2[1, 0], kv2[1, 1]);
            // рисуем 2 сторону квадрата
            g.DrawLine(myPen, kv2[1, 0], kv2[1, 1], kv2[2, 0], kv2[2, 1]);
            // рисуем 3 сторону квадрата
            g.DrawLine(myPen, kv2[2, 0], kv2[2, 1], kv2[3, 0], kv2[3, 1]);
            // рисуем 4 сторону квадрата 
            g.DrawLine(myPen, kv2[3, 0], kv2[3, 1], kv2[0, 0], kv2[0, 1]);
            g.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
            myPen.Dispose(); //освобождвем ресурсы, связанные с Pen
        }
    }
}
