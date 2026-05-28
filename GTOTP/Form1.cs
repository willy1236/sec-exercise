using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OtpNet;
using QRCoder;

namespace authenticator
{
    public partial class Form1 : Form
    {
        private byte[] _secretKey;
        private string _base32Secret;

        private const string Issuer = "GaOTPTest";
        private const string AccountName = "test@email.com";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. 產生 20 bytes Secret Key
            _secretKey = KeyGeneration.GenerateRandomKey(20);
            
            // 2. 轉成 Base32，Google Authenticator 使用此格式
            _base32Secret = Base32Encoding.ToString(_secretKey);
            txtSecret.Text = _base32Secret;
            
            // 3. 建立 otpauth URI
            string otpAuthUri =
                $"otpauth://totp/{Uri.EscapeDataString(Issuer)}:{ Uri.EscapeDataString(AccountName)}" +
                $"?secret={_base32Secret}" +
                $"&issuer={Uri.EscapeDataString(Issuer)}" +
                $"&algorithm=SHA1" +
                $"&digits=6" +
                $"&period=30";
            
            // 4. 產生 QR Code
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (_secretKey == null)
            {
                lblResult.Text = "請先產生 Secret Key 與 QR Code。";
                return;
            }
            string userCode = txtCode.Text.Trim();
            if (string.IsNullOrWhiteSpace(userCode))
            {
                lblResult.Text = "請輸入 Google Authenticator 顯示的 6 位數驗證碼。";
            return;
            }
            // 建立 TOTP 驗證器
            var totp = new Totp(_secretKey);
            // 容許前後各一個時間區間，避免手機與電腦時間略有誤差
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
