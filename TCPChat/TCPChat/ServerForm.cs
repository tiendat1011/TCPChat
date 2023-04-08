using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TCPChat
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        TcpListener server = null;
        Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
        NetworkStream stream = null;

        void StartServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
            //server = new TcpListener(IPAddress.Parse(tbIP.Text), Int32.Parse(tbPort.Text));
            server.Start();
            rtbMessage.Text += ("Server đang lắng nghe\n");
            Thread connect = new Thread(() =>
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    stream = client.GetStream();
                    string name = InformNewConnect(client);
                    Thread thread1 = new Thread(() => HandleClient(name));
                    thread1.Start();
                }

            });
            connect.Start();
        }
        string InformNewConnect(TcpClient client)
        {
            DateTime now = DateTime.Now;
            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);
            string name = Encoding.UTF8.GetString(buffer, 0, bytes);
            clients.Add(name, client);
            SendAllClient(name + " đã kết nối", client);
            rtbMessage.Text += ($"({now.ToString()}) {name} đã kết nối\n");
            stream.Flush();
            UpdateClientList();
            return name;
        }
        void HandleClient(string name)
        {
            DateTime now = DateTime.Now;
            TcpClient client = clients[name];
            while (true)
            {
                try
                {
                    string msg = ReceiveClient(client);
                    rtbMessage.Text += ($"({now.ToString()}) {name}: {msg}\n");
                    SendAllClient(name + " : " + msg, client);
                }
                catch
                {
                    clients.Remove(name);
                    SendAllClient(name + " đã ngắt kết nối", client);
                }    
            }
        }
        string ReceiveClient(TcpClient client)
        {
            byte[] buffer = new byte[1024];
            int bytes = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytes);
        }

        void SendAllClient(string msg, TcpClient client)
        {
            DateTime now = DateTime.Now;
            TcpClient Client = null;
            foreach(var key in clients.Keys)
            {
                Client = clients[key];
                if (Client.Connected && Client != client)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);
                    stream.Write(data, 0, data.Length);
                }
                else if(Client.Connected == false)
                {
                    byte[] data = Encoding.UTF8.GetBytes(key + " đã ngắt kết nối");
                    stream.Write(data, 0, data.Length);
                    clients.Remove(key);
                }    
            }
            stream.Flush();
        }
        void UpdateClientList()
        {
            List<string> names = new List<string>(clients.Keys);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Key", typeof(string));

            // Duyệt qua từng key trong danh sách và thêm chúng vào DataTable
            foreach (string name in names)
            {
                DataRow row = dataTable.NewRow();
                row["Key"] = name;
                dataTable.Rows.Add(row);
            }

            // Liên kết DataTable với DataGridView
            dgvClientList.DataSource = dataTable;

            // Đặt tên cho cột Key
            dgvClientList.Columns[0].HeaderText = "Client Names";
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            StartServer();
        }
        string strg = string.Empty;
        private void dgvClientList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            strg = dgvClientList.CurrentCell.ToString();
        }
        void RemoveClient()
        {
            TcpClient client = null;
            if (clients.TryGetValue(strg, out client))
            {
                byte[] buffer = Encoding.UTF8.GetBytes("Bạn đã bi ngắt kết nối khỏi phòng chat");
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                client.Close();
                clients.Remove(strg);
                SendAllClient(strg + " đã bị ngắt kết nối", client);
            }
            else
            {
                MessageBox.Show("Không tìm thấy client " + strg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveClient();
        }
    }
}
    