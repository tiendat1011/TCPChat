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

    }
}
