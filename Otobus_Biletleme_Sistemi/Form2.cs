using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otobus_Biletleme_Sistemi
{
    public partial class Form2 : Form
    {
        Yolculuk yolculuk;

        public Form2(Yolculuk yolculuk)
        {
            InitializeComponent();

            this.yolculuk = yolculuk;
            Label[] destinationLabels = { label1, label3, label5, label7, label9};

            foreach (Label label in destinationLabels)
            {
                label.Text = yolculuk.Nereden + "  <--->  " + yolculuk.Nereye;
            }


            Label[] dateLabels = { label2, label4, label6, label8, label10 };

            if (yolculuk.Donus_tarihi == null)
            {
                foreach(Label label in dateLabels) 
                {
                    label.Text = yolculuk.Gidis_tarihi;
                }
                
            } else
            {
                foreach (Label label in dateLabels) 
                {
                    label.Text = yolculuk.Gidis_tarihi + "  <--->  " + yolculuk.Donus_tarihi;
                }
                
            }
            Label[] timeLabels = { label11, label12, label13, label14, label15 };
            String[] timeValues = {"09:00", "12:00", "15:00", "19:00", "23:30" };
            int i = 0;
            foreach (Label label in timeLabels) 
            {
                label.Text = timeValues[i]; i++;
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3(yolculuk, label16.Text);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3(yolculuk, label17.Text);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3(yolculuk, label18.Text);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void panel4_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3(yolculuk, label19.Text);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void panel5_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var form3 = new Form3(yolculuk, label20.Text);
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }
    }
}
