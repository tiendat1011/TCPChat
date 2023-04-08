using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginForm : Form
    {
        public Socket clientSocket;
        public IPEndPoint remoteEP;
        public IPAddress ipAdress;
        public string user;
        public int port;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            user = txtUsername.Text;
            ipAdress = IPAddress.Parse(txtIPadd.Text);
            port = int.Parse(txtPort.Text);

            try
            {
                remoteEP = new IPEndPoint(ipAdress, port);
                clientSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(remoteEP);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
