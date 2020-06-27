using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ExampleTest
{
    public partial class Form1 : Form
    {
        DUANEntities db = new DUANEntities();
        public static int key;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string hash = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, textBox2.Text);

            }

            var result = (from u in db.DangNhaps
                          where u.username == textBox1.Text && u.password == hash
                          select u).ToList();
            var iduser = (from u in db.DangNhaps
                          where u.username == textBox1.Text && u.password == hash
                          select u.Id).ToList();

            
            if (result.Count() == 1)
            {
                //var student = (from s in db.DangNhaps
                //               where s.username == textBox1.Text
                //               select s).FirstOrDefault<DangNhap>();

                key = (from s in db.DangNhaps
                       where s.username == textBox1.Text
                       select s.Id).Single();

                //label1.Text = key.ToString();


                MDIParent2 sh = new MDIParent2();


                sh.Show();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form3 sh = new Form3();
            sh.Show();
        }
    }
}
