using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Otobus_Biletleme_Sistemi
{
    public partial class Form2 : Form
    {
        Yolcu yolcu;
        Yolcu yolcu2;
        Yolculuk yolculuk;
        Yolculuk donus;
        string path_to_db = (Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"Database1.mdf");
        public Form2(Yolcu yolcu, Yolculuk yolculuk, Yolculuk donus, Yolcu yolcu2)
        {
            InitializeComponent();

            this.yolcu = yolcu;
            this.yolculuk = yolculuk;
            this.donus = donus;
            this.yolcu2 = yolcu2;

            label2.Text = "İsim: " + yolcu.Isim;
            label3.Text = "Soyisim: " + yolcu.Soyisim;
            label4.Text = "TC Kimlik: " + yolcu.Tc_kimlik;
            label22.Text = "E-posta: " + yolcu.Eposta;
            
            label6.Text = "Sefer No: " + yolcu.Sefer_no;
            label7.Text = "Bilet No: " + yolcu.Bilet_no;

            label9.Text = "Nereden: " + yolculuk.Nereden;
            label10.Text = "Nereye: " + yolculuk.Nereye;
            label11.Text = "Tarih: " + yolculuk.Gidis_tarihi;
            label12.Text = "Saat: " + yolculuk.Saat;
            label13.Text = "Ücret: " + yolculuk.Ucret;
            label14.Text = "Sefer No: " + yolculuk.Sefer_no;
            label5.Text = "Koltuk No: " + yolcu.Koltuk_no.ToString();

            if (donus != null) 
            {
                label16.Text = "Nereden: " + donus.Nereden;
                label17.Text = "Nereye: " + donus.Nereye;
                label18.Text = "Tarih: " + donus.Gidis_tarihi;
                label19.Text = "Saat: " + donus.Saat;
                label20.Text = "Ücret: " + donus.Ucret;
                label21.Text = "Sefer No: " + donus.Sefer_no;

                label23.Text = "Koltuk No: " + yolcu2.Koltuk_no.ToString();
                label24.Text = "Bilet No: " + yolcu2.Bilet_no;

                label15.Show();
                label16.Show();
                label17.Show();
                label18.Show();
                label19.Show();
                label20.Show();
                label21.Show();
                label23.Show();
                label24.Show();
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            int check = 0;

            string yolcu_if_not_exists_insert = "IF NOT EXISTS (SELECT * FROM Yolcu WHERE Bilet_No = @bilet) BEGIN INSERT INTO Yolcu VALUES (@isim,@soyisim,@tc,@eposta,@koltuk,@sefer,@bilet) END";

            string sefer_if_not_exists_insert = "IF NOT EXISTS (SELECT * FROM Sefer WHERE Sefer_No = @sefer) BEGIN INSERT INTO Sefer VALUES (@nereden,@nereye,@tarih,@saat,@ucret,@sefer) END";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path_to_db + @";Integrated Security=True");
            
            con.Open();
            try {
                using (SqlCommand cmd = new SqlCommand(sefer_if_not_exists_insert, con))
                {
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = yolculuk.Sefer_no.Replace(" ", "");
                    cmd.Parameters.Add("@nereden", SqlDbType.NVarChar).Value = yolculuk.Nereden.Replace(" ", "");
                    cmd.Parameters.Add("@nereye", SqlDbType.NVarChar).Value = yolculuk.Nereye.Replace(" ", "");
                    cmd.Parameters.Add("@tarih", SqlDbType.NVarChar).Value = yolculuk.Gidis_tarihi.Replace(" ", "");
                    cmd.Parameters.Add("@saat", SqlDbType.NVarChar).Value = yolculuk.Saat.Replace(" ", "");
                    cmd.Parameters.Add("@ucret", SqlDbType.NVarChar).Value = yolculuk.Ucret.Replace(" ", "");

                    int rowsAdded = cmd.ExecuteNonQuery();
                }

                if (donus != null)
                {
                    using (SqlCommand cmd = new SqlCommand(sefer_if_not_exists_insert, con))
                    {
                        cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = donus.Sefer_no.Replace(" ", "");
                        cmd.Parameters.Add("@nereden", SqlDbType.NVarChar).Value = donus.Nereden.Replace(" ", "");
                        cmd.Parameters.Add("@nereye", SqlDbType.NVarChar).Value = donus.Nereye.Replace(" ", "");
                        cmd.Parameters.Add("@tarih", SqlDbType.NVarChar).Value = donus.Gidis_tarihi.Replace(" ", "");
                        cmd.Parameters.Add("@saat", SqlDbType.NVarChar).Value = donus.Saat.Replace(" ", "");
                        cmd.Parameters.Add("@ucret", SqlDbType.NVarChar).Value = donus.Ucret.Replace(" ", "");

                        int rowsAdded = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException) 
            {
                MessageBox.Show("Seferin veritabanına eklenmesinde hata oluştu!");
                return;
            }

            try {
                using (SqlCommand cmd = new SqlCommand(yolcu_if_not_exists_insert, con))
                {
                    cmd.Parameters.Add("@isim", SqlDbType.NVarChar).Value = yolcu.Isim.Replace(" ", "");
                    cmd.Parameters.Add("@soyisim", SqlDbType.NVarChar).Value = yolcu.Soyisim.Replace(" ", "");
                    cmd.Parameters.Add("@tc", SqlDbType.NVarChar).Value = yolcu.Tc_kimlik.Replace(" ", "");
                    cmd.Parameters.Add("@eposta", SqlDbType.NVarChar).Value = yolcu.Eposta.Replace(" ", "");
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = yolcu.Koltuk_no.ToString().Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = yolcu.Sefer_no.Replace(" ", "");
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = yolcu.Bilet_no.Replace(" ", "");

                    int rowsAdded = cmd.ExecuteNonQuery();
                    if (rowsAdded <= 0)
                    {
                        MessageBox.Show("Yolcu işleme alınamadı!");
                        check = 1;
                    }

                }

                using (SqlCommand cmd = new SqlCommand(yolcu_if_not_exists_insert, con))
                {
                    if (yolcu2 != null)
                    {
                        cmd.Parameters.Add("@isim", SqlDbType.NVarChar).Value = yolcu2.Isim.Replace(" ", "");
                        cmd.Parameters.Add("@soyisim", SqlDbType.NVarChar).Value = yolcu2.Soyisim.Replace(" ", "");
                        cmd.Parameters.Add("@tc", SqlDbType.NVarChar).Value = yolcu2.Tc_kimlik.Replace(" ", "");
                        cmd.Parameters.Add("@eposta", SqlDbType.NVarChar).Value = yolcu2.Eposta.Replace(" ", "");
                        cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = yolcu2.Koltuk_no.ToString().Replace(" ", "");
                        cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = yolcu2.Sefer_no.Replace(" ", "");
                        cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = yolcu2.Bilet_no.Replace(" ", "");

                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded <= 0)
                        {
                            MessageBox.Show("Yolcu 2 işleme alınamadı!");
                            check = 1;
                        }
                    }
                }
            } catch (SqlException)
            {
                MessageBox.Show("Seçtiğiniz koltuk halihazırda ayırtılmış durumdadır!\nLütfen farklı bir koltuk seçiniz.");
                return;
            }


            if (check == 0)
                {
                    this.Hide();
                    MessageBox.Show("Biletiniz işlenmiştir! İyi günler dileriz.");
                    this.Close();
                }
                else
                {
                    this.Hide();
                    MessageBox.Show("Biletiniz bir hata kaynaklı işleme alınmamıştır. Lütfen tekrar deneyiniz.");
                    this.Close();
                }

            
        }
    }
}
