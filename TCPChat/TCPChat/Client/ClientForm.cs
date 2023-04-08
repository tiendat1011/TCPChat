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
        }

        void send(Socket client)
        {
            byte[] data = Encoding.UTF8.GetBytes(txtMess.Text);
            client.Send(data);
            lstMess.Items.Add(txtMess.Text);
        }

        void receive(Socket client)
        {
            while (true)
            {
            byte[] buffer = new byte[1024];
            client.Receive(buffer);
            string m = Encoding.UTF8.GetString(buffer);
            lstMess.Items.Add(m);
            }
        }
    }
}
