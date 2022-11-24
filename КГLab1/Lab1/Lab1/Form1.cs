using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += button1_Click;

            Text = "Hello World!";
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            this.Size = new Size(500, 350);

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Привет");
        }

        /// <summary>
        /// this представляет ссылку на текущий объект - объект Form1, 
        /// то при создании второй формы она будет получать ее (ссылку) и через нее управлять первой формой.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {

            Form1 newForm1 = new Form1();
            newForm1.Show();

            Form2 newForm2 = new Form2(newForm1);
            newForm2.Show();
        }
    }
}
