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

namespace HASH01
{
    public partial class Form1 : Form
    {
        // MD5 hash = MD5.Create();
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
    }
}
