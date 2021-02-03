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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // * * * * * * * * * * * * * * * DEVOLOPED BY caner24 * * * * * * * * * * * * * * * //


        // İlgili databese bağlantı işlemleri

        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\cnr24\\source\\repos\\HastaneKayit\\database\\Database5.mdb");


        // Database bilgiçekme işlemlerini yapması için yordam oluşturup komut verme işlemleri

        // Database bilgiçekme işleminde randevulu sistem olduğu için doktorun adının seçili olduğu hastalarda ve güncel tarihe göre  randevuların sıralanması işlemi.

        private void HastaBilgiGetir()
        {
            listView1.Items.Clear();
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("Select * from hastabilgi where Doktor='" + "Prof.Dr Caner" + "' and RandevuTarih='"+ label2.Text + "' ", baglantim);
            OleDbDataReader oku = kom.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["HastaAd"].ToString() + " " + oku["HastaSoyAd"].ToString();
                ekle.SubItems.Add(oku["RandevuSaat"].ToString());

                listView1.Items.Add(ekle);
            }
            baglantim.Close();
        }

        // Database bilgiçekme işlemlerini yapması için yordam oluşturup komut verme işlemleri

        // Database bilgiçekme işleminde randevu saati gelen hastanının labellara yazarak hasta sırasının geldiğinin belirtilmesi

        private void HastaBilgiGetir2()
        {
            baglantim.Open();
            OleDbCommand kom = new OleDbCommand("Select * from hastabilgi where RandevuSaat='" + label1.Text + "' and RandevuTarih='" + dateTimePicker1.Text + "' and Doktor='" + "Prof.Dr Caner" + "'  ", baglantim);
            OleDbDataReader oku = kom.ExecuteReader();

            while (oku.Read())
            {
                label4.Text = oku["HastaAd"].ToString() + " " + oku["HastaSoyAd"].ToString();
                label5.Text = oku["RandevuSaat"].ToString();
            }
            baglantim.Close();
        }

        // Database ile sürekli olarak bilgileri refreshe edip forma aktarması için yordamlarımızı timer ile çağırma işleleri

        private void timer1_Tick(object sender, EventArgs e)
        {
            HastaBilgiGetir();
            HastaBilgiGetir2();
            label1.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            label18.Text = label18.Text.Substring(1) + label18.Text.Substring(0, 1);
        }

        // Timerimizin yordamları sürekli yenilemesi için form yüklenirken yani load ' da aktif olması lazım

        // Tarihi ise güncel olan günü baz alarak gösterdiği için datetime picker koyuldu.Ve datetimepicker ' ın görünmemesi için label'a yazdırılıp visable ' i false yapıldı.

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            label2.Text = dateTimePicker1.Text;
            dateTimePicker1.Visible = false;
        }
    }
}
