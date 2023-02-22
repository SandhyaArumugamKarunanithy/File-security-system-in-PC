using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Diagnostics.Process.Start();

namespace filesec
{
    public partial class Form2 : Form
    {
        public string folder;
        public string f,fname;
        public Form2(string fold)
        {
            InitializeComponent();
            folder = fold;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static FileStream Open(string path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access, FileShare.ReadWrite);
        }
  
        private void EncryptFile(string inputFile)
        {

            try
            {
                int i = 0;
                int[] n = new int[1000];
                string password = @"pvshi159"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = textBox1.Text;
                fname = Path.GetFileName(cryptFile);
                f = folder + "\\" + fname;

                FileStream fsIn = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                 {
                      n[i] = data;
                      Debug.WriteLine(n[i]);
                      i++;
                  }
                Debug.WriteLine(i);
                fsIn.Close();

                File.Delete(inputFile);

                FileStream fsCrypt = new FileStream(f, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);


                    for (int j = 0; j < i; j++)
                        cs.WriteByte((byte)n[j]);


                cs.Close();
                fsCrypt.Close();
                MessageBox.Show("Successfully encrypted!", "Success");


            }

            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            EncryptFile(user);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f2 = new Form3();
            f2.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
