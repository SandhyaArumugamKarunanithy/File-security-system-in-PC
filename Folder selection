using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using System.IO;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace filesec
{
    public partial class Form5 : Form
    {
        private string conn;
        private MySqlConnection connect;
        FolderBrowserDialog fbd;
        //public string folder;
        string MAC;
        public Form5()
        {
            InitializeComponent();
            MAC = GetMACAddress();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }

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
        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0)
            {
                string folder = textBox1.Text;
                db_connection();
                string p = null;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select folder from reg where MAC=@MAC";
                cmd.Parameters.AddWithValue("@MAC", MAC);
                cmd.Connection = connect;
                MySqlDataReader login = cmd.ExecuteReader();
                if (login.Read())
                {
                    p = (login["folder"].ToString());
                    MessageBox.Show(p);
                    connect.Close();
                }
                else
                {
                    connect.Close();
                }

                if (p.Equals(""))
                {
                    p = fbd.SelectedPath;
                    DirectoryInfo di = new DirectoryInfo(fbd.SelectedPath);
                    if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        di.Attributes = FileAttributes.Hidden;
                    }
                    db_connection();
                    MySqlCommand sqlComm = new MySqlCommand();
                    using (MySqlConnection cn = new MySqlConnection(connect.ConnectionString))
                    {
                        MySqlCommand c = new MySqlCommand();
                        c.Connection = cn;
                        c.CommandText = "UPDATE reg SET folder=@folder WHERE MAC=@MAC";
                        c.Parameters.AddWithValue("@MAC", MAC);
                        c.Parameters.AddWithValue("@folder", folder);
                        cn.Open();
                        int numRowsUpdated = c.ExecuteNonQuery();
                        c.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("you already have a folder");
                }
                MessageBox.Show(p);

                string path = fbd.SelectedPath + "\\hide.bat";
                string msg = "attrib +h +s /s /d \"" + p + "\"";

                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    File.WriteAllText(path, msg);

                }
                else
                {
                    File.Delete(path);
                    File.Create(path).Close();
                    File.WriteAllText(path, msg);

                }


                Process pro = new Process();

                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.FileName = path;
                pro.StartInfo.CreateNoWindow = true;
                pro.Start();
                SHChangeNotify((uint)SHCNE.SHCNE_ASSOCCHANGED, (uint)SHCNF.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

                try
                {
                    string adminUserName = Environment.UserName;// getting your adminUserName
                    DirectorySecurity ds = Directory.GetAccessControl(p);
                    FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
                    ds.AddAccessRule(fsa);
                    Directory.SetAccessControl(p, ds);
                    MessageBox.Show("Locked");

                }

                catch
                {
                }

                this.Hide();
                Form2 f = new Form2(p);
                f.Show();


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path;
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select folder from reg where MAC=@MAC";
            cmd.Parameters.AddWithValue("@MAC", MAC);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                path = (login["folder"].ToString());
                connect.Close();
                this.Hide();
                Form2 f1 = new Form2(path);
                f1.Show();
            }
            else
            {
                connect.Close();
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

