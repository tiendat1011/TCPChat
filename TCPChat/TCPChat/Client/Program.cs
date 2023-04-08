using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            LoginForm loginform = new LoginForm();
            Application.Run(loginform);

            if (loginform.DialogResult == DialogResult.OK)
            {
                ClientForm clientform = new ClientForm();
                clientform.m_clientSocket = loginform.clientSocket;
                clientform.m_ipAdress = loginform.ipAdress;
                clientform.m_port = loginform.port;
                clientform.m_remoteEP = loginform.remoteEP;
                clientform.m_user = loginform.user;

                clientform.ShowDialog();
            }
        }
    }
}
