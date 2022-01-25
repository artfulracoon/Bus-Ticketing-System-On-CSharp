using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Xml;


namespace Otobus_Biletleme_Sistemi
{
    public partial class Form3 : Form
    {
        System.Drawing.Point point = new System.Drawing.Point(521, 34);
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"Database1.mdf") + @";Integrated Security=True");
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Location != point) 
            {
                pictureBox1.Location = point;
                pictureBox1.Size = new System.Drawing.Size(150, 150);
            }
            
            textBox1.Visible = true;
            button4.Visible = true;
            label4.Visible = true;

            label2.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button5.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Location != point)
            {
                pictureBox1.Location = point;
                pictureBox1.Size = new System.Drawing.Size(150, 150);
            }

            panel2.Visible = false;

            textBox1.Visible = false;
            button4.Visible = false;
            label4.Visible = false;

            label2.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button5.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string bilet_no = textBox1.Text;
            string sefer_no;
            con.Open();
            string bilet_no_sorgu = "SELECT Sefer_No FROM Yolcu WHERE Bilet_No = @bilet_no";
            try
            {
                using (SqlCommand cmd = new SqlCommand(bilet_no_sorgu, con))
                {
                    cmd.Parameters.Add("@bilet_no", SqlDbType.NVarChar).Value = bilet_no;
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    sefer_no = reader.GetString(0);

                    reader.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Aradığınız PNR kayıtlarda bulunamamıştır!");
                con.Close();
                return;
            }

            try 
            {
                string yolculuk_veri_sorgu = "SELECT * FROM Sefer WHERE Sefer_No = @sefer_no";
                
                using (SqlCommand cmd = new SqlCommand(yolculuk_veri_sorgu, con))
                {
                    cmd.Parameters.Add("@sefer_no", SqlDbType.NVarChar).Value = sefer_no;

                    SqlDataReader reader = cmd.ExecuteReader();

                    string[] columns = new string[6];
                    string[] data = new string[6];

                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns[i] = reader.GetName(i).Replace(" ", "").ToUpper();
                            data[i] = reader.GetString(i).Replace(" ", "");
                        }
                    }

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string format = "{0,-55} {1,-25}\n";
                    sb.AppendFormat(format, "", "");
                    for (int ctr = 0; ctr < columns.Length; ctr++)
                        sb.AppendFormat(format, columns[ctr], data[ctr]);

                    reader.Close();

                    var result = MessageBox.Show(sb.ToString(),"Bilet üzerinde değişiklik yapmak istiyor musunuz?",MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                        {
                        using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM Yolcu WHERE Sefer_No = @sefer_no AND Bilet_No = @bilet", con))
                        {
                            cmd2.Parameters.Add("@sefer_no", SqlDbType.NVarChar).Value = sefer_no;
                            cmd2.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = bilet_no;
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            string[] data2 = new string[7];

                            while (reader2.Read())
                            {

                                for (int i = 0; i < reader2.FieldCount; i++)
                                {
                                    data2[i] = reader2.GetString(i).Replace(" ", "");
                                }
                            }

                            label4.Visible = false; 
                            textBox1.Visible = false;
                            panel2.Visible = true;

                            textBox4.Text = data2[0];
                            textBox5.Text = data2[1];
                            textBox6.Text = data2[2];
                            textBox7.Text = data2[3];
                            textBox8.Text = data2[4];
                            label11.Text = data2[5];
                            label13.Text = data2[6];

                            reader2.Close();
                        }
                    }
  
                    con.Close();
                }
                    

            }
            catch (SqlException)
            {
                MessageBox.Show("Aradığınız PNR kayıtlarda bulunamamıştır!");
                con.Close();
                return;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string username = textBox2.Text;
            string password = textBox3.Text;

            XmlDocument doc = new XmlDocument();
            doc.Load((Application.StartupPath.Substring(0,Application.StartupPath.Length - 9) + @"admins.xml"));

            foreach (XmlNode node in doc.SelectNodes("/admin/users")) 
            {

                if (node.SelectSingleNode("username").InnerText.Equals(username)) {
                    if (node.SelectSingleNode("password").InnerText.Equals(password))
                    {

                        this.Hide();
                        var form4 = new Form4();
                        form4.Closed += (s, args) => this.Close();
                        form4.Show();
                        return;

                    }
                }
            }
            MessageBox.Show("Girdiğiniz kullanıcı adı ve şifre ile eşleşen bir kayıt yoktur!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd;

     
            cmd = new SqlCommand("UPDATE Yolcu SET Isim=@isim,Soyisim=@soyisim,TC_Kimlik=@tc,Eposta=@eposta,Koltuk_No=@koltuk,Sefer_No=@sefer WHERE Bilet_No=@bilet", con);

            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@isim", SqlDbType.NVarChar).Value = textBox4.Text.Replace(" ", "");
                    cmd.Parameters.Add("@soyisim", SqlDbType.NVarChar).Value = textBox5.Text.Replace(" ", "");
                    cmd.Parameters.Add("@tc", SqlDbType.NVarChar).Value = textBox6.Text.Replace(" ", "");
                    cmd.Parameters.Add("@eposta", SqlDbType.NVarChar).Value = textBox7.Text.Replace(" ", "");
                    cmd.Parameters.Add("@koltuk", SqlDbType.NVarChar).Value = textBox8.Text.Replace(" ", "");
                    cmd.Parameters.Add("@sefer", SqlDbType.NVarChar).Value = label11.Text.Replace(" ", "");
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = label13.Text.Replace(" ", "");

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu verileri başarıyla değiştirildi!");
                    con.Close();

                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Değiştirilme sırasında hata oluştu! Aynı yolcuyu eklemeye çalışıyor olabilirsiniz veya seçili seferdeki koltuk numarası dolu olabilir.");
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE Yolcu WHERE Bilet_No=@bilet", con);
            try
            {
                using (cmd)
                {
                    cmd.Parameters.Add("@bilet", SqlDbType.NVarChar).Value = textBox1.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yolcu verileri başarıyla silindi!");
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

