using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        IPEndPoint remoteEP = null;
        NetworkStream stream = null;
        TcpClient client = null;
        void Connect()
        {
            remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            try
            {
                client = new TcpClient();
                client.Connect(remoteEP);
                stream = client.GetStream();
                InformServer(tbName.Text);
                rtbReceivedMsg.Text += "Đã kết nối đến server\n";           
                Thread thread = new Thread(ListenMsg);
                thread.Start();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void InformServer(string name)
        {
            byte[] data = Encoding.UTF8.GetBytes(name);
            stream.Write(data, 0 , data.Length);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }
        void Send()
        {
            DateTime now = DateTime.Now;
            byte[] buffer = Encoding.ASCII.GetBytes(rtbSentMsg.Text);
            stream.Write(buffer, 0, buffer.Length);
            rtbReceivedMsg.Text += "(" + now.ToString() + ")"+ tbName.Text + ":" + rtbSentMsg.Text + '\n';
            rtbSentMsg.Clear();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            Send();
        }
        void ListenMsg()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string msg = Encoding.UTF8.GetString(buffer, 0, bytes);
                    rtbReceivedMsg.Text += msg + '\n';
                }
            }
            catch
            {
                client.Close();
                MessageBox.Show("Ngắt kết nối với máy chủ", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Close();
        }
    }
}