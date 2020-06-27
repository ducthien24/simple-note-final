using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ExampleTest
{
    public partial class Form3 : Form
    {
        //int a = 1;
        DUANEntities context = new DUANEntities();
        public Form3()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var checkusername = (from u in context.DangNhaps
                          where u.username == textBox1.Text
                          select u).ToList();
            if(checkusername.Count() > 0)
            {
                MessageBox.Show("Ton tai");
            }
            else
            {
                string hash = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    hash = GetMd5Hash(md5Hash, textBox2.Text);

                }

                var result = (from u in context.DangNhaps
                              select u).ToList();
                var a = result.Count();
                if (a == 0)
                {
                    var std = new DangNhap()
                    {
                        Id = 0,
                        username = textBox1.Text,
                        password = hash,
                    };
                    var st = new NoiDung()
                    {
                        Id = 0,
                        namefile = "",
                        thoigian = "",
                        userId = std.Id
                    };
                    context.DangNhaps.Add(std);

                    context.SaveChanges();
                }
                else
                {
                    var std = new DangNhap()
                    {
                        Id = a++,
                        username = textBox1.Text,
                        password = hash,
                    };
                    context.DangNhaps.Add(std);

                    context.SaveChanges();
                }

                MDIParent2 h = new MDIParent2();
                h.Show();
            }
            //using (var context = new DUANEntities())
            //{
            //    string hash = "";
            //    using (MD5 md5Hash = MD5.Create())
            //    {
            //        hash = GetMd5Hash(md5Hash, textBox2.Text);

            //    }

            //    var result = (from u in context.DangNhaps
            //                  select u).ToList();
            //    var a = result.Count();
            //    if( a == 0)
            //    {
            //        var std = new DangNhap()
            //        {
            //            Id = 0,
            //            username = textBox1.Text,
            //            password = hash,
            //        };
            //        context.DangNhaps.Add(std);

            //        context.SaveChanges();
            //    }
            //    else
            //    {
            //        var std = new DangNhap()
            //        {
            //            Id = a++,
            //            username = textBox1.Text,
            //            password = hash,
            //        };
            //        context.DangNhaps.Add(std);

            //        context.SaveChanges();
            //    }

            //    MessageBox.Show("Success");

            //a++;
            

        
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
    }
}
