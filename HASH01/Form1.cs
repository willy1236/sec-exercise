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
using System.IO;

namespace HASH01
{
    public partial class Form1 : Form
    {
        // MD5 hash = MD5.Create();
        DES des_encodeing = DES.Create();
        SHA256 hash = SHA256.Create();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = Convert.ToBase64String(
                                hash.ComputeHash(
                                    Encoding.Default.GetBytes(textBox1.Text)
                                )
                          );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            des_encodeing.Key = UTF8Encoding.UTF8.GetBytes(textBox2.Text);
            des_encodeing.IV = UTF8Encoding.UTF8.GetBytes(textBox5.Text);
            des_encodeing.Mode = CipherMode.CBC;
            des_encodeing.Padding = PaddingMode.PKCS7;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des_encodeing.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] plain_text_byte = UTF8Encoding.UTF8.GetBytes(textBox3.Text);
            cs.Write(plain_text_byte, 0, plain_text_byte.Length);
            cs.FlushFinalBlock();
            textBox4.Text = Convert.ToBase64String(ms.ToArray()); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            des_encodeing.Key = UTF8Encoding.UTF8.GetBytes(textBox2.Text);
            des_encodeing.IV = UTF8Encoding.UTF8.GetBytes(textBox5.Text);
            des_encodeing.Mode = CipherMode.CBC;
            des_encodeing.Padding = PaddingMode.PKCS7;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des_encodeing.CreateDecryptor(), CryptoStreamMode.Write);
            byte[] plain_text_byte = Convert.FromBase64String(textBox4.Text);
            cs.Write(plain_text_byte, 0, plain_text_byte.Length);
            cs.FlushFinalBlock();
            textBox6.Text = Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
