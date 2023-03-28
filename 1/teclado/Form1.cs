using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace teclado
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        
        private List<Point> buttonPositions = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            buttonPositions.Add(simpleButton1.Location);
            buttonPositions.Add(simpleButton2.Location);
            buttonPositions.Add(simpleButton3.Location);
            buttonPositions.Add(simpleButton4.Location);
            buttonPositions.Add(simpleButton5.Location);
            buttonPositions.Add(simpleButton6.Location);
            buttonPositions.Add(simpleButton7.Location);
            buttonPositions.Add(simpleButton8.Location);
            buttonPositions.Add(simpleButton9.Location);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Misturar(buttonPositions); 

            simpleButton1.Location = buttonPositions[0];
            simpleButton2.Location = buttonPositions[1];
            simpleButton3.Location = buttonPositions[2];
            simpleButton4.Location = buttonPositions[3];
            simpleButton5.Location = buttonPositions[4];
            simpleButton6.Location = buttonPositions[5];
            simpleButton7.Location = buttonPositions[6];
            simpleButton8.Location = buttonPositions[7];
            simpleButton9.Location = buttonPositions[8];

        }

        private void Misturar<T>(IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SendKeys.Send("1");
        }
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SendKeys.Send("2");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SendKeys.Send("3");
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SendKeys.Send("4");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SendKeys.Send("5");
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            SendKeys.Send("6");
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            SendKeys.Send("7");
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SendKeys.Send("8");
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            SendKeys.Send("9");
        }
    }
}
