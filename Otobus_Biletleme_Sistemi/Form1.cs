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
    public partial class Form1 : Form
    {
        int gidisDonus = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            gidisDonus = 0;
            monthCalendar2.Hide();
            label2.Hide();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            gidisDonus = 1;
            monthCalendar2.Show();
            label2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string donus_tarihi = null;
            if (gidisDonus == 1)
            {
                donus_tarihi = monthCalendar2.SelectionRange.Start.ToString();
            }

            Yolculuk yolculuk = new Yolculuk(comboBox1.GetItemText(comboBox1.SelectedItem),
                comboBox2.GetItemText(comboBox2.SelectedItem),
                monthCalendar1.SelectionRange.Start.ToString(),
                donus_tarihi, numericUpDown1.Value);
            
            this.Hide();
            var form2 = new Form2(yolculuk);
            form2.Closed += (s, args) => this.Close();
            form2.Show();
            //this.Close();
        }
    }

    public class Yolculuk : Object
    {
        string nereden;
        string nereye;
        string gidis_tarihi;
        string donus_tarihi;
        decimal yolcu_say;

        public Yolculuk(string nereden, string nereye, string gidis_tarihi,
            string donus_tarihi, decimal yolcu_say)
        {
            this.Nereden = nereden;
            this.Nereye = nereye;
            this.Gidis_tarihi = gidis_tarihi.Substring(0,10);

            if (donus_tarihi == null) { this.Donus_tarihi = donus_tarihi; } 
            else { this.Donus_tarihi = donus_tarihi.Substring(0, 10); }
            
            this.Yolcu_say = yolcu_say;
        }

        public string Nereden { get => nereden; set => nereden = value; }
        public string Nereye { get => nereye; set => nereye = value; }
        public string Gidis_tarihi { get => gidis_tarihi; set => gidis_tarihi = value; }
        public string Donus_tarihi { get => donus_tarihi; set => donus_tarihi = value; }
        public decimal Yolcu_say { get => yolcu_say; set => yolcu_say = value; }
    }

}
