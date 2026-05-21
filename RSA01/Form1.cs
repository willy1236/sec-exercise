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

namespace RSA01
{
    public partial class Form1 : Form
    {
        string pub_xml, pvt_xml;
        RSAParameters pub_parameter, pvt_parameter;
        string path = @".\key";

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(path + @"\pub.xml");
            pub_xml = "";
            while(!sr.EndOfStream)
            {
                pub_xml += sr.ReadLine();
            }
            sr.Close();

            sr = new StreamReader(path + @"\pvt.xml");
            pvt_xml = "";
            while (!sr.EndOfStream)
            {
                pvt_xml += sr.ReadLine();
            }
            sr.Close();

            PubKey.Text = pub_xml;
            PvtKey.Text = pvt_xml;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CryText.Text = EncryptRSA(PlainText.Text, pub_xml);
        }

        public static string EncryptRSA(string original, string xmlString)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlString);
                byte[] s = Encoding.UTF8.GetBytes(original);
                return BitConverter.ToString(rsa.Encrypt(s, false)).Replace("-", string.Empty);
            }
            catch
            {
                return original;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DecodeText.Text = DecryptRSA(CryText.Text, pvt_xml);
        }

        public static string DecryptRSA(string hexstring, string xmlString)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlString);
                byte[] s = new byte[hexstring.Length / 2];
                for (int i = 0; i < hexstring.Length; i += 2)
                {
                    s[i / 2] = Byte.Parse(hexstring[i].ToString() + hexstring[i + 1].ToString(),
                        System.Globalization.NumberStyles.HexNumber);
                }
                return Encoding.UTF8.GetString(rsa.Decrypt(s, false));
            }
            catch {
                return hexstring;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            pub_xml = rsa.ToXmlString(false);
            pvt_xml = rsa.ToXmlString(true);
            pub_parameter = rsa.ExportParameters(false);
            pvt_parameter = rsa.ExportParameters(true);
            PubKey.Text = pub_xml;
            PvtKey.Text = pvt_xml;

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                MessageBox.Show("子目錄建立成功，時間：" + Directory.GetCreationTime(path).ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("子目錄建立失敗：" + ex.ToString());
            }

            StreamWriter sw = new StreamWriter(path + @"\pub.xml");
            sw.WriteLine(pub_xml);
            sw.Close();

            sw = new StreamWriter(path + @"\pvt.xml");
            sw.WriteLine(pvt_xml);
            sw.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
