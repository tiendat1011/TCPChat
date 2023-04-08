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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length > 0 && txtIPadd.Text.Length > 0 && txtPort.Text.Length > 0)
                btnLogin.Enabled = true;
            else
                btnLogin.Enabled = false;
        }

        private void txtIPadd_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length > 0 && txtIPadd.Text.Length > 0 && txtPort.Text.Length > 0)
                btnLogin.Enabled = true;
            else
                btnLogin.Enabled = false;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length > 0 && txtIPadd.Text.Length > 0 && txtPort.Text.Length > 0)
                btnLogin.Enabled = true;
            else
                btnLogin.Enabled = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername_TextChanged((object)sender, e);
            txtIPadd_TextChanged(sender, e);
            txtPort_TextChanged(sender, e);
        }
    }
}
