using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace client1
{
    public partial class Form1 : Form
    {
        private const int port = 15000;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string server_ip = textBox1.Text.Trim();
            string message = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(server_ip))
            {
                MessageBox.Show("請輸入 Server IP");
                return;
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("請輸入訊息");
                return;
            }

            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(server_ip, port);
                    AppendLog("已連線到 Server。");

                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(message);

                        stream.Write(data, 0, data.Length);
                        AppendLog("已送出： " + message);

                        byte[] buffer = new byte[1024];
                        int byte_read = stream.Read(buffer, 0, buffer.Length);

                        string response = Encoding.UTF8.GetString(buffer, 0, byte_read);
                        AppendLog("收到回覆： " + response);
                    }
                }
            }
            catch (Exception exc) { 
                AppendLog("連線失敗： " + exc.Message);
            }
        }
        private void AppendLog(string message)
        {
            textBox3.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
    }
}
