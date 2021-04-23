using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace HastaneKayit
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        // * * * * * * * * * * * * * * * DEVELOPED BY caner24 * * * * * * * * * * * * * * * //

        // İlgili databese bağlantı işlemleri

        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\cnr24\\source\\repos\\HastaneKayit\\database\\Database5.mdb");

        // Database bilgiçekme işlemlerini yapması için yordam oluşturup komut verme işlemleri

        private void HastaBilgileriGetir() 
        {
            listView2.Items.Clear();
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("Select *  from hastabilgi ", baglantim);
            OleDbDataReader oku = kom.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["Tc"].ToString();
                ekle.SubItems.Add(oku["HastaAd"].ToString());
                ekle.SubItems.Add(oku["HastaSoyAd"].ToString());
                ekle.SubItems.Add(oku["RandevuBölüm"].ToString());
                ekle.SubItems.Add(oku["Doktor"].ToString());
                ekle.SubItems.Add(oku["RandevuTarih"].ToString());
                ekle.SubItems.Add(oku["RandevuSaat"].ToString());

                listView2.Items.Add(ekle);
            }
            baglantim.Close();
        }

        // Formun load kısmında yani açılışında database bilgiçekme işlemlerini yapması için yordamın çağırılması.

        private void Form2_Load(object sender, EventArgs e)
        {
            HastaBilgileriGetir();
        }

        // Yeni bir hasta randevu kaydı oluşturulunca bu kayıdın databasemize kaydedilmesi işlemi.

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("INSERT INTO hastabilgi (Tc,HastaAd,HastaSoyAd,RandevuBölüm,Doktor,RandevuTarih,RandevuSaat)values(@tc,@hastadi,@hastasoyadi,@rndbölüm,@doktoru,@rndtarihi,@rndsaati) ", baglantim);
            kom.Parameters.AddWithValue("@tc", textBox1.Text);
            kom.Parameters.AddWithValue("@hastadi", textBox2.Text);
            kom.Parameters.AddWithValue("@hastasoyadi", textBox3.Text);
            kom.Parameters.AddWithValue("@rndbölüm", comboBox3.Text);
            kom.Parameters.AddWithValue("@rndbölüm", comboBox2.Text);
            kom.Parameters.AddWithValue("@rndtarihi", dateTimePicker1.Text);
            kom.Parameters.AddWithValue("@rndsaati", comboBox1.Text);
            kom.ExecuteNonQuery();
            baglantim.Close();
            HastaBilgileriGetir();

        }

        // Hasta bilgilerinin yeni eklenen bilgi varmı diye refresh edilmesi işlemleri.

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            HastaBilgileriGetir();
        }

        // Doktora göre hastaların sıralarını görmesi için oluşturulmuş panele yönlendirilmesi işlemi 

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Form1 canerhoca = new Form1();
            canerhoca.Show();
            this.Hide();

        }
        float tc;

        // Mouse click özelliği ile yaralandığımız hasta Tc kimlik numarasına göre hastanın bilgilerinin güncellenmesi.

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand komu = new OleDbCommand("Update hastabilgi set Tc='" + textBox1.Text.ToString() + "',HastaAd='" + textBox2.Text.ToString() + "',HastaSoyAd='" + textBox3.Text.ToString() + "',RandevuBölüm='" + comboBox3.Text.ToString() + "',Doktor='" + comboBox2.Text.ToString() + "',RandevuTarih='" + dateTimePicker1.Text.ToString() + "',RandevuSaat='" + comboBox1.Text.ToString() + "' where Tc ='" + label11.Text.ToString() +"'   ", baglantim);
            komu.ExecuteNonQuery();
            baglantim.Close();
            HastaBilgileriGetir();
        }

        // Hasta Tc kimlik numarasına göre hastanın randevu kayıtının silinmesi işlemi

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("Delete from hastabilgi where Tc=@tcsi",baglantim);
            kom.Parameters.AddWithValue("@tcsi", textBox5.Text);
            kom.ExecuteNonQuery();
            baglantim.Close();
            HastaBilgileriGetir();
        }

        // Hasta Tc kimlik numarasına göre hastanın bilgilerinin yer alması.

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("Select *  from hastabilgi where Tc like '%"+textBox4.Text+"' ",baglantim);
            OleDbDataReader oku = kom.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["Tc"].ToString();
                ekle.SubItems.Add(oku["HastaAd"].ToString());
                ekle.SubItems.Add(oku["HastaSoyAd"].ToString());

                listView1.Items.Add(ekle);
            }
            baglantim.Close();
        }

        // Mouse double click özelliğinden yararlanarak istelinen hastanın bilgilerine 2 kere basılınca textboxlarda yer alması işlemi.

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tc= float.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView2.SelectedItems[0].SubItems[0].Text;
            label11.Text = textBox1.Text;
            textBox5.Text = listView2.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView2.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView2.SelectedItems[0].SubItems[2].Text;
            comboBox3.Text = listView2.SelectedItems[0].SubItems[3].Text;
            comboBox2.Text = listView2.SelectedItems[0].SubItems[4].Text;
            dateTimePicker1.Value =Convert.ToDateTime(listView2.SelectedItems[0].SubItems[5].Text);
            comboBox1.Text = listView2.SelectedItems[0].SubItems[6].Text;
        }

        // Mouse double click özelliğinden yararlanarak istelinen hastanın bilgilerine 2 kere basılınca textboxlarda yer alması işlemi.

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {   tc = float.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        // Doktora göre hastaların sıralarını görmesi için oluşturulmuş panele yönlendirilmesi işlemi 

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Form3 ahmethoca = new Form3();
            ahmethoca.Show();
            this.Hide();
        }

        // Doktor olarak mevcut 2 doktor yazıldığı için bölümlerin sadece ilgili doktorlara yönlendirilmesi işlemi.

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text== "Kulak Burun Boğaz")
            {
                comboBox2.Text = "Prof.Dr Caner";
                comboBox2.Enabled = false;
            }
            if(comboBox3.Text == "Göz Hastalıkları")
            {
                comboBox2.Text = "Prof.Dr Ahmet";
                comboBox2.Enabled = false;
            }
        }
    }
}
