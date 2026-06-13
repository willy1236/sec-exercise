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
        static string key_path = @".\secret.key";
        string secret_key;

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
                            Id Integer Primary Key Autoincrement,
                            Name TEXT NOT NULL,
                            Password TEXT NOT NULL,
                            SecretKey TEXT NOT NULL);
                            ";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }

            secret_key = GetSecretKey();
        }

        public static string GenerateSHA256Secret()
        {
            return Convert.ToBase64String(KeyGeneration.GenerateRandomKey(32));
        }

        public static string GenerateBase32Secret()
        {
            return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
        }
        
        public static string GetSecretKey()
        {
            if (File.Exists(key_path))
            {
                return File.ReadAllText(key_path).Trim();
            }
            
            string newKey = GenerateSHA256Secret();
            File.WriteAllText(key_path, newKey);
            return newKey;
        }

        public static string ComputeHmacSha256(string message, string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }

            if (left.Length != right.Length)
            {
                return false;
            }

            int accum = 0;
            for (int i = 0; i < left.Length; i++)
            {
                accum |= left[i] ^ right[i];
            }

            return accum == 0;
        }
        public static bool VerifyHmac(string hash1, string hash2)
        {
            byte[] bytes1 = Convert.FromBase64String(hash1);
            byte[] bytes2 = Convert.FromBase64String(hash2);
            return FixedTimeEquals(bytes1, bytes2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("請填入帳號");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("請填入密碼");
                return;
            }

            string _base32Secret = GenerateBase32Secret();
            string AccountName = textBox1.Text;
            using (DataTable dt = new DataTable())
            using (conn = new SQLiteConnection(connection_str))
            {
                conn.Open();
                
                sql = @"Select Id, Name, Password, SecretKey From Users Where Name = @Name;";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("此帳號已被註冊");
                    return;
                }

                sql = @"Insert into Users(Name, Password, SecretKey)
                    Values(@Name, @Password, @SecretKey);
                    ";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", ComputeHmacSha256(textBox2.Text, secret_key));
                cmd.Parameters.AddWithValue("@SecretKey", _base32Secret);
                cmd.ExecuteNonQuery();
            }
            
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
            lblResult.Text = "註冊完成，請使用 Google Authenticator 掃描 QR Code。";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("請填入帳號");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("請填入密碼");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("請填入驗證碼");
                return;
            }

            DataTable dt = new DataTable();
            using (conn = new SQLiteConnection(connection_str))
            {
                sql = @"Select Id, Name, Password, SecretKey From Users Where Name = @Name;";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            if (dt.Rows.Count != 1) {
                lblResult.Text = "帳號或密碼不正確";
                return;
            }

            string pw_input = ComputeHmacSha256(textBox2.Text, secret_key);
            string pw_db = dt.Rows[0]["Password"].ToString();
            if (!VerifyHmac(pw_input, pw_db))
            {
                lblResult.Text = "帳號或密碼不正確";
                return;
            }

            string _base32Secret = dt.Rows[0]["SecretKey"].ToString();
            string userCode = txtCode.Text.Trim();
            var totp = new Totp(Base32Encoding.ToBytes(_base32Secret));
            bool isValid = totp.VerifyTotp(
                userCode,
                out long timeStepMatched,
                new VerificationWindow(previous: 1, future: 1)
            );
            if (isValid)
            {
                lblResult.Text = "驗證成功！";
            }
            else
            {
                lblResult.Text = "驗證失敗，請確認驗證碼或系統時間。";
            }
        }
    }
}
