using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }
        TcpListener server;
        Dictionary<string, TcpClient> clients;
        void Connect()
        {
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234 );
                server.Start();
                Task.Run(() =>
                {
                    TcpClient client = server.AcceptTcpClient();
                    clients = new Dictionary<string, TcpClient>();
                    NetworkStream stream = client.GetStream();
                    try
                    {
                        StreamReader reader = new StreamReader(stream);
                        string name = reader.ReadLine();
                        clients.Add(name, client);
                        rtxtShow.Text += (name + " has joined the chat.\n");
                    }
                    catch (IOException)
                    {
                        // Client has disconnected
                        client.Close();
                        return;
                    }

                    while (true)
                    {
                        try
                        {
                            StreamReader reader = new StreamReader(stream);
                            string message = reader.ReadLine();

                            if (message != null)
                            {
                                
                                // Parse the message to extract the recipient and content
                                string[] parts = message.Split(new char[] { ':' }, 2);
                                string recipient = parts[0];
                                string content = parts[1];
                                rtxtShow.Text += ($"{recipient} : {content}");

                                // Send the message to the recipient
                                if (clients.ContainsKey(recipient))
                                {
                                    TcpClient recipientClient = clients[recipient];
                                    StreamWriter writer = new StreamWriter(recipientClient.GetStream());
                                    writer.WriteLine(content);
                                    writer.Flush();
                                }
                                else
                                {
                                    rtxtShow.Text += ("Error: " + recipient + " not found.\n");
                                }
                            }
                        }
                        catch (IOException)
                        {
                            // Client has disconnected
                            break;
                        }
                    }

                    // Remove the client from the dictionary
                    foreach (string name in clients.Keys)
                    {
                        if (clients[name] == client)
                        {
                            clients.Remove(name);
                            rtxtShow.Text += (name + " has left the chat.\n");
                            break;
                        }
                    }

                    client.Close();
                });
            }
            catch(Exception ex)
            {

            }
        }
        private void ServerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
