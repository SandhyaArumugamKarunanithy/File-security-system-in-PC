using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
//using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;


namespace filesec  
{
    public partial class Form1 : Form
    {

        private string conn;
        private MySqlConnection connect;
        public Form1(string user)
        {
            InitializeComponent();
            textBox2.Text = user;
            textBox2.Enabled = false;

        }

        private void db_connection()
        {
            //try
            {
                conn = "Server=localhost;Database=fs;Uid=root;Pwd=;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            /*catch (MySqlException e)
            {
                MessageBox.Show("start xampp");
                throw;
            }*/
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

        private bool validate_login(string pass)
        {
            string MAC = GetMACAddress();
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from reg where MAC=@MAC and passcode2=@pass";
            cmd.Parameters.AddWithValue("@MAC", MAC);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string pass= textBox1.Text;

            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(pass);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();



            if (pass == "")
            {
                MessageBox.Show("Empty Fields Detected ! Please fill up all the fields");
                return;
            }
            bool r = validate_login(output);
            if (r)
            {
                MessageBox.Show("Correct Login Credentials");
                this.Hide();
                Form3 f2 = new Form3();
                f2.Show();
            }
            else
                MessageBox.Show("Incorrect Login Credentials");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
