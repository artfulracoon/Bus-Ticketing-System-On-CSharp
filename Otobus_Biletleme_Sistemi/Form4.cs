using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Otobus_Biletleme_Sistemi
{

    public partial class Form4 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"Database1.mdf") + @";Integrated Security=True");
        SqlCommand cmd;

        public Form4()
        {
            InitializeComponent();
            tabPage1.Text = "YOLCU";
            tabPage2.Text = "SEFER";
            dataGridView1.DataSource = bindingSource1;
            dataGridView2.DataSource = bindingSource2;
            GetData("SELECT * FROM Yolcu", dataGridView1, bindingSource1);
            GetData("SELECT * FROM Sefer", dataGridView2, bindingSource2);
        }
        public void GetData(string selectCommand, DataGridView dgw, BindingSource bs)
        {
            try
            {
                
                dataAdapter = new SqlDataAdapter(selectCommand, @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"Database1.mdf") + @";Integrated Security=True");

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(table);
                bs.DataSource = table;

                dgw.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Veritabanı bağlantısında sorun çıktı.");
                
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(" ", "");
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Replace(" ", "");
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Replace(" ", "");   
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString().Replace(" ", "");
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Replace(" ", "");
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString().Replace(" ", "");
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString().Replace(" ", "");
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox9.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(" ", "");
            textBox10.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString().Replace(" ", "");
            textBox11.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString().Replace(" ", "");
            textBox12.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString().Replace(" ", "");
            textBox13.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString().Replace(" ", "");
            textBox14.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString().Replace(" ", "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("INSERT INTO Yolcu VALUES (@isim,@soyisim,@tc,@eposta,@koltuk,@sefer,@bilet)", con);

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@isim", SqlDbType.NVarChar).Value = textBox1.Text.Replace(" ", "");
                    cmd.Parameters.Add("@soyisim", SqlDbType.NVarChar).Value = textBox2.Text.Replace(" ", "");
                    cmd.Parameters.Add("@tc", SqlDbType.NVarChar).Value = textBox3.Text.Replace(" ", "");
                    cmd.Parameters.Add("@eposta", SqlDbType.NVarChar).Value = textBox4.Text.Replace(" ", "");
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = textBox5.Text.Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = textBox6.Text.Replace(" ", "");
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = textBox7.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Veritabanına başarıyla eklendi!");
                    GetData("SELECT * FROM Yolcu", dataGridView1, bindingSource1);
                    con.Close();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Eklemede hata oluştu! Aynı bilet numarası eklenmeye çalışıyor olabilir, lütfen kontrol ediniz.");
                con.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("UPDATE Yolcu SET Isim=@isim,Soyisim=@soyisim,TC_Kimlik=@tc,Eposta=@eposta,Koltuk_No=@koltuk,Sefer_No=@sefer WHERE Bilet_No=@bilet", con);
            
            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@isim", SqlDbType.NVarChar).Value = textBox1.Text.Replace(" ", "");
                    cmd.Parameters.Add("@soyisim", SqlDbType.NVarChar).Value = textBox2.Text.Replace(" ", "");
                    cmd.Parameters.Add("@tc", SqlDbType.NVarChar).Value = textBox3.Text.Replace(" ", "");
                    cmd.Parameters.Add("@eposta", SqlDbType.NVarChar).Value = textBox4.Text.Replace(" ", "");
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = textBox5.Text.Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = textBox6.Text.Replace(" ", "");
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = textBox7.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu verileri başarıyla değiştirildi!\n" +
                        "NOT:Bilet numarası değiştirelemez!");
                    GetData("SELECT * FROM Yolcu", dataGridView1, bindingSource1);
                    con.Close();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Değiştirilme sırasında hata oluştu!");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("DELETE Yolcu WHERE Bilet_No=@bilet", con);
            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = textBox7.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu verileri başarıyla silindi!");
                    GetData("SELECT * FROM Yolcu", dataGridView1, bindingSource1);
                    con.Close();

                }
            } catch (SqlException)
            {
                MessageBox.Show("Silinmede hata oluştu!");
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("INSERT INTO Sefer VALUES (@nereden,@nereye,@tarih,@saat,@ucret,@sefer)", con);

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@nereden", SqlDbType.NVarChar).Value = textBox9.Text.Replace(" ", "");
                    cmd.Parameters.Add("@nereye", SqlDbType.NVarChar).Value = textBox10.Text.Replace(" ", "");
                    cmd.Parameters.Add("@tarih", SqlDbType.NVarChar).Value = textBox12.Text.Replace(" ", "");
                    cmd.Parameters.Add("@saat", SqlDbType.NVarChar).Value = textBox12.Text.Replace(" ", "");
                    cmd.Parameters.Add("@ucret", SqlDbType.NVarChar).Value = textBox13.Text.Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = textBox14.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Veritabanına başarıyla eklendi!");
                    GetData("SELECT * FROM Sefer", dataGridView2, bindingSource2);
                    con.Close();
                }
            }
            catch (SqlException) 
            {
                MessageBox.Show("Eklemede hata oluştu! Aynı bilet numarası eklenmeye çalışıyor olabilir, lütfen kontrol ediniz.");
                con.Close();
            }




}

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("UPDATE Sefer SET Nereden=@nereden,Nereye=@nereye,Tarih=@tarih,Saat=@saat,Ucret=@ucret WHERE Sefer_No=@sefer", con);

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@nereden", SqlDbType.NVarChar).Value = textBox9.Text.Replace(" ", "");
                    cmd.Parameters.Add("@nereye", SqlDbType.NVarChar).Value = textBox10.Text.Replace(" ", "");
                    cmd.Parameters.Add("@tarih", SqlDbType.NVarChar).Value = textBox12.Text.Replace(" ", "");
                    cmd.Parameters.Add("@saat", SqlDbType.NVarChar).Value = textBox12.Text.Replace(" ", "");
                    cmd.Parameters.Add("@ucret", SqlDbType.NVarChar).Value = textBox13.Text.Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = textBox14.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sefer verileri başarıyla değiştirildi!\n" +
                        "NOT: Sefer numarası değiştirilemez!");
                    GetData("SELECT * FROM Sefer", dataGridView2, bindingSource2);
                    con.Close();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Değiştirilme sırasında hata oluştu!");
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("DELETE Sefer WHERE Sefer_No=@sefer", con);
            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = textBox14.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sefer verileri başarıyla silindi!");
                    GetData("SELECT * FROM Sefer", dataGridView2, bindingSource2);
                    con.Close();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Silinmede hata oluştu!");
                con.Close();
            }
        }
    }
}
