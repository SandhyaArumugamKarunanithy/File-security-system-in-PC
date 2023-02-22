using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing.QrCode;
using System.Net.NetworkInformation;
using MySql.Data.MySqlClient;

namespace filesec
{
    public partial class Form0 : Form
    {
        public string user = null;
        private string conn;
        public static System.Timers.Timer timer;
        private MySqlConnection connect;
        public Form0()
        {
            InitializeComponent();
            Shown += Form0_Shown;

        }
        private void Form0_Shown(object sender, EventArgs e)
        {
            //time();

            QRCodeWriter qr = new QRCodeWriter();
            string url = GetMACAddress();
            var matrix = qr.encode(url, ZXing.BarcodeFormat.QR_CODE, 200, 200);

            ZXing.BarcodeWriter w = new ZXing.BarcodeWriter();

            w.Format = ZXing.BarcodeFormat.QR_CODE;

            Bitmap img = w.Write(matrix);
            pictureBox1.Image = img;
            time();
            Console.ReadLine();
        }
  public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        private void db_connection()
        {
            try
            {
                conn = "Server=localhost;Database=fs;Uid=root;Pwd=;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("start xampp");
                throw;
            }
        }
        private bool validate_login()
        {
            
            string MAC = GetMACAddress();
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select username,ack from reg where MAC=@MAC and ack=1";
            cmd.Parameters.AddWithValue("@MAC", MAC);
            cmd.Parameters.AddWithValue("@username", user);

            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                user= (login["username"].ToString());
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }
        public void time()
        {
            bool r = validate_login();
            if (r)
            {
                MessageBox.Show("Ack received...");
                string MAC = GetMACAddress();
                db_connection();
                MySqlCommand sqlComm = new MySqlCommand();
                using (MySqlConnection cn = new MySqlConnection(connect.ConnectionString))
                {
                    MySqlCommand c = new MySqlCommand();
                    c.Connection = cn;
                    c.CommandText = "UPDATE reg SET ack=0 WHERE MAC=@MAC";
                    c.Parameters.AddWithValue("@MAC", MAC);
                    cn.Open();
                    int numRowsUpdated = c.ExecuteNonQuery();
                    c.Dispose();
                }
                this.Hide();
                Form1 f2 = new Form1(user);
                f2.Show();
            }
            else
            {
                MessageBox.Show("waiting for ack...");
                this.Hide();
                Form0 f2 = new Form0();
                f2.Show();
               // Refresh();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }


