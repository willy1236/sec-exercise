using OtpNet;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace term_project
{
    public partial class Form1 : Form
    {
        string pub_xml, pvt_xml;
        RSAParameters pub_parameter, pvt_parameter;
        string key_dir = @".\key";

        string db_path = "User.db";
        string connection_str;
        SQLiteConnection conn;
        string sql;
        SQLiteCommand cmd;

        private const string Issuer = "GaOTPTest";
        public Form1()
        {
            InitializeComponent();
            connection_str = $"Data Source={db_path};" + "Version=3;";
            if (!File.Exists(db_path))
            {
                SQLiteConnection.CreateFile(db_path);
                using (conn = new SQLiteConnection(connection_str))
                {
                    conn.Open();

                    sql = @"CREATE TABLE IF NOT EXISTS Users(
                            Id TEXT Primary Key,
                            Name TEXT NOT NULL,
                            Password TEXT NOT NULL,
                            SecreKey TEXT NULL);
                            ";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            Load_key();
        }

        public void Load_key()
        {
            if (File.Exists(key_dir + @"\pvt.xml"))
            {
                StreamReader sr = new StreamReader(key_dir + @"\pub.xml");
                pub_xml = "";
                while(!sr.EndOfStream)
                {
                    pub_xml += sr.ReadLine();
                }
                sr.Close();

                sr = new StreamReader(key_dir + @"\pvt.xml");
                pvt_xml = "";
                while (!sr.EndOfStream)
                {
                    pvt_xml += sr.ReadLine();
                }
                sr.Close();
            }
            else
            {
                Create_key();
            }
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
            catch
            {
                return hexstring;
            }
        }


        public string Generate_secret()
        {
            return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
        }
        public void Create_key()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            pub_xml = rsa.ToXmlString(false);
            pvt_xml = rsa.ToXmlString(true);
            pub_parameter = rsa.ExportParameters(false);
            pvt_parameter = rsa.ExportParameters(true);

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(key_dir);
                MessageBox.Show("密鑰子目錄建立成功，時間：" + Directory.GetCreationTime(key_dir).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("密鑰子目錄建立失敗：" + ex.ToString());
            }

            StreamWriter sw = new StreamWriter(key_dir + @"\pub.xml");
            sw.WriteLine(pub_xml);
            sw.Close();

            sw = new StreamWriter(key_dir + @"\pvt.xml");
            sw.WriteLine(pvt_xml);
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (conn = new SQLiteConnection(connection_str))
            {
                conn.Open();
                sql = @"Insert into Users(Id, Name, Password, SecreKey)
                    Values(@Id, @Name, @Password, @SecreKey);
                    ";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox3.Text);
                cmd.Parameters.AddWithValue("@Password", EncryptRSA(textBox2.Text, pub_xml));
                cmd.Parameters.AddWithValue("@SecreKey", Generate_secret());
                cmd.ExecuteNonQuery();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (conn = new SQLiteConnection(connection_str))
            {
                sql = @"Select Id, Name, Password, SecreKey From Users Where Id = @Id Order by ID";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", textBox1.Text);
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            if (dt.Rows.Count != 1) {
                MessageBox.Show("帳號或密碼不正確");
                return;
            }

            // TODO: 非正確的比對實作方式
            string pw_input = textBox2.Text;
            string pw_db = DecryptRSA(dt.Rows[0]["Password"].ToString(), pvt_xml);
            if (pw_db != pw_input)
            {
                MessageBox.Show("帳號或密碼不正確");
                return;
            }
            
            // dataGridView1.DataSource = dt;
            string _base32Secret = dt.Rows[0]["SecreKey"].ToString();
            string AccountName = dt.Rows[0]["Id"].ToString();
            txtSecret.Text = _base32Secret;

            string otpAuthUri =
                $"otpauth://totp/{Uri.EscapeDataString(Issuer)}:{Uri.EscapeDataString(AccountName)}" +
                $"?secret={_base32Secret}" +
                $"&issuer={Uri.EscapeDataString(Issuer)}" +
                $"&algorithm=SHA1" +
                $"&digits=6" +
                $"&period=30";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(otpAuthUri,
           QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrBitmap = qrCode.GetGraphic(8);
                picQr.Image = qrBitmap;
            }
            lblResult.Text = "請使用 Google Authenticator 掃描 QR Code。";


        }
    }
}
