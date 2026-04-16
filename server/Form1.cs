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
using System.Threading;
namespace server
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private Thread server_thread;
        private bool is_running;
        private const int port = 15000;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (is_running)
            {
                AppendLog("Server 已經啟動");
                return;
            }
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                is_running = true;

                AppendLog($"Server 啟動成功，Port = {port}");
                server_thread = new Thread(StartServer);
                server_thread.IsBackground = true;
                server_thread.Start();
            }
            catch (Exception exc)
            {
                AppendLog($"Server 啟動失敗 {exc.Message}");
            }
        }
        void AppendLog(String message)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() =>
                {
                    textBox1.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
                }));
            }
            else
            {
                textBox1.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            }
        }
        void StartServer()
        {
            while (is_running)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    AppendLog("有 Client 連線進來");

                    Thread client_thread = new Thread(() => HandleClient(client));
                    client_thread.IsBackground = true;
                    client_thread.Start();
                }
                catch (Exception exc)
                {
                    if (is_running)
                    {
                        AppendLog("Server 錯誤：" + exc.Message);
                    }
                }
            }
        }

        void HandleClient(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int byte_read = stream.Read(buffer, 0, buffer.Length);

                    string message = Encoding.UTF8.GetString(buffer, 0, byte_read);
                    AppendLog("收到： " + message);

                    string response = "Server 回覆： 已收到 [" + message + "]";
                    byte[] response_byte = Encoding.UTF8.GetBytes(response);


                    stream.Write(response_byte, 0, response_byte.Length);
                    AppendLog("已回覆 Client，");
                }
            }
            catch(Exception exc)
            {
                AppendLog("Client 處理錯誤：" + exc.Message);
            }
        }
    }
}
