using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Otobus_Biletleme_Sistemi
{
    public partial class Form1 : Form
    {
        int gidisDonus = 0;
        int ikinciSeferSecimi = 0;
        Yolculuk yolculuk;
        Yolcu yolcu;
        Yolcu yolcu2;
        Yolculuk donus;
        string sefer_no;
        string sefer_no2;
        string path_to_db = (Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"Database1.mdf");
        public Form1()
        {
            
            InitializeComponent();
            panel1.BringToFront();
            panel3.BackColor = Color.FromArgb(41, 41, 61);
            panel6.Visible = false;
            panel12.Visible = false;

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

            if (gidisDonus == 1)
            {
                donus = new Yolculuk(comboBox2.GetItemText(comboBox2.SelectedItem),
                    comboBox1.GetItemText(comboBox1.SelectedItem),
                monthCalendar2.SelectionRange.Start.ToString());
   

            }

            yolculuk = new Yolculuk(comboBox1.GetItemText(comboBox1.SelectedItem),
                comboBox2.GetItemText(comboBox2.SelectedItem),
                monthCalendar1.SelectionRange.Start.ToString());

            showPart2(yolculuk);
        }

        private void showPart2(Yolculuk yolculuk)
        {
            panel1.Visible = false;
            panel6.Visible = true;
            panel3.BackColor = Color.FromArgb(51, 51, 76);
            panel4.BackColor = Color.FromArgb(41, 41, 61);

            label5.Text = "SEFER SEÇİMİ (GİDİŞ)";

            label7.Text = label8.Text = label9.Text = label10.Text = label11.Text = yolculuk.Nereden + "  --> ";

            label12.Text = label13.Text = label14.Text = label15.Text = label16.Text = yolculuk.Nereye;

            label17.Text = "09:00";
            label18.Text = "15:00";
            label19.Text = "18:00";
            label20.Text = "21:00";
            label21.Text = "00:00";

            label22.Text = "120 TL";
            label23.Text = "120 TL";
            label24.Text = "150 TL";
            label25.Text = "130 TL";
            label26.Text = "100 TL";

        }

        private void showPart3()
        {
            if (gidisDonus == 1) 
            {
                showPart2(donus);
                gidisDonus = 0;
                ikinciSeferSecimi = 1;
                label5.Text = "SEFER SEÇİMİ (DÖNÜŞ)";
                label3.Text = "Koltuk Numarası (GİDİŞ)";
                label31.Visible = true;
                button37.Visible = true;
                numericUpDown2.Visible = true;
                return;
            }

            label5.Text = "SEFER SEÇİMİ";

            Hashtable plaka = PlakaOlusturucu();

            sefer_no = (string)plaka[yolculuk.Nereden] + (string)plaka[yolculuk.Nereye] + yolculuk.Saat.Replace(":", "") +
                   yolculuk.Gidis_tarihi.Replace("/", "");

            if (ikinciSeferSecimi == 1)
                sefer_no2 = (string)plaka[donus.Nereden] + (string)plaka[donus.Nereye] + donus.Saat.Substring(0, 2) + donus.Saat.Substring(3, 2) +
                   donus.Gidis_tarihi.Substring(0, 2) + donus.Gidis_tarihi.Substring(3, 2) + donus.Gidis_tarihi.Substring(8, 2);

            panel6.Visible = false;
            panel12.Visible = true;
            panel4.BackColor = Color.FromArgb(51, 51, 76);
            panel5.BackColor = Color.FromArgb(41, 41, 61);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 17 26
            if (ikinciSeferSecimi == 0)
            {
                yolculuk.Saat = label17.Text;
                yolculuk.Ucret = label26.Text;
                //ikinciSeferSecimi = 1;
            }
            else
            {
                donus.Saat = label17.Text;
                donus.Ucret = label26.Text;
            }
            showPart3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 18 25
            if (ikinciSeferSecimi == 0)
            {
                yolculuk.Saat = label18.Text;
                yolculuk.Ucret = label25.Text;
                //ikinciSeferSecimi = 1;
            }
            else
            {
                donus.Saat = label18.Text;
                donus.Ucret = label25.Text;
            }
            showPart3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 19 24
            if (ikinciSeferSecimi == 0)
            {
                yolculuk.Saat = label19.Text;
                yolculuk.Ucret = label24.Text;
                //ikinciSeferSecimi = 1;
            }
            else
            {
                donus.Saat = label19.Text;
                donus.Ucret = label24.Text;
            }
            showPart3();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // 20 23
            if (ikinciSeferSecimi == 0)
            {
                yolculuk.Saat = label20.Text;
                yolculuk.Ucret = label23.Text;
                //ikinciSeferSecimi = 1;
            }
            else
            {
                donus.Saat = label20.Text;
                donus.Ucret = label23.Text;
            }
            showPart3();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 21 22
            if (ikinciSeferSecimi == 0)
            {
                yolculuk.Saat = label21.Text;
                yolculuk.Ucret = label22.Text;
                //ikinciSeferSecimi = 1;
            }
            else 
            {
                donus.Saat = label21.Text;
                donus.Ucret = label22.Text;
            }

            showPart3();
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            panel7.BackColor = Color.FromArgb(168, 169, 180);
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = SystemColors.Control;
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            panel8.BackColor = Color.FromArgb(168, 169, 180);
        }

        private void panel8_MouseLeave(object sender, EventArgs e)
        {
            panel8.BackColor = SystemColors.Control;
        }

        private void panel9_MouseMove(object sender, MouseEventArgs e)
        {
            panel9.BackColor = Color.FromArgb(168, 169, 180);
        }

        private void panel9_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = SystemColors.Control;
        }

        private void panel10_MouseMove(object sender, MouseEventArgs e)
        {
            panel10.BackColor = Color.FromArgb(168, 169, 180);
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = SystemColors.Control;
        }

        private void panel11_MouseMove(object sender, MouseEventArgs e)
        {
            panel11.BackColor = Color.FromArgb(168, 169, 180);
        }

        private void panel11_MouseLeave(object sender, EventArgs e)
        {
            panel11.BackColor = SystemColors.Control;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "") 
            {
                MessageBox.Show("Verilen Alanlar Boş Geçilemez!");
                return;
            }

            yolcu = new Yolcu(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, numericUpDown1.Value);
            
            yolcu.Sefer_no = sefer_no;
            yolculuk.Sefer_no = sefer_no;

            if (donus != null)
            {
                yolcu2 = new Yolcu(yolcu.Isim, yolcu.Soyisim, yolcu.Tc_kimlik, yolcu.Eposta, numericUpDown2.Value);

                yolcu2.Sefer_no = sefer_no2;
                donus.Sefer_no = sefer_no2;
            }


            this.Hide();
            var form2 = new Form2(yolcu, yolculuk, donus, yolcu2);
            form2.Closed += (s, args) => this.Close();
            form2.Show();
            
        }
        Button last_button = null;
        string secili_koltuk_no = null;
        private void koltuk_sec(object sender, EventArgs e)
        {
            Button buton = (Button)sender;
            if (last_button == null) { }
            else if (ilk_koltuk.ToString() == last_button.Text) { last_button.BackColor = Color.Orange; }
            else { last_button.BackColor = Color.White; }
            
            last_button = buton;
            secili_koltuk_no = buton.Text;
            if (ikinci_koltuk_secimi == 0)
            {
                numericUpDown1.Value = Int32.Parse(secili_koltuk_no);
                buton.BackColor = Color.Orange;
            }

            else 
            { 
                numericUpDown2.Value = Int32.Parse(secili_koltuk_no);
                buton.BackColor = Color.Green;
            }
                
        }

        decimal ilk_koltuk = 0;
        decimal ikinci_koltuk = 0;
        decimal ikinci_koltuk_secimi = 0;
        private void button36_Click(object sender, EventArgs e)
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path_to_db + @";Integrated Security=True");

            string koltuk_check = "SELECT COUNT(*) FROM Yolcu WHERE (Koltuk_No = @koltuk AND Sefer_No = @sefer)";
            con.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand(koltuk_check, con))
                {
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = numericUpDown1.Value;
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = sefer_no;

                    int koltuk_dolu = (int)cmd.ExecuteScalar();

                    if (koltuk_dolu > 0)
                    {
                        MessageBox.Show("Gidiş yolculuğunda seçtiğiniz koltuk doludur!\nLütfen başka bir koltuk seçiniz.");
                        return;
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("İlk koltuk seçiminde SQL Exception Hatası!");
                return;
            }

            ilk_koltuk = numericUpDown1.Value;
            if (ikinciSeferSecimi == 1) { ikinci_koltuk_secimi = 1; }
            last_button.BackColor= Color.Orange;
            last_button = null;
            button36.BackColor = Color.Orange;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path_to_db + ";Integrated Security=True");

            string koltuk_check = "SELECT COUNT(*) FROM Yolcu WHERE (Koltuk_No = @koltuk AND Sefer_No = @sefer)";
            con.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand(koltuk_check, con))
                {
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = numericUpDown2.Value;
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = sefer_no2;

                    int koltuk_dolu2 = (int)cmd.ExecuteScalar();

                    if (koltuk_dolu2 > 0)
                    {
                        MessageBox.Show("Dönüş yolculuğunda seçtiğiniz koltuk doludur!\nLütfen başka bir koltuk seçiniz.");
                        return;
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("İkinci koltuk seçiminde SQL Exception Hatası!");
                return;
            }


            ikinci_koltuk = numericUpDown2.Value;
            button37.BackColor = Color.Green;
        }
        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            last_button.BackColor = Color.White;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            last_button.BackColor = Color.White;
        }

        Hashtable PlakaOlusturucu()
        {
            Hashtable plakalar = new Hashtable();
            plakalar.Add("ADANA", "01");
            plakalar.Add("ANKARA", "06");
            plakalar.Add("BURSA", "16");
            plakalar.Add("ÇANAKKALE", "17");
            plakalar.Add("DİYARBAKIR", "21");
            plakalar.Add("İSTANBUL", "34");
            plakalar.Add("İZMİR", "35");
            plakalar.Add("KOCAELİ", "41");
            plakalar.Add("MUŞ", "49");
            plakalar.Add("TRABZON", "61");
            plakalar.Add("TUNCELİ", "62");

            return plakalar;
        }
    }

    public class Yolcu : Object
    {
        private static Random random = new Random();

        public static string BiletPNR()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string isim;
        string soyisim;
        string tc_kimlik;
        string eposta;
        decimal koltuk_no;
        string bilet_no;
        string sefer_no;

        public Yolcu(string isim, string soyisim, string tc_kimlik, string eposta, decimal koltuk_no)
        {
            Isim = isim;
            Soyisim = soyisim;
            Tc_kimlik = tc_kimlik;
            Eposta = eposta;
            Koltuk_no = koltuk_no;
            Bilet_no = BiletPNR();
        }

        public string Isim { get => isim; set => isim = value; }
        public string Soyisim { get => soyisim; set => soyisim = value; }
        public string Tc_kimlik { get => tc_kimlik; set => tc_kimlik = value; }
        public string Eposta { get => eposta; set => eposta = value; }
        public string Bilet_no { get => bilet_no; set => bilet_no = value; }
        public decimal Koltuk_no { get => koltuk_no; set => koltuk_no = value; }
        public string Sefer_no { get => sefer_no; set => sefer_no = value; }
    }

    public class Yolculuk : Object
    {
        string nereden;
        string nereye;
        string gidis_tarihi;
        string saat = null;
        string ucret = null;
        string sefer_no = null;

        public Yolculuk(string nereden, string nereye, string gidis_tarihi)
        {
            Nereden = nereden;
            Nereye = nereye;
            Gidis_tarihi = gidis_tarihi.Substring(0, 10);
        }

        public string Nereden { get => nereden; set => nereden = value; }
        public string Nereye { get => nereye; set => nereye = value; }
        public string Gidis_tarihi { get => gidis_tarihi; set => gidis_tarihi = value; }
        public string Saat { get => saat; set => saat = value; }
        public string Ucret { get => ucret; set => ucret = value; }
        public string Sefer_no { get => sefer_no; set => sefer_no = value; }
    }



}
