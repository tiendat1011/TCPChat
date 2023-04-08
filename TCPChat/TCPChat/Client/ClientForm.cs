using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Client
{
    public partial class ClientForm : Form
    {
        public Socket m_clientSocket;
        public IPEndPoint m_remoteEP;
        public IPAddress m_ipAdress;
        public string m_user;
        public int m_port;

        public ClientForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            connect();
        }

        void connect()
        {
            try
            {
                m_clientSocket.Connect(m_remoteEP);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Thread listen = new Thread(receive);
            listen.IsBackground = true;
            listen.Start();
        }

        void send()
        {
            byte[] data = Encoding.UTF8.GetBytes(txtMess.Text);
            m_clientSocket.Send(data);
            lstMess.Items.Add(txtMess.Text);
        }

        void receive()
        {
            while (true)
            {
            byte[] buffer = new byte[1024];
            m_clientSocket.Receive(buffer);
            string m = Encoding.UTF8.GetString(buffer);
            lstMess.Items.Add(m);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            send();
        }
    }
}
