using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matrices
{
    public partial class Form3 : Form
    {
        const int MaxN = 10; // максимально допустимая размерность матрицы
        int n; // текущая размерность матрицы
        TextBox[] MatrText = null; // матрица элементов типа TextBox
        double[] Matr1 = new double[MaxN]; // матрица 1 чисел с плавающей точкой
        double[] Matr2 = new double[MaxN]; // матрица 2 чисел с плавающей точкой
        double[] Matr3 = new double[MaxN]; // матрица результатов
        bool f1; // флажок, который указывает о вводе данных в матрицу Matr1
        bool f2; // флажок, который указывает о вводе данных в матрицу Matr2
        int dx = 40, dy = 20; // ширина и высота ячейки в MatrText[,]
        Form4 form4 = null;   // экземпляр (объект) класса формы Form2

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }
            n = int.Parse(textBox1.Text);

            Clear_MatrText();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i].TabIndex = i * n + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i].Visible = true;
                }
            }
            // 4. Корректировка размеров формы
            form4.Width = 10 + n * dx + 20;
            form4.Height = 10 + n * dy + form4.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form4
            form4.button1.Left = 10;
            form4.button1.Top = 10 + n * dy + 10;
            form4.button1.Width = form4.Width - 30;

            // 6. Вызов формы Form4
            if (form4.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr1
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (MatrText[i].Text != "")
                        {
                            Matr1[i] = Double.Parse(MatrText[i].Text);
                        }
                        else
                        {
                            Matr1[i] = 0;
                        }
                    }
                }
                // 8. Данные в матрицу Matr1 внесены
                f1 = true;
                label1.Text = "true";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Чтение размерности матрицы
            if (textBox1.Text == "")
            {
                return;
            }
            n = int.Parse(textBox1.Text);

            // 2. Обнулить ячейки MatrText
            Clear_MatrText();

            // 3. Настройка свойств ячеек матрицы MatrText
            //    с привязкой к значению n и форме Form2
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i].TabIndex = i * n + j + 1;

                    // 3.2. Сделать ячейку видимой
                    MatrText[i].Visible = true;
                }
            }
            // 4. Корректировка размеров формы
            form4.Width = 10 + n * dx + 20;
            form4.Height = 10 + n * dy + form4.button1.Height + 50;

            // 5. Корректировка позиции и размеров кнопки на форме Form2
            form4.button1.Left = 10;
            form4.button1.Top = 10 + n * dy + 10;
            form4.button1.Width = form4.Width - 30;

            // 6. Вызов формы Form2
            if (form4.ShowDialog() == DialogResult.OK)
            {
                // 7. Перенос строк из формы Form2 в матрицу Matr2
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Matr2[i] = Double.Parse(MatrText[i].Text);
                    }
                }

                // 8. Матрица Matr2 сформирована
                f2 = true;
                label2.Text = "true";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!((f1 == true) && (f2 == true)))
            {
                return;
            }

            //векторное произведение векторов
            Matr3[0] = (Matr1[1] * Matr2[2]) - (Matr1[2] * Matr2[1]);
            Matr3[1] = (Matr1[0] * Matr2[2]) - (Matr1[2] * Matr2[0]);
            Matr3[2] = (Matr1[0] * Matr2[1]) - (Matr1[1] * Matr2[0]);

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i].TabIndex = i * n + j + 1;

                    // 3.2. Перевести число в строку
                    MatrText[i].Text = Matr3[i].ToString();
                }
            }

            // 4. Вывод формы
            form4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Проверка, введены ли данные хотя бы в одной матрице
            if (!((f1 == true) || (f2 == true)))
            {
                return;
            }

            // 2. Умножение вектора на константу
            for (int i = 0; i < n; i++)
            {
                Matr3[i] = Matr1[i] * n;
            }

            // 3. Внесение данных в MatrText
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // 3.1. Порядок табуляции
                    MatrText[i].TabIndex = i * n + j + 1;

                    // 3.2. Перевести число в строку
                    MatrText[i].Text = Matr3[i].ToString();
                }
            }

            // 4. Вывод формы
            form4.ShowDialog();
        }

        private void Clear_MatrText()
        {
            // Обнуление ячеек MatrText
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrText[i].Text = "0";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // І. Инициализация элементов управления и внутренних переменных
            textBox1.Text = "";
            f1 = f2 = false; // матрицы еще не заполнены
            label1.Text = "false";
            label2.Text = "false";

            // ІІ. Выделение памяти и настройка MatrText
            int i;

            // 1. Выделение памяти для формы Form2
            form4 = new Form4();

            // 2. Выделение памяти под самую матрицу
            MatrText = new TextBox[MaxN];

            // 3. Выделение памяти для каждой ячейки матрицы и ее настройка
            for (i = 0; i < MaxN; i++)
            {
                    // 3.1. Выделить память
                    MatrText[i] = new TextBox();

                    // 3.2. Обнулить эту ячейку
                    MatrText[i].Text = "0";

                    // 3.3. Установить позицию ячейки в форме Form2
                    MatrText[i].Location = new System.Drawing.Point(10 + i * dx, 10 + dy);

                    // 3.4. Установить размер ячейки
                    MatrText[i].Size = new System.Drawing.Size(dx, dy);

                    // 3.5. Пока что спрятать ячейку
                    MatrText[i].Visible = false;

                    // 3.6. Добавить MatrText[i,j] в форму form2
                    form4.Controls.Add(MatrText[i]);
            }

        }
    }
}
